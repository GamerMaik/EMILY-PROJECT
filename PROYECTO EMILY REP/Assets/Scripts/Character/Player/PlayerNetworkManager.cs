using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

namespace KC
{
    public class PlayerNetworkManager : CharacterNetworkManager
    {
        PlayerManager player;
        public NetworkVariable<FixedString64Bytes> characterName = new NetworkVariable<FixedString64Bytes>("Character", NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

        [Header("Equipment")]
        public NetworkVariable<int> currentWeaponBeingUsed = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<int> currentRightHandWeaponID = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<int> currentLeftHandWeaponID = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

        public NetworkVariable<bool> isUsingRightHand = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<bool> isUsingLeftHand = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

        [Header("Two Hand Weapon")]
        public NetworkVariable<int> currentWeaponBeingTwoHanded = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<bool> isTwoHandingWeapon = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<bool> isTwoHandingRightWeapon = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<bool> isTwoHandingLeftWeapon = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

        [Header("Armor")]
        public NetworkVariable<bool> isMale = new NetworkVariable<bool>(true, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<int> headEquipmentID = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<int> bodyEquipmentID = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<int> legEquipmentID = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<int> handEquipmentID = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        protected override void Awake()
        {
            base.Awake();

            player = GetComponent<PlayerManager>();
        }

        public void SetCharacterActionHand(bool rightHandedAction)
        {
            if (rightHandedAction)
            {
                isUsingLeftHand.Value = false;
                isUsingRightHand.Value = true;
            }
            else
            {
                isUsingRightHand.Value = false;
                isUsingLeftHand.Value = true;
            }
        }

        public void SetNewMaxHealthValue(int oldVitality, int newVitality)
        {
            maxHealth.Value = player.playerStatsManager.CalculateHealthBasedOnVitalityLevel(newVitality);
            PlayerUIManager.instance.playerUIHudManager.SetMaxHealthValue(maxHealth.Value);
            currentHealth.Value = maxHealth.Value;
        }
        public void SetNewMaxStaminaValue(int oldEndurance, int newEndurance)
        {
            maxStamina.Value = player.playerStatsManager.CalculateStaminaBasedOnEnduranceLevel(newEndurance);
            PlayerUIManager.instance.playerUIHudManager.SetMaxStaminaValue(maxStamina.Value);
            currentStamina.Value = maxStamina.Value;
        }

        public void OnCurrentRightHandWeaponIdChange(int oldID, int newId)
        {
            WeaponItem newWeapon =Instantiate(WorldItemDatabase.Instance.GetWeaponById(newId));
            player.playerInventoryManager.currentRightHandWeapon = newWeapon;
            player.playerEquipmentManager.LoadRightWeapon();

            if (player.IsOwner)
            {
                PlayerUIManager.instance.playerUIHudManager.SetRightWeaponQuickSlotIcon(newId);
            }
        }

        public void OnCurrentLeftHandWeaponIdChange(int oldID, int newId)
        {
            WeaponItem newWeapon =Instantiate(WorldItemDatabase.Instance.GetWeaponById(newId));
            player.playerInventoryManager.currentLeftHandWeapon = newWeapon;
            player.playerEquipmentManager.LoadLeftWeapon();

            if (player.IsOwner)
            {
                PlayerUIManager.instance.playerUIHudManager.SetLeftWeaponQuickSlotIcon(newId);
            }
        }

        public void OnCurrentWeaponBeingUsedIDChange(int oldID, int newId)
        {
            WeaponItem newWeapon = Instantiate(WorldItemDatabase.Instance.GetWeaponById(newId));
            player.playerCombatManager.currentWeaponBeingUsed = newWeapon;

            if (player.IsOwner)
                return;

            if (player.playerCombatManager.currentWeaponBeingUsed != null)
                player.playerAnimatorManager.updateAnimatorController(player.playerCombatManager.currentWeaponBeingUsed.weaponAnimator);
        }

        public override void OnIsBlockingChanged(bool oldStatus, bool newStatus)
        {
            base.OnIsBlockingChanged(oldStatus, newStatus);

            if (IsOwner)
            {
                player.playerStatsManager.blockingPhysicalAbsorption = player.playerCombatManager.currentWeaponBeingUsed.physicalBaseDamagedAbsorption;
                player.playerStatsManager.blockingMagicAbsorption = player.playerCombatManager.currentWeaponBeingUsed.magicBaseDamagedAbsorption;
                player.playerStatsManager.blockingFireAbsorption = player.playerCombatManager.currentWeaponBeingUsed.fireBaseDamagedAbsorption;
                player.playerStatsManager.blockingLightningAbsorption = player.playerCombatManager.currentWeaponBeingUsed.lightningBaseDamagedAbsorption;
                player.playerStatsManager.blockingHolyAbsorption = player.playerCombatManager.currentWeaponBeingUsed.holyBaseDamagedAbsorption;
                player.playerStatsManager.blockingStability = player.playerCombatManager.currentWeaponBeingUsed.stability;
            }
        }

        public void OnIsTwoHandingWeaponChanged(bool oldStatus, bool newStatus)
        {
            if (!isTwoHandingWeapon.Value)
            {
                if (IsOwner)
                {
                    isTwoHandingLeftWeapon.Value = false;
                    isTwoHandingRightWeapon.Value = false;
                }
                player.playerEquipmentManager.UnTwoHandWeapon();
                player.playerEffectsManager.RemoveStaticEffects(WorldCharacterEffectsManager.instance.twoHandingEffects.staticEffectID);
            }
            else
            {
                StaticCharacterEffects twoHandEffect = Instantiate(WorldCharacterEffectsManager.instance.twoHandingEffects);
                player.playerEffectsManager.AddStaticEffects(twoHandEffect);
            }
            player.animator.SetBool("isTwoHandindWeapon", isTwoHandingWeapon.Value);
        }

        public void OnIsTwoHandingRightWeaponChanged(bool oldStatus, bool newStatus)
        {
            if (!isTwoHandingRightWeapon.Value)
                return;

            if (IsOwner)
            {
                currentWeaponBeingTwoHanded.Value = currentRightHandWeaponID.Value;
                isTwoHandingWeapon.Value = true;
            }

            player.playerInventoryManager.currentTwoHandWeapon = player.playerInventoryManager.currentRightHandWeapon;
            player.playerEquipmentManager.TwoHandRightWeapon();
        }

        public void OnIsTwoHandingLeftWeaponChanged(bool oldStatus, bool newStatus)
        {
            if (!isTwoHandingLeftWeapon.Value)
                return;

            if (IsOwner)
            {
                currentWeaponBeingTwoHanded.Value = currentLeftHandWeaponID.Value;
                isTwoHandingWeapon.Value = true;
            }

            player.playerInventoryManager.currentTwoHandWeapon = player.playerInventoryManager.currentLeftHandWeapon;
            player.playerEquipmentManager.TwoHandLeftWeapon();
        }

        public void OnHeadEquipmentChanged(int oldValue, int newValue)
        {
            if (IsOwner)
                return;

            HeadEquipmentItem equipment = WorldItemDatabase.Instance.GetHeadEquipmentById(headEquipmentID.Value);

            if(equipment != null)
            {
                player.playerEquipmentManager.LoadHeadEquipment(Instantiate(equipment));
            }
            else
            {
                player.playerEquipmentManager.LoadHeadEquipment(null);
            }
        }

        public void OnBodyEquipmentChanged(int oldValue, int newValue)
        {
            if (IsOwner)
                return;

            BodyEquipmentItem equipment = WorldItemDatabase.Instance.GetBodyEquipmentById(bodyEquipmentID.Value);

            if (equipment != null)
            {
                player.playerEquipmentManager.LoadBodyEquipment(Instantiate(equipment));
            }
            else
            {
                player.playerEquipmentManager.LoadBodyEquipment(null);
            }
        }

        public void OnLegEquipmentChanged(int oldValue, int newValue)
        {
            if (IsOwner)
                return;

            LegEquipmentItem equipment = WorldItemDatabase.Instance.GetLegEquipmentById(legEquipmentID.Value);

            if (equipment != null)
            {
                player.playerEquipmentManager.LoadLegEquipment(Instantiate(equipment));
            }
            else
            {
                player.playerEquipmentManager.LoadLegEquipment(null);
            }
        }

        public void OnHandEquipmentChanged(int oldValue, int newValue)
        {
            if (IsOwner)
                return;

            HandEquipmentItem equipment = WorldItemDatabase.Instance.GetHandEquipmentById(handEquipmentID.Value);

            if (equipment != null)
            {
                player.playerEquipmentManager.LoadHandEquipment(Instantiate(equipment));
            }
            else
            {
                player.playerEquipmentManager.LoadHandEquipment(null);
            }
        }

        [ServerRpc]
        public void NotifyTheServerOfWeaponActionServerRpc(ulong clientId, int actionID, int weaponID)
        {
            if (IsServer)
            {
                NotifyTheServerOfWeaponActionClientRpc(clientId, actionID, weaponID);
            }
        }

        [ClientRpc]
        private void NotifyTheServerOfWeaponActionClientRpc(ulong clientId, int actionID, int weaponID)
        {
            if (clientId != NetworkManager.Singleton.LocalClientId)
            {
                PerformWeaponBasedAction(actionID, weaponID);
            }
        }

        private void PerformWeaponBasedAction(int actionID, int weaponID)
        {
            WeaponItemActions weaponAction = WorldActionManager.instance.GetWeaponItemActionByID(actionID);

            if (weaponAction != null)
            {
                weaponAction.AttempToPerformAction(player, WorldItemDatabase.Instance.GetWeaponById(weaponID));
            }
            else
            {
                Debug.LogError("ACTION IS NULL, NO SE PUEDEN REALIZAR LAS ACCIONES");
            }
        }
    }
}
