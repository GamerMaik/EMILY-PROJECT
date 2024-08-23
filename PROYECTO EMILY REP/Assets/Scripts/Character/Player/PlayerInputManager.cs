using UnityEngine;
using UnityEngine.SceneManagement;

namespace KC
{
    public class PlayerInputManager : MonoBehaviour
    {
        #region Variables
        public static PlayerInputManager instance;
        public PlayerManager player;
        PlayerControls playerControls;

        [Header("Camera Movement Input")]
        [SerializeField] Vector2 cameraInput;
        public float cameraHorizontalInput;
        public float cameraVerticalInput;

        [Header("Look On INput")]
        [SerializeField] bool lockOn_Input;
        [SerializeField] bool lockOn_Left_Input;
        [SerializeField] bool lockOn_Right_Input;
        private Coroutine lockOnCoroutine;

        [Header("Player Movement Input")]
        [SerializeField] Vector2 movementInput;
        public float horizontal_Input; 
        public float vertical_Input;
        public float moveAmount;

        [Header("Player Actions input")]
        [SerializeField] bool dodge_Input = false;
        [SerializeField] bool sprint_Input = false;
        [SerializeField] bool jump_Input = false;
        [SerializeField] bool RB_Input = false;
        [SerializeField] bool hold_Shift_Input = false;
        [SerializeField] bool hold_RT_Input = false;

        

        #endregion

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
           
        }
        private void Start()
        {
            DontDestroyOnLoad(gameObject);

            SceneManager.activeSceneChanged += OnSceneChange;
            instance.enabled = false;
            if (playerControls != null)
            {
                playerControls.Disable();
            }
        }
        private void OnSceneChange(Scene oldScene, Scene newScene)
        {
            //Si se carga la Escena del Mundo principal se habilitan los controles del jugador
            if (newScene.buildIndex== WorldSaveGameManager.instance.GetWorldSceneIndex())
            {
                instance.enabled = true;

                if (playerControls != null)
                {
                    playerControls.Enable();
                }
                //posiblemente aca se pueda bloquear el cursor en ves de usar otro script para controlarlo
            }
            //Si estamos en cualquier otra escene se desactiva
            else
            {
                instance.enabled=false;

                if (playerControls != null)
                {
                    playerControls.Disable();
                }
            }
        }
        private void OnEnable()
        {
            if (playerControls == null)
            {
                playerControls = new PlayerControls();

                playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
                playerControls.PlayerCamera.Movement.performed += i => cameraInput = i.ReadValue<Vector2>();
                playerControls.PlayerActions.Dodge.performed += i => dodge_Input = true;
                playerControls.PlayerActions.Jump.performed += i => jump_Input = true;
                playerControls.PlayerActions.RB.performed += i => RB_Input = true;

                //Cargar tipos de ataque
                playerControls.PlayerActions.HoldShift.performed += i => hold_Shift_Input = true;
                playerControls.PlayerActions.HoldShift.canceled += i => hold_Shift_Input = false;

                playerControls.PlayerActions.HoldRT.performed += i => hold_RT_Input = true;
                playerControls.PlayerActions.HoldRT.canceled += i => hold_RT_Input = false;

                //Bloquar la camara a un objetivo
                playerControls.PlayerActions.LookOn.performed += i => lockOn_Input = true;
                playerControls.PlayerActions.SeekLeftLockOntarget.performed += i => lockOn_Left_Input = true;
                playerControls.PlayerActions.SeekRightLockOntarget.performed += i => lockOn_Right_Input = true;

                //Detectar si cambia o no la entrada para saber si corre o deja de correr 
                playerControls.PlayerActions.Sprint.performed += i => sprint_Input = true;
                playerControls.PlayerActions.Sprint.canceled += i => sprint_Input = false;

                
            }
            playerControls.Enable();
        }
        private void OnDestroy()
        {
            //Cuando el objeto es destruido cancelamos la suscripcicon del evento nuevamente
            SceneManager.activeSceneChanged -= OnSceneChange;
        }
        private void OnApplicationFocus(bool focus)
        {
            if (enabled)
            {
                if (focus)
                {
                    playerControls.Enable();
                }
                else
                {
                    playerControls.Disable();
                }
            }
        }
        private void Update()
        {
            HandleAllInputs();
        }
        private void HandleAllInputs() 
        {
            HandleLockOnInput();
            HandleLockOnSwitchTargetInput();
            HandlePlayerMovementInput();
            HandleCameraMovementInput();
            HandleDodgeInput();
            HandleSprintInput();
            HandleJumpInput();
            HandRBInput();
        }

