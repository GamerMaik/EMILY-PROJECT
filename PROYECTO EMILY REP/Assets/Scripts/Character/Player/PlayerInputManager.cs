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
        [SerializeField] bool lookOn_Input;

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

                //Detectar si cambia o no la entrada para saber si corre o deja de correr 
                playerControls.PlayerActions.Sprint.performed += i => sprint_Input = true;
                playerControls.PlayerActions.Sprint.canceled += i => sprint_Input = false;

                //Bloquar la camara a un objetivo
                playerControls.PlayerActions.LookOn.performed += i => lookOn_Input = true;
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
            HandleLookOnInput();
            HandlePlayerMovementInput();
            HandleCameraMovementInput();
            HandleDodgeInput();
            HandleSprintInput();
            HandleJumpInput();
            HandRBInput();
        }

        #region Look On
        private void HandleLookOnInput()
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
                
            }
            if (lookOn_Input && player.playerNetworkManager.isLokedOn.Value)
            {
                lookOn_Input = false;
                //verificamos si tenemos un objetivo
                return;
            }

            if (lookOn_Input && !player.playerNetworkManager.isLokedOn.Value)
            {
                lookOn_Input = false;
                //si se usa un arma de rango largo no se sbloqueará al objetivo ya que no queremos interferir con la vista o el apuntado


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
            player.playerAnimatorManager.UpdateAnimatorMovementParameters(0, moveAmount, player.playerNetworkManager.isSprinting.Value);
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
        #endregion
    }
}
