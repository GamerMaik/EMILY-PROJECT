using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Netcode;
using System.Security.Cryptography;
using System.Text;

namespace KC
{
    public class PlayerManager : CharacterManager
    {

        [HideInInspector] public PlayerAnimatorManager playerAnimatorManager;
        [HideInInspector] public PlayerLocomotionManager playerlocomotionManager;
        [HideInInspector] public PlayerNetworkManager playerNetworkManager;
        [HideInInspector] public PlayerStatsManager playerStatsManager;
        [HideInInspector] public PlayerInventoryManager playerInventoryManager;
        [HideInInspector] public PlayerEquipmentManager playerEquipmentManager;
        [HideInInspector] public PlayerCombatManager playerCombatManager;
        [HideInInspector] public PlayerInteractionManager playerInteractionManager;
        [HideInInspector] public PlayerEffectsManager playerEffectsManager;
        [HideInInspector] public PlayerBodyManager playerBodyManager;

        protected override void Awake()
        {
            base.Awake();

            //Haz más cosas, solo para el jugador.

            playerlocomotionManager = GetComponent<PlayerLocomotionManager>();
            playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
            playerNetworkManager = GetComponent<PlayerNetworkManager>();
            playerStatsManager = GetComponent<PlayerStatsManager>();
            playerInventoryManager = GetComponent<PlayerInventoryManager>();
            playerEquipmentManager = GetComponent<PlayerEquipmentManager>();
            playerCombatManager = GetComponent<PlayerCombatManager>();
            playerInteractionManager = GetComponent<PlayerInteractionManager>();
            playerEffectsManager = GetComponent<PlayerEffectsManager>();
            playerBodyManager = GetComponent<PlayerBodyManager>();
        }

        protected override void Update()
        {
            base.Update();
            if (!IsOwner) 
                return;

            //Se ejecuta todos los movimientos de nuestros personajes
            playerlocomotionManager.HandleAllMovement();

            //Regeneracion de estamina
            playerStatsManager.RegenerateStamina();
        }

        protected override void LateUpdate()
        {
            if (!IsOwner)
                return;
            
            base.LateUpdate();

            PlayerCamera.instance.HandleAllCameraActions();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            
        }

