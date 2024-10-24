using UnityEngine;

namespace KC
{
    public class PlayerEquipmentManager : CharacterEquipmentManager
    {
        PlayerManager player;
        [Header("Weapon Model Instantiation Slot")]
        public WeaponModelInstantiationSlot rightHandWeaponSlot;
        public WeaponModelInstantiationSlot rightHandShieldSlot;
        public WeaponModelInstantiationSlot leftHandWeaponSlot;
        public WeaponModelInstantiationSlot leftHandShieldSlot;
        public WeaponModelInstantiationSlot backSlot;


        [Header("Weapon Models")]
        public GameObject rightHandWeaponModel;
        public GameObject leftHandWeaponModel;

        [Header("Weapons Managers")]
        [SerializeField] WeaponManager rightWeaponManager;
        [SerializeField] WeaponManager leftWeaponManager;

        protected override void Awake()
        {
            base.Awake();

            player = GetComponent<PlayerManager>();

            InitializeWeaponSlot();
        }
        protected override void Start()
        {
            base.Start();

            LoadWeaponsOnBothHands();
        }
        private void InitializeWeaponSlot()
        {
            WeaponModelInstantiationSlot[] weaponSlots = GetComponentsInChildren<WeaponModelInstantiationSlot>();
            foreach (var weaponSlot in weaponSlots)
            {
                if (weaponSlot.weaponSlot  == WeaponModelSlot.RightHandWeaponSlot)
                {
                    rightHandWeaponSlot = weaponSlot;
                }
                else if (weaponSlot.weaponSlot == WeaponModelSlot.RightHandShieldSlot)
                {
                    rightHandShieldSlot = weaponSlot;
                }
                else if (weaponSlot.weaponSlot == WeaponModelSlot.LeftHandWeaponSlot)
                {
                    leftHandWeaponSlot = weaponSlot;
                }
                else if (weaponSlot.weaponSlot == WeaponModelSlot.LeftHandShieldSlot)
                {
                    leftHandShieldSlot = weaponSlot;
                }
                else if (weaponSlot.weaponSlot == WeaponModelSlot.BackSlot)
                {
                    backSlot = weaponSlot;
                }
            }
        }
        public void LoadWeaponsOnBothHands()
        {
            LoadRightWeapon();
            LoadLeftWeapon();
        }