        #region Look On
        private void HandleLockOnInput()
        {
            //si el objetivo esta muerto
            if(player.playerNetworkManager.isLokedOn.Value)
            {
                if(player.playerCombatManager.currentTarget == null)
                    return;

                if (player.playerCombatManager.currentTarget.isDead.Value)
                {
                    player.playerNetworkManager.isLokedOn.Value = false;

                }
                //Desbloquear el objetivo 
                if (lockOnCoroutine != null)
                    StopCoroutine(lockOnCoroutine);

                lockOnCoroutine = StartCoroutine(PlayerCamera.instance.WaithThenFindNewTarget()); 
                
            }

            if (lockOn_Input && player.playerNetworkManager.isLokedOn.Value)
            {
                lockOn_Input = false;
                PlayerCamera.instance.ClearLockOnTargets();
                player.playerNetworkManager.isLokedOn.Value = false;
                //verificamos si tenemos un objetivo
                return;
            }

            if (lockOn_Input && !player.playerNetworkManager.isLokedOn.Value)
            {
                lockOn_Input = false;
                //si se usa un arma de rango largo no se sbloqueará al objetivo ya que no queremos interferir con la vista o el apuntado
                PlayerCamera.instance.HandleLocatingLockOnTarget();


                if(PlayerCamera.instance.nearestLockOnTarget != null)
                {
                    player.playerCombatManager.SetTarget(PlayerCamera.instance.nearestLockOnTarget);
                    player.playerNetworkManager.isLokedOn.Value = true;
                }
            }
        }

        private void HandleLockOnSwitchTargetInput()
        {
            if (lockOn_Left_Input)
            {
                lockOn_Left_Input = false;
                if (player.playerNetworkManager.isLokedOn.Value)
                {
                    PlayerCamera.instance.HandleLocatingLockOnTarget();

                    if (PlayerCamera.instance.leftLockOnTarget != null)
                    {
                        player.playerCombatManager.SetTarget(PlayerCamera.instance.leftLockOnTarget);
                    }
                }
            }

            if (lockOn_Right_Input)
            {
                lockOn_Right_Input = false;
                if (player.playerNetworkManager.isLokedOn.Value)
                {
                    PlayerCamera.instance.HandleLocatingLockOnTarget();

                    if (PlayerCamera.instance.rightLockOnTarget != null)
                    {
                        player.playerCombatManager.SetTarget(PlayerCamera.instance.rightLockOnTarget);
                    }
                }
            }
        }

        #endregion

        #region Movimiento
        private void HandlePlayerMovementInput()
        {
            vertical_Input = movementInput.y;
            horizontal_Input = movementInput.x;

            //Regresa un numer absoluto(para moverse en horizontal)
            moveAmount = Mathf.Clamp01(Mathf.Abs(vertical_Input) + Mathf.Abs(horizontal_Input));

            if (moveAmount <= 0.5 && moveAmount > 0)
            {
                moveAmount = 0.5f;
            }
            else if (moveAmount >0.5 && moveAmount <= 1)
            {
                moveAmount = 1;
            }

            if (player == null)
                return;
            //pasamos 0 ya que solo queremos que el personaje
            //esté inactivo, camine, o corra en una direccion que es al frente, ademas
            //el personaje siempre se mueve a la direccion que mira la camara

            if (!player.playerNetworkManager.isLokedOn.Value || player.playerNetworkManager.isSprinting.Value)
            {
                player.playerAnimatorManager.UpdateAnimatorMovementParameters(0, moveAmount, player.playerNetworkManager.isSprinting.Value);
            }
            else
            {
                player.playerAnimatorManager.UpdateAnimatorMovementParameters(horizontal_Input, vertical_Input, player.playerNetworkManager.isSprinting.Value);
            }
        }
        private void HandleCameraMovementInput()
        {
            cameraVerticalInput = cameraInput.y;
            cameraHorizontalInput = cameraInput.x;
        }
        #endregion

        #region Acciones
        private void HandleDodgeInput()
        {
            if (dodge_Input)
            {
                dodge_Input = false;
                //En un futuro cuando tengamos un menu o interfaz activa no queremos realizar una accion si dicha ventana está abierta
                player.playerlocomotionManager.AttempToPerformDodge();
            }
        }

        private void HandleSprintInput()
        {
            if (sprint_Input)
            {
                player.playerlocomotionManager.HandleSprinting();
            }
            else
            {
                player.playerNetworkManager.isSprinting.Value = false;
            }
        }

        private void HandleJumpInput()
        {
            if (jump_Input)
            {
                jump_Input = false;

                player.playerlocomotionManager.AttempToPerformJump();
            }
        }

        private void HandRBInput()
        {
            if (RB_Input)
            {
                RB_Input = false;

                //No queremos hacer nada si tenemos una ventana abierta en un futuro 
                player.playerNetworkManager.SetCharacterActionHand(true);

                //si usamos un arma en cada mano queremos ejecutar el ataque con 2 manos

                player.playerCombatManager.PerformWeaponBasedAction(player.playerInventoryManager.currentRightHandWeapon.oh_RB_Action, player.playerInventoryManager.currentRightHandWeapon);
            }
        }

        private void HandleStrongAttack()
        {
            if (hold_Shift_Input && hold_RT_Input)
            {
                //Implementar el golpe cargado segun a la cantidad de presión
            }
            else
            {
                //Implementar el golpe normal
            }
        }
        #endregion
    }
}