        protected override void OnDisable()
        {
            base.OnDisable();
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnectedCalback;

            //Si el personaje es propio
            if (IsOwner)
            {
                PlayerCamera.instance.player = this;
                PlayerInputManager.instance.player = this;
                WorldSaveGameManager.instance.player = this;

                playerNetworkManager.vitality.OnValueChanged += playerNetworkManager.SetNewMaxHealthValue;
                playerNetworkManager.endurance.OnValueChanged += playerNetworkManager.SetNewMaxStaminaValue;

                playerNetworkManager.currentHealth.OnValueChanged += PlayerUIManager.instance.playerUIHudManager.SetNewHealthValue;
                playerNetworkManager.currentStamina.OnValueChanged += PlayerUIManager.instance.playerUIHudManager.SetNewStaminaValue;
                playerNetworkManager.currentStamina.OnValueChanged += playerStatsManager.ResetStaminaRegenrationTimer;

            }
            if (!IsOwner)
                characterNetworkManager.currentHealth.OnValueChanged += characterUIManager.OnHPChanged;

            //Body Type
            playerNetworkManager.isMale.OnValueChanged += playerNetworkManager.OnIsMaleChange;
            //Estadisticas
            playerNetworkManager.currentHealth.OnValueChanged += playerNetworkManager.CheckHP;

            //Bloquear A
            playerNetworkManager.isLokedOn.OnValueChanged += playerNetworkManager.OnIsLockedOnChange;
            playerNetworkManager.currentTargetNetworkObjetID.OnValueChanged += playerNetworkManager.OnLockOnTargetIdChange;

            //Equipamiento
            playerNetworkManager.currentRightHandWeaponID.OnValueChanged += playerNetworkManager.OnCurrentRightHandWeaponIdChange;
            playerNetworkManager.currentLeftHandWeaponID.OnValueChanged += playerNetworkManager.OnCurrentLeftHandWeaponIdChange;
            playerNetworkManager.currentWeaponBeingUsed.OnValueChanged += playerNetworkManager.OnCurrentWeaponBeingUsedIDChange;
            playerNetworkManager.isBlocking.OnValueChanged += playerNetworkManager.OnIsBlockingChanged;

            playerNetworkManager.headEquipmentID.OnValueChanged += playerNetworkManager.OnHeadEquipmentChanged;
            playerNetworkManager.bodyEquipmentID.OnValueChanged += playerNetworkManager.OnBodyEquipmentChanged;
            playerNetworkManager.legEquipmentID.OnValueChanged += playerNetworkManager.OnLegEquipmentChanged;
            playerNetworkManager.handEquipmentID.OnValueChanged += playerNetworkManager.OnHandEquipmentChanged;

            //Two Hand
            playerNetworkManager.isTwoHandingWeapon.OnValueChanged += playerNetworkManager.OnIsTwoHandingWeaponChanged;
            playerNetworkManager.isTwoHandingRightWeapon.OnValueChanged += playerNetworkManager.OnIsTwoHandingRightWeaponChanged;
            playerNetworkManager.isTwoHandingLeftWeapon.OnValueChanged += playerNetworkManager.OnIsTwoHandingLeftWeaponChanged;

            //Flags
            playerNetworkManager.isChargingAttack.OnValueChanged += playerNetworkManager.IsChargingAttackChanged;


            if (IsOwner && !IsServer)
            {
                //Debug.LogWarning("Ingreso con exito pero no se ejecuta");
                //playerNetworkManager.vitality.OnValueChanged += playerNetworkManager.SetNewMaxHealthValue;
                //playerNetworkManager.endurance.OnValueChanged += playerNetworkManager.SetNewMaxStaminaValue;
                //playerNetworkManager.currentHealth.OnValueChanged += PlayerUIManager.instance.playerUIHudManager.SetNewHealthValue;
                //playerNetworkManager.currentStamina.OnValueChanged += PlayerUIManager.instance.playerUIHudManager.SetNewStaminaValue;
                //playerNetworkManager.currentStamina.OnValueChanged += playerStatsManager.ResetStaminaRegenrationTimer;
                LoadGameDataFromCurrentCharacterData(ref WorldSaveGameManager.instance.currentCharacterData);
            }
        }

        public override void OnNetworkDespawn()
        {
            base.OnNetworkDespawn();

            NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnectedCalback;

            //Si el personaje es propio
            if (IsOwner)
            {
                playerNetworkManager.vitality.OnValueChanged -= playerNetworkManager.SetNewMaxHealthValue;
                playerNetworkManager.endurance.OnValueChanged -= playerNetworkManager.SetNewMaxStaminaValue;

                playerNetworkManager.currentHealth.OnValueChanged -= PlayerUIManager.instance.playerUIHudManager.SetNewHealthValue;
                playerNetworkManager.currentStamina.OnValueChanged -= PlayerUIManager.instance.playerUIHudManager.SetNewStaminaValue;
                playerNetworkManager.currentStamina.OnValueChanged -= playerStatsManager.ResetStaminaRegenrationTimer;

            }
            if (!IsOwner)
                characterNetworkManager.currentHealth.OnValueChanged -= characterUIManager.OnHPChanged;

            //Tipo de cuerpo
            playerNetworkManager.isMale.OnValueChanged -= playerNetworkManager.OnIsMaleChange;
            //Estadisticas
            playerNetworkManager.currentHealth.OnValueChanged -= playerNetworkManager.CheckHP;

            //Bloquear A
            playerNetworkManager.isLokedOn.OnValueChanged -= playerNetworkManager.OnIsLockedOnChange;
            playerNetworkManager.currentTargetNetworkObjetID.OnValueChanged += playerNetworkManager.OnLockOnTargetIdChange;

            //Equipamiento
            playerNetworkManager.currentRightHandWeaponID.OnValueChanged -= playerNetworkManager.OnCurrentRightHandWeaponIdChange;
            playerNetworkManager.currentLeftHandWeaponID.OnValueChanged -= playerNetworkManager.OnCurrentLeftHandWeaponIdChange;
            playerNetworkManager.currentWeaponBeingUsed.OnValueChanged -= playerNetworkManager.OnCurrentWeaponBeingUsedIDChange;

            playerNetworkManager.headEquipmentID.OnValueChanged -= playerNetworkManager.OnHeadEquipmentChanged;
            playerNetworkManager.bodyEquipmentID.OnValueChanged -= playerNetworkManager.OnBodyEquipmentChanged;
            playerNetworkManager.legEquipmentID.OnValueChanged -= playerNetworkManager.OnLegEquipmentChanged;
            playerNetworkManager.handEquipmentID.OnValueChanged -= playerNetworkManager.OnHandEquipmentChanged;

            //Two Hand
            playerNetworkManager.isTwoHandingWeapon.OnValueChanged -= playerNetworkManager.OnIsTwoHandingWeaponChanged;
            playerNetworkManager.isTwoHandingRightWeapon.OnValueChanged -= playerNetworkManager.OnIsTwoHandingRightWeaponChanged;
            playerNetworkManager.isTwoHandingLeftWeapon.OnValueChanged -= playerNetworkManager.OnIsTwoHandingLeftWeaponChanged;
            //Flags
            playerNetworkManager.isChargingAttack.OnValueChanged -= playerNetworkManager.IsChargingAttackChanged;
        }