        //Right Weapon
        public void SwitchRightWeapon()
        {
            if (!player.IsOwner)
                return;

            //Esto esta hecho para que no pueda cambiar de arma mientras el jugador esté muerto MMC
            if (player.isDead.Value)
                return;

            player.playerAnimatorManager.PlayerTargetActionAnimation("Swap_Right_Weapon_01", false, false, true, true);

            WeaponItem selectedWeapon = null;

            player.playerInventoryManager.rightHandWeaponIndex += 1;

            //si nuestro indice esta fuera del rango lo devolvemos a 0 para no causar errores
            if (player.playerInventoryManager.rightHandWeaponIndex < 0 || player.playerInventoryManager.rightHandWeaponIndex > 2)
            {
                player.playerInventoryManager.rightHandWeaponIndex = 0;

                //Verificamos is tenemos más de un arma
                float weaponCount = 0;
                WeaponItem firstWeapon = null;
                int firstWeaponPosition = 0;

                for (int i = 0; i < player.playerInventoryManager.weaponsRightHandSlots.Length; i++)
                {
                    if (player.playerInventoryManager.weaponsRightHandSlots[i].itemID != WorldItemDatabase.Instance.unarmedWeapon.itemID)
                    {
                        weaponCount += 1;
                        if (firstWeapon == null)
                        {
                            firstWeapon = player.playerInventoryManager.weaponsRightHandSlots[i];
                            firstWeaponPosition = i;
                        }
                    }
                }
                if (weaponCount <= 1)
                {
                    player.playerInventoryManager.rightHandWeaponIndex = -1;
                    selectedWeapon = WorldItemDatabase.Instance.unarmedWeapon;
                    player.playerNetworkManager.currentRightHandWeaponID.Value = selectedWeapon.itemID;
                }
                else
                {
                    player.playerInventoryManager.rightHandWeaponIndex = firstWeaponPosition;
                    player.playerNetworkManager.currentRightHandWeaponID.Value = firstWeapon.itemID;
                }
                return;
            }

            foreach (WeaponItem weapon in player.playerInventoryManager.weaponsRightHandSlots)
            {
                //Verificamos si el arma que esta en nuestra mano derecha es igual al arma desarmada

                //Si el arma no es igual al arma desarmada entonces procedemos a verificar si tiene el mismomo numero de identificacion
                if (player.playerInventoryManager.weaponsRightHandSlots[player.playerInventoryManager.rightHandWeaponIndex].itemID != WorldItemDatabase.Instance.unarmedWeapon.itemID)
                {
                    selectedWeapon = player.playerInventoryManager.weaponsRightHandSlots[player.playerInventoryManager.rightHandWeaponIndex];

                    //Asignar el id del arma de la Red para todos los clientes conectados
                    player.playerNetworkManager.currentRightHandWeaponID.Value = player.playerInventoryManager.weaponsRightHandSlots[player.playerInventoryManager.rightHandWeaponIndex].itemID;
                    return;
                }
            }
            if (selectedWeapon == null && player.playerInventoryManager.rightHandWeaponIndex <= 2)
            {
                SwitchRightWeapon();
            }
        }
        public void LoadRightWeapon()
        {
            if (player.playerInventoryManager.currentRightHandWeapon != null)
            {
                //Removemos el arma antigua
                if (rightHandWeaponSlot.currentWeaponModel != null)
                    rightHandWeaponSlot.UnloadWeapon();

                if (rightHandShieldSlot.currentWeaponModel != null)
                    rightHandShieldSlot.UnloadWeapon();

                //Trae la nueva arma
                rightHandWeaponModel = Instantiate(player.playerInventoryManager.currentRightHandWeapon.weaponModel);

                switch (player.playerInventoryManager.currentRightHandWeapon.weaponModelType)
                {
                    case WeaponModelType.Weapon:
                        rightHandWeaponSlot.PlaceWeaponModelIntoSlot(rightHandWeaponModel);
                        break;
                    case WeaponModelType.Shield:
                        rightHandShieldSlot.PlaceWeaponModelIntoSlot(rightHandWeaponModel);
                        break;
                    default:
                        break;
                }

                
                rightWeaponManager = rightHandWeaponModel.GetComponent<WeaponManager>();
                rightWeaponManager.SetWeaponDamage(player, player.playerInventoryManager.currentRightHandWeapon);
                player.playerAnimatorManager.updateAnimatorController(player.playerInventoryManager.currentRightHandWeapon.weaponAnimator);
            }
        }

