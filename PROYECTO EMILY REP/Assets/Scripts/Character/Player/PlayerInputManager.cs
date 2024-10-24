using System;
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
        public bool switchMovement_Input = false;

        [Header("Player Actions input")]
        [SerializeField] bool dodge_Input = false;
        [SerializeField] bool sprint_Input = false;
        [SerializeField] bool jump_Input = false;

        [Header("Bumper Inputs")]
        [SerializeField] bool RB_Input = false;
        [SerializeField] bool LB_Input = false;


        [Header("Trigguer Inputs")]
        //[SerializeField] bool RT_Input = false;
        [SerializeField] bool hold_RT_Input = false;
        [SerializeField] bool hold_Alt_Input = false;


        [Header("Two Hand Inputs")]
        [SerializeField] bool two_Hand_Input = false;
        [SerializeField] bool two_Hand_Right_Weapon_Input = false;
        [SerializeField] bool two_Hand_Left_Weapon_Input = false;

        [Header("Switch Armament")]
        [SerializeField] bool switch_Right_Weapon_Input = false;
        [SerializeField] bool switch_Left_Weapon_Input = false;

        [Header("Qued Inputs")]
        [SerializeField] bool input_Que_Is_Active = false;
        [SerializeField] float default_Que_Input_Time = 0.35f;
        [SerializeField] float que_Input_Timer = 0;
        [SerializeField] bool que_RB_Input = false;
        [SerializeField] bool que_RT_Input = false;

        [Header("Interactions")]
        [SerializeField] bool interaction_Input = false;
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
                playerControls.PlayerMovement.SwitchMovement.performed += i => switchMovement_Input = !switchMovement_Input;

                //Actions
                playerControls.PlayerActions.Dodge.performed += i => dodge_Input = true;
                playerControls.PlayerActions.Jump.performed += i => jump_Input = true;

                playerControls.PlayerActions.SwitchRightItem.performed += i => switch_Right_Weapon_Input = true;
                playerControls.PlayerActions.SwitchLeftItem.performed += i => switch_Left_Weapon_Input = true;

                //Bumpers
                playerControls.PlayerActions.RB.performed += i => RB_Input = true;
                playerControls.PlayerActions.LB.performed += i => LB_Input = !LB_Input;
                playerControls.PlayerActions.LB.canceled += i => player.playerNetworkManager.isBlocking.Value = false;

                //Trigguers
                playerControls.PlayerActions.HoldRT.performed += i => hold_RT_Input = true;
                playerControls.PlayerActions.HoldRT.canceled += i => hold_RT_Input = false;

                playerControls.PlayerActions.HoldAlt.performed += i => hold_Alt_Input = true;
                playerControls.PlayerActions.HoldAlt.canceled += i => hold_Alt_Input = false;

                //Two hand
                playerControls.PlayerActions.TwoHandWeapon.performed += i => two_Hand_Input = true;
                playerControls.PlayerActions.TwoHandWeapon.canceled += i => two_Hand_Input = false;
                playerControls.PlayerActions.TwoHandRightWeapon.performed += i => two_Hand_Right_Weapon_Input = true;
                playerControls.PlayerActions.TwoHandRightWeapon.canceled += i => two_Hand_Right_Weapon_Input = false;
                playerControls.PlayerActions.TwoHandLeftWeapom.performed += i => two_Hand_Left_Weapon_Input = true;
                playerControls.PlayerActions.TwoHandLeftWeapom.canceled += i => two_Hand_Left_Weapon_Input = false;
                //Bloquar la camara a un objetivo
                playerControls.PlayerActions.LookOn.performed += i => lockOn_Input = true;
                playerControls.PlayerActions.SeekLeftLockOntarget.performed += i => lockOn_Left_Input = true;
                playerControls.PlayerActions.SeekRightLockOntarget.performed += i => lockOn_Right_Input = true;

                //Detectar si cambia o no la entrada para saber si corre o deja de correr 
                playerControls.PlayerActions.Sprint.performed += i => sprint_Input = true;
                playerControls.PlayerActions.Sprint.canceled += i => sprint_Input = false;

                //Qued inputs
                playerControls.PlayerActions.queRB.performed += i => QueInput(ref que_RB_Input);
                playerControls.PlayerActions.queRT.performed += i => QueInput(ref que_RT_Input);

                //Interactions
                playerControls.PlayerActions.Interaction.performed += i => interaction_Input = true;

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
            HandleTwoHandInput();
            HandleLockOnInput();
            HandleLockOnSwitchTargetInput();
            HandlePlayerMovementInput();
            HandleCameraMovementInput();
            HandleDodgeInput();
            HandleSprintInput();
            HandleJumpInput();
            HandleRBInput();
            HandleLBInput();
            HandleRTInput();
            HandleChargeRTInput();
            HandleSwitchRightWeaponInput();
            HandleSwitchLeftWeaponInput();
            HandleQuedInputs();
            HandleInteractionInput();
        }

        //Two hand 
        private void HandleTwoHandInput()
        {
            if (!two_Hand_Input)
                return;

            if (two_Hand_Right_Weapon_Input)
            {
                RB_Input = false;
                two_Hand_Right_Weapon_Input = false;
                player.playerNetworkManager.isBlocking.Value = false;

                if (player.playerNetworkManager.isTwoHandingWeapon.Value)
                {

                    player.playerNetworkManager.isTwoHandingWeapon.Value = false;
                    return;
                }
                else
                {

                    player.playerNetworkManager.isTwoHandingRightWeapon.Value = true;
                    return;
                }
            }
            else if (two_Hand_Left_Weapon_Input)
            {
                LB_Input = false;
                two_Hand_Left_Weapon_Input = false;
                player.playerNetworkManager.isBlocking.Value = false;

                if (player.playerNetworkManager.isTwoHandingWeapon.Value)
                {

                    player.playerNetworkManager.isTwoHandingWeapon.Value = false;
                    return;
                }
                else
                {

                    player.playerNetworkManager.isTwoHandingLeftWeapon.Value = true;
                    return;
                }
            }

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

            if (!switchMovement_Input) // Si está en modo caminar
            {
                moveAmount = Mathf.Min(moveAmount, 0.5f); // Limita a un máximo de 0.5 (caminar)
            }
            else // Si está en modo trotar
            {
                moveAmount = Mathf.Clamp(moveAmount, 0f, 1f); // Permite hasta 1 (trotar)
            }

            if (player == null)
                return;

            if (moveAmount != 0)
            {
                player.playerNetworkManager.isMoving.Value = true;
            }
            else
            {
                player.playerNetworkManager.isMoving.Value = false;
            }
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

        private void HandleRBInput()
        {
            if (two_Hand_Input)
                return;

            if (RB_Input)
            {
                RB_Input = false;

                //No queremos hacer nada si tenemos una ventana abierta en un futuro 
                player.playerNetworkManager.SetCharacterActionHand(true);

                //si usamos un arma en cada mano queremos ejecutar el ataque con 2 manos

                player.playerCombatManager.PerformWeaponBasedAction(player.playerInventoryManager.currentRightHandWeapon.oh_RB_Action, player.playerInventoryManager.currentRightHandWeapon);
            }
        }

        private void HandleLBInput()
        {
            if (two_Hand_Input)
                return;

            if (LB_Input)
            {
                //LB_Input = false;
                //Debug.Log("Entra al click");
                //RT_Input = false;

                //No queremos hacer nada si tenemos una ventana abierta en un futuro 
                player.playerNetworkManager.SetCharacterActionHand(false);

                //si usamos un arma en cada mano queremos ejecutar el ataque con 2 manos

                player.playerCombatManager.PerformWeaponBasedAction(player.playerInventoryManager.currentLeftHandWeapon.oh_LB_Action, player.playerInventoryManager.currentLeftHandWeapon);
            }
        }

        private void HandleRTInput()
        {
            if (hold_RT_Input && !hold_Alt_Input)
            {
                //RT_Input = false;
                LB_Input = false;

                // No queremos hacer nada si tenemos una ventana abierta en un futuro
                player.playerNetworkManager.SetCharacterActionHand(true);

                // Si usamos un arma en cada mano queremos ejecutar el ataque con 2 manos
                player.playerCombatManager.PerformWeaponBasedAction(player.playerInventoryManager.currentRightHandWeapon.oh_RT_Action, player.playerInventoryManager.currentRightHandWeapon);
            }
        }

        private void HandleChargeRTInput()
        {
            // Solo verificamos esto cuando vamos a realizar alguna acción que se pueda cargar (hechizos de carga, alguna arma cargada, etc.)
            if (hold_Alt_Input && hold_RT_Input)
            {
                //RT_Input = false;
                LB_Input = false;
               
                if (player.isPerformingAction)
                {
                    if (player.playerNetworkManager.isUsingRightHand.Value)
                    {
                        
                        player.playerNetworkManager.isChargingAttack.Value = (hold_Alt_Input && hold_RT_Input);
                    }
                }
            }
            else
            {
                player.playerNetworkManager.isChargingAttack.Value = (hold_Alt_Input && hold_RT_Input);
            }
        }

        private void HandleSwitchRightWeaponInput()
        {
            if (switch_Right_Weapon_Input)
            {
                switch_Right_Weapon_Input = false;
                player.playerEquipmentManager.SwitchRightWeapon();
            }
        }

        private void HandleSwitchLeftWeaponInput()
        {
            if (switch_Left_Weapon_Input)
            {
                switch_Left_Weapon_Input = false;
                player.playerEquipmentManager.SwitchLeftWeapon();
            }
        }

        private void HandleInteractionInput()
        {
            if (interaction_Input)
            {
                interaction_Input = false;

                player.playerInteractionManager.Interact();
            }
        }
        #endregion

        private void QueInput(ref bool quedInput)
        {
            que_RB_Input = false;
            que_RT_Input = false;

            //Realizamos una comprobacion para saber si tenemos una pantalla abierta

            if(player.isPerformingAction || player.playerNetworkManager.isJumping.Value)
            {
                quedInput = true;
                que_Input_Timer = default_Que_Input_Time;
                input_Que_Is_Active = true;

            }
        }

        private void ProcessQuedInputs()
        {
            if (player.isDead.Value)
                return;

            if (que_RB_Input)
                RB_Input = true;

            if (que_RT_Input)
                LB_Input = true;
        }

        private void HandleQuedInputs()
        {
            if (input_Que_Is_Active)
            {

                if (que_Input_Timer > 0)
                {
                    que_Input_Timer -= Time.deltaTime;
                    ProcessQuedInputs();
                }
                else
                {
                    que_RB_Input = false;
                    que_RT_Input = false;

                    input_Que_Is_Active = false;
                    que_Input_Timer = 0;
                }
            }
        }
    }
}
 