        private void OnClientConnectedCalback(ulong clienID)
        {
            WorldGameSessionManager.instance.AddPlayerToActivePlayerList(this);
            //Una lista de los jugadores activos en la partida
            if (!IsServer && IsOwner)
            {
                foreach (var player in WorldGameSessionManager.instance.players)
                {
                    //Comprobamos si no somos nostros
                    if (player != this)
                    {
                        player.LoadOtherPlayerCharacterWhenJoiningServer();
                    }
                }
            }
        }
        public override IEnumerator ProcessDeathEvent(bool manuallySelectDeathAnimation = false)
        {

            if (IsOwner)
            {
                PlayerUIManager.instance.playerUIPopUpManager.SendYouDiedPopUp();
                //mmc_agregar pantalla de carga y teleportar al jugador
            }

            //En un futuro verificaremos todos los jugadores que estén vivos y si hay 0 respawnaer a todos los jugadore

            return base.ProcessDeathEvent(manuallySelectDeathAnimation);
        }

        public override void ReviveCharacter()
        {
            base.ReviveCharacter();

            if (IsOwner) 
            {
                isDead.Value = false;
                playerNetworkManager.currentHealth.Value = playerNetworkManager.maxHealth.Value;
                playerNetworkManager.currentStamina.Value = playerNetworkManager.maxStamina.Value;

                //Mas adelante restaurar el Focus sobre un enemigo

                //Reproducir cualquier efecto de revivir

                playerAnimatorManager.PlayerTargetActionAnimation("Empty", false);
            }
        }

        public void SaveGameDataCurrentCharacterData(ref CharacterSaveData currentCharacterData)
        {
            currentCharacterData.characterName = playerNetworkManager.characterName.Value.ToString();
            //currentCharacterData.characterName = EncriptName(playerNetworkManager.characterName.Value.ToString());
            currentCharacterData.isMale = playerNetworkManager.isMale.Value;
            currentCharacterData.xPosition = transform.position.x;
            currentCharacterData.yPosition = transform.position.y;
            currentCharacterData.zPosition = transform.position.z;

            currentCharacterData.currentHealth = playerNetworkManager.currentHealth.Value;
            currentCharacterData.currentStamina = playerNetworkManager.currentStamina.Value;

            currentCharacterData.vitality = playerNetworkManager.vitality.Value;
            currentCharacterData.endurance = playerNetworkManager.endurance.Value;

            //Equipamiento
            currentCharacterData.headEquipment = playerNetworkManager.headEquipmentID.Value;
            currentCharacterData.bodyEquipment = playerNetworkManager.bodyEquipmentID.Value;
            currentCharacterData.legEquipment = playerNetworkManager.legEquipmentID.Value;
            currentCharacterData.handEquipment = playerNetworkManager.handEquipmentID.Value;

            currentCharacterData.rightWeaponIndex = playerInventoryManager.rightHandWeaponIndex;
            currentCharacterData.rightWeapon01 = playerInventoryManager.weaponsRightHandSlots[0].itemID;
            currentCharacterData.rightWeapon02 = playerInventoryManager.weaponsRightHandSlots[1].itemID;
            currentCharacterData.rightWeapon03 = playerInventoryManager.weaponsRightHandSlots[2].itemID;

            currentCharacterData.leftWeaponIndex = playerInventoryManager.leftHandWeaponIndex;
            currentCharacterData.leftWeapon01 = playerInventoryManager.weaponsLeftHandSlots[0].itemID;
            currentCharacterData.leftWeapon02 = playerInventoryManager.weaponsLeftHandSlots[1].itemID;
            currentCharacterData.leftWeapon03 = playerInventoryManager.weaponsLeftHandSlots[2].itemID;

            //Inventory
            currentCharacterData.inventoryItems.Clear(); // Limpia cualquier valor previo

            foreach (var item in playerInventoryManager.itemsInventory)
            {
                int itemID = item.itemID;

                if (currentCharacterData.inventoryItems.ContainsKey(itemID))
                {
                    // Incrementa la cantidad si ya existe el worldSpawnIteractableID
                    currentCharacterData.inventoryItems[itemID]++;
                }
                else
                {
                    // Agrega el worldSpawnIteractableID con una cantidad inicial de 1 si no existe aún
                    currentCharacterData.inventoryItems[itemID] = 1;
                }
            }
        }