        //Left Weapon
        public void SwitchLeftWeapon()
        {
            if (!player.IsOwner)
                return;

            //Esto esta hecho para que no pueda cambiar de arma mientras el jugador esté muerto MMC
            if (player.isDead.Value)
                return;

            player.playerAnimatorManager.PlayerTargetActionAnimation("Swap_Left_Weapon_01", false, false, true, true);

            WeaponItem selectedWeapon = null;

            player.playerInventoryManager.leftHandWeaponIndex += 1;

            //si nuestro indice esta fuera del rango lo devolvemos a 0 para no causar errores
            if (player.playerInventoryManager.leftHandWeaponIndex < 0 || player.playerInventoryManager.leftHandWeaponIndex > 2)
            {
                player.playerInventoryManager.leftHandWeaponIndex = 0;

                //Verificamos is tenemos más de un arma
                float weaponCount = 0;
                WeaponItem firstWeapon = null;
                int firstWeaponPosition = 0;

                for (int i = 0; i < player.playerInventoryManager.weaponsLeftHandSlots.Length; i++)
                {
                    if (player.playerInventoryManager.weaponsLeftHandSlots[i].itemID != WorldItemDatabase.Instance.unarmedWeapon.itemID)
                    {
                        weaponCount += 1;
                        if (firstWeapon == null)
                        {
                            firstWeapon = player.playerInventoryManager.weaponsLeftHandSlots[i];
                            firstWeaponPosition = i;
                        }
                    }
                }
                if (weaponCount <= 1)
                {
                    player.playerInventoryManager.leftHandWeaponIndex = -1;
                    selectedWeapon = WorldItemDatabase.Instance.unarmedWeapon;
                    player.playerNetworkManager.currentLeftHandWeaponID.Value = selectedWeapon.itemID;
                }
                else
                {
                    player.playerInventoryManager.leftHandWeaponIndex = firstWeaponPosition;
                    player.playerNetworkManager.currentLeftHandWeaponID.Value = firstWeapon.itemID;
                }
                return;
            }

            foreach (WeaponItem weapon in player.playerInventoryManager.weaponsLeftHandSlots)
            {
                //Verificamos si el arma que esta en nuestra mano derecha es igual al arma desarmada

                //Si el arma no es igual al arma desarmada entonces procedemos a verificar si tiene el mismomo numero de identificacion
                if (player.playerInventoryManager.weaponsLeftHandSlots[player.playerInventoryManager.leftHandWeaponIndex].itemID != WorldItemDatabase.Instance.unarmedWeapon.itemID)
                {
                    selectedWeapon = player.playerInventoryManager.weaponsLeftHandSlots[player.playerInventoryManager.leftHandWeaponIndex];

                    //Asignar el id del arma de la Red para todos los clientes conectados
                    player.playerNetworkManager.currentLeftHandWeaponID.Value = player.playerInventoryManager.weaponsLeftHandSlots[player.playerInventoryManager.leftHandWeaponIndex].itemID;
                    return;
                }
            }
            if (selectedWeapon == null && player.playerInventoryManager.leftHandWeaponIndex <= 2)
            {
                SwitchLeftWeapon();
            }
        }
        public void LoadLeftWeapon()
        {
            if (player.playerInventoryManager.currentLeftHandWeapon != null)
            {
                //Removemos el arma antigua
                if (leftHandWeaponSlot.currentWeaponModel != null)
                    leftHandWeaponSlot.UnloadWeapon();

                if (leftHandShieldSlot.currentWeaponModel != null)
                    leftHandShieldSlot.UnloadWeapon();

                //Trae la nueva arma
                leftHandWeaponModel = Instantiate(player.playerInventoryManager.currentLeftHandWeapon.weaponModel);

                switch (player.playerInventoryManager.currentLeftHandWeapon.weaponModelType)
                {
                    case WeaponModelType.Weapon:
                        leftHandWeaponSlot.PlaceWeaponModelIntoSlot(leftHandWeaponModel);
                        break;
                    case WeaponModelType.Shield:
                        leftHandShieldSlot.PlaceWeaponModelIntoSlot(leftHandWeaponModel);
                        break;
                    default:
                        break;
                }

                leftWeaponManager = leftHandWeaponModel.GetComponent<WeaponManager>();
                leftWeaponManager.SetWeaponDamage(player, player.playerInventoryManager.currentLeftHandWeapon);
            }
        }