        public void LoadGameDataFromCurrentCharacterData(ref CharacterSaveData currentCharacterData)
        {
            playerNetworkManager.characterName.Value = currentCharacterData.characterName;
            playerNetworkManager.isMale.Value = currentCharacterData.isMale;
            playerBodyManager.ToggleBodyType(currentCharacterData.isMale);

            Vector3 myPosition = new Vector3 (currentCharacterData.xPosition, currentCharacterData.yPosition, currentCharacterData.zPosition);
            transform.position = myPosition;

            playerNetworkManager.vitality.Value = currentCharacterData.vitality;
            playerNetworkManager.endurance.Value = currentCharacterData.endurance;

            playerNetworkManager.maxHealth.Value = playerStatsManager.CalculateHealthBasedOnVitalityLevel(playerNetworkManager.vitality.Value);
            playerNetworkManager.maxStamina.Value = playerStatsManager.CalculateStaminaBasedOnEnduranceLevel(playerNetworkManager.endurance.Value);
            playerNetworkManager.currentHealth.Value = currentCharacterData.currentHealth;
            playerNetworkManager.currentStamina.Value = currentCharacterData.currentStamina; 
            PlayerUIManager.instance.playerUIHudManager.SetMaxStaminaValue(playerNetworkManager.maxStamina.Value);

            //Equipment
            // Cargar Equipamiento usando el nuevo método GetItemById
            playerInventoryManager.headEquipmentItem = WorldItemDatabase.Instance.GetItemById(currentCharacterData.headEquipment) as HeadEquipmentItem;
            playerInventoryManager.bodyEquipmentItem = WorldItemDatabase.Instance.GetItemById(currentCharacterData.bodyEquipment) as BodyEquipmentItem;
            playerInventoryManager.legEquipmentItem = WorldItemDatabase.Instance.GetItemById(currentCharacterData.legEquipment) as LegEquipmentItem;
            playerInventoryManager.handEquipmentItem = WorldItemDatabase.Instance.GetItemById(currentCharacterData.handEquipment) as HandEquipmentItem;

            playerInventoryManager.rightHandWeaponIndex = currentCharacterData.rightWeaponIndex;
            playerInventoryManager.leftHandWeaponIndex = currentCharacterData.leftWeaponIndex;

            if (playerInventoryManager.weaponsRightHandSlots.Length > 2)
            {
                playerInventoryManager.weaponsRightHandSlots[0] = WorldItemDatabase.Instance.GetItemById(currentCharacterData.rightWeapon01) as WeaponItem;
                playerInventoryManager.weaponsRightHandSlots[1] = WorldItemDatabase.Instance.GetItemById(currentCharacterData.rightWeapon02) as WeaponItem;
                playerInventoryManager.weaponsRightHandSlots[2] = WorldItemDatabase.Instance.GetItemById(currentCharacterData.rightWeapon03) as WeaponItem;
            }

            if (playerInventoryManager.weaponsLeftHandSlots.Length > 2)
            {
                playerInventoryManager.weaponsLeftHandSlots[0] = WorldItemDatabase.Instance.GetItemById(currentCharacterData.leftWeapon01) as WeaponItem;
                playerInventoryManager.weaponsLeftHandSlots[1] = WorldItemDatabase.Instance.GetItemById(currentCharacterData.leftWeapon02) as WeaponItem;
                playerInventoryManager.weaponsLeftHandSlots[2] = WorldItemDatabase.Instance.GetItemById(currentCharacterData.leftWeapon03) as WeaponItem;
            }

            playerEquipmentManager.EquipArmor();


            playerInventoryManager.itemsInventory.Clear();
            foreach (var itemEntry in currentCharacterData.inventoryItems)
            {
                int itemID = itemEntry.Key;
                int itemCount = itemEntry.Value;

                Item item = WorldItemDatabase.Instance.GetItemById(itemID);

                if (item != null)
                {
                    for (int i = 0; i < itemCount; i++)
                    {
                        playerInventoryManager.AddItemsToInventory(item);
                    }
                }
                else
                {
                    Debug.LogWarning($"Item con ID {itemID} no encontrado en la base de datos.");
                }
            }

            playerInventoryManager.rightHandWeaponIndex = currentCharacterData.rightWeaponIndex;
            if (currentCharacterData.rightWeaponIndex >= 0 &&
                currentCharacterData.rightWeaponIndex < playerInventoryManager.weaponsRightHandSlots.Length &&
                playerInventoryManager.weaponsRightHandSlots[currentCharacterData.rightWeaponIndex] != null)
            {
                playerInventoryManager.rightHandWeaponIndex = currentCharacterData.rightWeaponIndex;
                playerNetworkManager.currentRightHandWeaponID.Value = playerInventoryManager.weaponsRightHandSlots[currentCharacterData.rightWeaponIndex].itemID;
            }
            playerInventoryManager.leftHandWeaponIndex = currentCharacterData.leftWeaponIndex;
            
            if (currentCharacterData.leftWeaponIndex >= 0 &&
                currentCharacterData.leftWeaponIndex < playerInventoryManager.weaponsLeftHandSlots.Length &&
                playerInventoryManager.weaponsLeftHandSlots[currentCharacterData.leftWeaponIndex] != null)
            {
                playerNetworkManager.currentLeftHandWeaponID.Value = playerInventoryManager.weaponsLeftHandSlots[currentCharacterData.leftWeaponIndex].itemID;
            }
        }