        //Two Hand
        public void UnTwoHandWeapon()
        {
            player.playerAnimatorManager.updateAnimatorController(player.playerInventoryManager.currentRightHandWeapon.weaponAnimator);

            //Left Hand
            if(player.playerInventoryManager.currentLeftHandWeapon.weaponModelType == WeaponModelType.Weapon)
            {
                leftHandWeaponSlot.PlaceWeaponModelIntoSlot(leftHandWeaponModel);
            }
            else if (player.playerInventoryManager.currentLeftHandWeapon.weaponModelType == WeaponModelType.Shield)
            {
                leftHandShieldSlot.PlaceWeaponModelIntoSlot(leftHandWeaponModel);
            }

            //Right Hand
            rightHandWeaponSlot.PlaceWeaponModelIntoSlot(rightHandWeaponModel);

            rightWeaponManager.SetWeaponDamage(player, player.playerInventoryManager.currentRightHandWeapon);
            leftWeaponManager.SetWeaponDamage(player, player.playerInventoryManager.currentLeftHandWeapon);
        }
        public void TwoHandRightWeapon()
        {
            if (player.playerInventoryManager.currentRightHandWeapon == WorldItemDatabase.Instance.unarmedWeapon)
            {
                if (player.IsOwner)
                {
                    player.playerNetworkManager.isTwoHandingRightWeapon.Value = false;
                    player.playerNetworkManager.isTwoHandingWeapon.Value = false;
                }

                return;
            }

            player.playerAnimatorManager.updateAnimatorController(player.playerInventoryManager.currentRightHandWeapon.weaponAnimator);

            backSlot.PlaceWeaponModelInUnequipedSlot(leftHandWeaponModel, player.playerInventoryManager.currentLeftHandWeapon.weaponClass, player);

            rightHandWeaponSlot.PlaceWeaponModelIntoSlot(rightHandWeaponModel);

            rightWeaponManager.SetWeaponDamage(player, player.playerInventoryManager.currentRightHandWeapon);
            leftWeaponManager.SetWeaponDamage(player, player.playerInventoryManager.currentLeftHandWeapon);
        }
        public void TwoHandLeftWeapon()
        {
            if (player.playerInventoryManager.currentLeftHandWeapon == WorldItemDatabase.Instance.unarmedWeapon)
            {
                if (player.IsOwner)
                {
                    player.playerNetworkManager.isTwoHandingLeftWeapon.Value = false;
                    player.playerNetworkManager.isTwoHandingWeapon.Value = false;
                }

                return;
            }

            player.playerAnimatorManager.updateAnimatorController(player.playerInventoryManager.currentLeftHandWeapon.weaponAnimator);

            backSlot.PlaceWeaponModelInUnequipedSlot(rightHandWeaponModel, player.playerInventoryManager.currentRightHandWeapon.weaponClass, player);

            rightHandWeaponSlot.PlaceWeaponModelIntoSlot(leftHandWeaponModel);

            rightWeaponManager.SetWeaponDamage(player, player.playerInventoryManager.currentRightHandWeapon);
            leftWeaponManager.SetWeaponDamage(player, player.playerInventoryManager.currentLeftHandWeapon);
        }
        
        //Damage Colliders
        public void OpenDamageCollider()
        {
            //Open Right Weapon damage collider
            if (player.playerNetworkManager.isUsingRightHand.Value)
            {
                rightWeaponManager.meleeDamageCollider.EnableDamageCollider();
                player.characterSoundFXManager.PlaySoundFX(WorldSoundFXManager.instance.ChooseRandomSFXFromArray(player.playerInventoryManager.currentRightHandWeapon.whooshes));
            }
            //Open Left Weapon damage collider
            else if (player.playerNetworkManager.isUsingLeftHand.Value)
            {
                leftWeaponManager.meleeDamageCollider.EnableDamageCollider();
                player.characterSoundFXManager.PlaySoundFX(WorldSoundFXManager.instance.ChooseRandomSFXFromArray(player.playerInventoryManager.currentLeftHandWeapon.whooshes));
            }

            //Play whoosh Sfx
        }

        public void CloseDamageCollider()
        {
            //Open Right Weapon damage collider
            if (player.playerNetworkManager.isUsingRightHand.Value)
            {
                rightWeaponManager.meleeDamageCollider.DisableDamageCollider();
            }
            //Open Left Weapon damage collider
            else if (player.playerNetworkManager.isUsingLeftHand.Value)
            {
                leftWeaponManager.meleeDamageCollider.DisableDamageCollider();
            }

            //Play whoosh Sfx
        }
    }
}