        public void LoadOtherPlayerCharacterWhenJoiningServer()
        {
            //Sncronizar Tipo Cuerpo
            playerNetworkManager.OnIsMaleChange(false, playerNetworkManager.isMale.Value);
            //sincronizar armas
            playerNetworkManager.OnCurrentRightHandWeaponIdChange(0, playerNetworkManager.currentRightHandWeaponID.Value);
            playerNetworkManager.OnCurrentLeftHandWeaponIdChange(0, playerNetworkManager.currentLeftHandWeaponID.Value);

            //Sync Armors
            playerNetworkManager.OnHeadEquipmentChanged(0, playerNetworkManager.headEquipmentID.Value);
            playerNetworkManager.OnBodyEquipmentChanged(0, playerNetworkManager.bodyEquipmentID.Value);
            playerNetworkManager.OnLegEquipmentChanged(0, playerNetworkManager.legEquipmentID.Value);
            playerNetworkManager.OnHandEquipmentChanged(0, playerNetworkManager.handEquipmentID.Value);

            //Sincronizar estado de dos manos
            playerNetworkManager.OnIsTwoHandingRightWeaponChanged(false, playerNetworkManager.isTwoHandingRightWeapon.Value);
            playerNetworkManager.OnIsTwoHandingLeftWeaponChanged(false, playerNetworkManager.isTwoHandingLeftWeapon.Value);
            //Sync estado de bloqueo
            playerNetworkManager.OnIsBlockingChanged(false, playerNetworkManager.isBlocking.Value);

            if (playerNetworkManager.isLokedOn.Value)
            {
                playerNetworkManager.OnLockOnTargetIdChange(0, playerNetworkManager.currentTargetNetworkObjetID.Value);
            }
        }

        private string EncriptName(string name)
        {
            using var sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(name));
            
            var sb = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                sb.Append(bytes[i].ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
