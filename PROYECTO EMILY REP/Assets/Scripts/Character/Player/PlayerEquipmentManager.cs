using UnityEngine;
using System.Collections.Generic;
namespace KC
{
    public class PlayerEquipmentManager : CharacterEquipmentManager
    {
        PlayerManager player;
        [Header("Weapon Model Instantiation Slot")]
        [HideInInspector] public WeaponModelInstantiationSlot rightHandWeaponSlot;
        [HideInInspector] public WeaponModelInstantiationSlot rightHandShieldSlot;
        [HideInInspector] public WeaponModelInstantiationSlot leftHandWeaponSlot;
        [HideInInspector] public WeaponModelInstantiationSlot leftHandShieldSlot;
        [HideInInspector] public WeaponModelInstantiationSlot backSlot;


        [Header("Weapon Models")]
        [HideInInspector] public GameObject rightHandWeaponModel;
        [HideInInspector] public GameObject leftHandWeaponModel;

        [Header("Weapons Managers")]
        WeaponManager rightWeaponManager;
        WeaponManager leftWeaponManager;

        [Header("Debug delete later")]
        [SerializeField] bool equipNewItems = false;

        #region General Equipment
        [Header("General Equipments Models")]
        public GameObject hatsObject;
        [HideInInspector] public GameObject[] hats;
        public GameObject hoodsObject;
        [HideInInspector] public GameObject[] hoods;
        public GameObject faceCoverObject;
        [HideInInspector] public GameObject[] faceCovers;
        public GameObject helmetAccesoriesObject;
        [HideInInspector] public GameObject[] helmetAccesories;
        public GameObject backAccesoriesObject;
        [HideInInspector] public GameObject[] backAccesories;
        public GameObject hipAccesoriesObject;
        [HideInInspector] public GameObject[] hipAccesories;
        public GameObject rightShoulderObject;
        [HideInInspector] public GameObject[] rightShoulder;
        public GameObject rightElbowObject;
        [HideInInspector] public GameObject[] rightElbow;
        public GameObject rightKneeObject;
        [HideInInspector] public GameObject[] rightKnee;
        public GameObject lefttShoulderObject;
        [HideInInspector] public GameObject[] leftShoulder;
        public GameObject leftElbowObject;
        [HideInInspector] public GameObject[] leftElbow;
        public GameObject leftKneeObject;
        [HideInInspector] public GameObject[] leftKnee;
        #endregion

        #region Male Equipments
        [Header("Male Equipment Models")]
        public GameObject maleFullHelmetObject;
        [HideInInspector] public GameObject[] maleHeadFullHelmets;
        public GameObject maleFullBodyObjects;
        [HideInInspector] public GameObject[] maleBodies;
        public GameObject maleRightUpperArmObject;
        [HideInInspector] public GameObject[] maleRightUpperArms;
        public GameObject maleRightLowerArmObject;
        [HideInInspector] public GameObject[] maleRightLowerArms;
        public GameObject maleRightHandObject;
        [HideInInspector] public GameObject[] maleRightHands;
        public GameObject maleLeftUpperArmObject;
        [HideInInspector] public GameObject[] maleLeftUpperArms;
        public GameObject maleLeftLowerArmObject;
        [HideInInspector] public GameObject[] maleLefttLowerArms;
        public GameObject maleLeftHandObject;
        [HideInInspector] public GameObject[] maleLefttHands;
        public GameObject maleHipsObect;
        [HideInInspector] public GameObject[] malehips;
        public GameObject maleRightLegObject;
        [HideInInspector] public GameObject[] maleRightLegs;
        public GameObject maleLeftLegObject;
        [HideInInspector] public GameObject[] maleLeftLegs;
        #endregion

        #region Female Equipments
        [Header("Female Equipment Models")]
        public GameObject femaleFullHelmetObject;
        [HideInInspector] public GameObject[] femaleHeadFullHelmets;
        public GameObject femaleFullBodyObjects;
        [HideInInspector] public GameObject[] femaleBodies;
        public GameObject femaleRightUpperArmObject;
        [HideInInspector] public GameObject[] femaleRightUpperArms;
        public GameObject femaleRightLowerArmObject;
        [HideInInspector] public GameObject[] femaleRightLowerArms;
        public GameObject femaleRightHandObject;
        [HideInInspector] public GameObject[] femaleRightHands;
        public GameObject femaleLeftUpperArmObject;
        [HideInInspector] public GameObject[] femaleLeftUpperArms;
        public GameObject femaleLeftLowerArmObject;
        [HideInInspector] public GameObject[] femaleLefttLowerArms;
        public GameObject femaleLeftHandObject;
        [HideInInspector] public GameObject[] femaleLefttHands;
        public GameObject femaleHipsObect;
        [HideInInspector] public GameObject[] femalehips;
        public GameObject femaleRightLegObject;
        [HideInInspector] public GameObject[] femaleRightLegs;
        public GameObject femaleLeftLegObject;
        [HideInInspector] public GameObject[] femaleLeftLegs;
        #endregion
        protected override void Awake()
        {
            base.Awake();

            player = GetComponent<PlayerManager>();

            InitializeWeaponSlot();

            //HATS
            List<GameObject> hatsList = new List<GameObject>();
            foreach (Transform child in hatsObject.transform)
            {
                hatsList.Add(child.gameObject);
            }

            hats = hatsList.ToArray();

            //HOODS
            List<GameObject> hoodsList = new List<GameObject>();
            foreach (Transform child in hoodsObject.transform)
            {
                hoodsList.Add(child.gameObject);
            }

            hoods = hoodsList.ToArray();

            //FACE COVERS
            List<GameObject> faceCoverList = new List<GameObject>();
            foreach (Transform child in faceCoverObject.transform)
            {
                faceCoverList.Add(child.gameObject);
            }

            faceCovers = faceCoverList.ToArray();

            //HELMET ACCESORIES
            List<GameObject> helmetAccesoriesList = new List<GameObject>();
            foreach (Transform child in helmetAccesoriesObject.transform)
            {
                helmetAccesoriesList.Add(child.gameObject);
            }

            helmetAccesories = helmetAccesoriesList.ToArray();

            //BACK ACCESORIES
            List<GameObject> backAccesoriesList = new List<GameObject>();
            foreach (Transform child in backAccesoriesObject.transform)
            {
                backAccesoriesList.Add(child.gameObject);
            }

            backAccesories = backAccesoriesList.ToArray();

            //HIP ACCESORIES
            List<GameObject> hipAccesoriesList = new List<GameObject>();
            foreach (Transform child in hipAccesoriesObject.transform)
            {
                hipAccesoriesList.Add(child.gameObject);
            }

            hipAccesories = hipAccesoriesList.ToArray();

            //RIGHT SHOULDERS
            List<GameObject> rightShoulderList =new List<GameObject>();
            foreach (Transform child in rightShoulderObject.transform)
            {
                rightShoulderList.Add(child.gameObject);
            }

            rightShoulder = rightShoulderList.ToArray();

            //RIGHT ELBOW
            List<GameObject> rightElbowList = new List<GameObject>();
            foreach (Transform child in rightElbowObject.transform)
            {
                rightElbowList.Add(child.gameObject);
            }

            rightElbow = rightElbowList.ToArray();

            //RIGHT KNEE
            List<GameObject> rightKneeList = new List<GameObject>();
            foreach (Transform child in rightKneeObject.transform)
            {
                rightKneeList.Add(child.gameObject);
            }

            rightKnee = rightKneeList.ToArray();

            //LEFT SHOULDERS
            List<GameObject> leftShoulderList = new List<GameObject>();
            foreach (Transform child in lefttShoulderObject.transform)
            {
                leftShoulderList.Add(child.gameObject);
            }

            leftShoulder = leftShoulderList.ToArray();

            //LEFT ELBOW
            List<GameObject> leftElbowList = new List<GameObject>();
            foreach (Transform child in leftElbowObject.transform)
            {
                leftElbowList.Add(child.gameObject);
            }

            leftElbow = leftElbowList.ToArray();

            //LEFT KNEE
            List<GameObject> leftKneeList = new List<GameObject>();
            foreach (Transform child in leftKneeObject.transform)
            {
                leftKneeList.Add(child.gameObject);
            }

            leftKnee = leftKneeList.ToArray();

            //MALE FULL HELMET
            List<GameObject> maleFullHelmetsList = new List<GameObject>();
            foreach (Transform child in maleFullHelmetObject.transform)
            {
                maleFullHelmetsList.Add(child.gameObject);
            }

            maleHeadFullHelmets = maleFullHelmetsList.ToArray();

            //MALE FULL BODY
            List<GameObject> malebodiesList = new List<GameObject>();
            foreach (Transform child in maleFullBodyObjects.transform)
            {
                malebodiesList.Add(child.gameObject);
            }

            maleBodies = malebodiesList.ToArray();

            //MALE RIGHT UPPER ARM
            List<GameObject> maleRightUpperArmsList = new List<GameObject>();
            foreach (Transform child in maleRightUpperArmObject.transform)
            {
                maleRightUpperArmsList.Add(child.gameObject);
            }

            maleRightUpperArms = maleRightUpperArmsList.ToArray();

            //MALE RIGHT LOWER ARM
            List<GameObject> maleRightLowerArmsList = new List<GameObject>();
            foreach (Transform child in maleRightLowerArmObject.transform)
            {
                maleRightLowerArmsList.Add(child.gameObject);
            }

            maleRightLowerArms = maleRightLowerArmsList.ToArray();

            //MALE RIGHT HAND
            List<GameObject> maleRightHandsList = new List<GameObject>();
            foreach (Transform child in maleRightHandObject.transform)
            {
                maleRightHandsList.Add(child.gameObject);
            }

            maleRightHands = maleRightHandsList.ToArray();

            //MALE LEFT UPPER ARM
            List<GameObject> maleLeftUpperArmsList = new List<GameObject>();
            foreach (Transform child in maleLeftUpperArmObject.transform)
            {
                maleLeftUpperArmsList.Add(child.gameObject);
            }

            maleLeftUpperArms = maleLeftUpperArmsList.ToArray();

            //MALE LEFT LOWER ARM
            List<GameObject> maleLefttLowerArmsList = new List<GameObject>();
            foreach (Transform child in maleLeftLowerArmObject.transform)
            {
                maleLefttLowerArmsList.Add(child.gameObject);
            }

            maleLefttLowerArms = maleLefttLowerArmsList.ToArray();

            //MALE LEFT HAND
            List<GameObject> maleLefttHandsList = new List<GameObject>();
            foreach (Transform child in maleLeftHandObject.transform)
            {
                maleLefttHandsList.Add(child.gameObject);
            }

            maleLefttHands = maleLefttHandsList.ToArray();

            //MALE HIPS
            List<GameObject> maleHipsList = new List<GameObject>();
            foreach (Transform child in maleHipsObect.transform)
            {
                maleHipsList.Add(child.gameObject);
            }

            malehips = maleHipsList.ToArray();

            //MALE RIGHT LEG
            List<GameObject> maleRightLegsList = new List<GameObject>();
            foreach (Transform child in maleRightLegObject.transform)
            {
                maleRightLegsList.Add(child.gameObject);
            }
            maleRightLegs = maleRightLegsList.ToArray();

            //MALE RIGHT LEG
            List<GameObject> maleLeftLegsList = new List<GameObject>();
            foreach (Transform child in maleLeftLegObject.transform)
            {
                maleLeftLegsList.Add(child.gameObject);
            }

            maleLeftLegs = maleLeftLegsList.ToArray();

            //FEMALE FULL HELMETS
            List<GameObject> femaleHeadFullHelmetsList = new List<GameObject>();
            foreach (Transform child in femaleFullHelmetObject.transform)
            {
                femaleHeadFullHelmetsList.Add(child.gameObject);
            }

            femaleHeadFullHelmets = femaleHeadFullHelmetsList.ToArray();
            //FEMALE FULL BODY
            List<GameObject> femaleBodiesList = new List<GameObject>();
            foreach (Transform child in femaleFullBodyObjects.transform)
            {
                femaleBodiesList.Add(child.gameObject);
            }

            femaleBodies = femaleBodiesList.ToArray();

            //FEMALE RIGHT UPPER ARM
            List<GameObject> femaleRightUpperArmsList = new List<GameObject>();
            foreach (Transform child in femaleRightUpperArmObject.transform)
            {
                femaleRightUpperArmsList.Add(child.gameObject);
            }

            femaleRightUpperArms = femaleRightUpperArmsList.ToArray();

            //FEMALE RIGHT LOWER
            List<GameObject> femaleRightLowerArmsList = new List<GameObject>();
            foreach (Transform child in femaleRightLowerArmObject.transform)
            {
                femaleRightLowerArmsList.Add(child.gameObject);
            }

            femaleRightLowerArms = femaleRightLowerArmsList.ToArray();

            //FEMALE RIGHT HANDS
            List<GameObject> femaleRightHandsList = new List<GameObject>();
            foreach (Transform child in femaleRightHandObject.transform)
            {
                femaleRightHandsList.Add(child.gameObject);
            }

            femaleRightHands = femaleRightHandsList.ToArray();

            //FEMALE LEFT UPPER ARM
            List<GameObject> femaleLeftUpperArmsList = new List<GameObject>();
            foreach (Transform child in femaleLeftUpperArmObject.transform)
            {
                femaleLeftUpperArmsList.Add(child.gameObject);
            }

            femaleLeftUpperArms = femaleLeftUpperArmsList.ToArray();

            //FEMALE LEFT LOWER ARM
            List<GameObject> femaleLefttLowerArmsList = new List<GameObject>();
            foreach (Transform child in femaleLeftLowerArmObject.transform)
            {
                femaleLefttLowerArmsList.Add(child.gameObject);
            }

            femaleLefttLowerArms = femaleLefttLowerArmsList.ToArray();

            //FEMALE LEFT HANDS
            List<GameObject> femaleLefttHandsList = new List<GameObject>();
            foreach (Transform child in femaleLeftHandObject.transform)
            {
                femaleLefttHandsList.Add(child.gameObject);
            }

            femaleLefttHands = femaleLefttHandsList.ToArray();

            //FEMALE LEFT HIPS
            List<GameObject> femalehipsList = new List<GameObject>();
            foreach (Transform child in femaleHipsObect.transform)
            {
                femalehipsList.Add(child.gameObject);
            }

            femalehips = femalehipsList.ToArray();

            //FEMALE RIGHT LEG
            List<GameObject> femaleRightLegsList = new List<GameObject>();
            foreach (Transform child in femaleRightLegObject.transform)
            {
                femaleRightLegsList.Add(child.gameObject);
            }

            femaleRightLegs = femaleRightLegsList.ToArray();

            //FEMALE LEFT LEG
            List<GameObject> femaleLeftLegsList = new List<GameObject>();
            foreach (Transform child in femaleLeftLegObject.transform)
            {
                femaleLeftLegsList.Add(child.gameObject);
            }

            femaleLeftLegs = femaleLeftLegsList.ToArray();
        }

        private void Update()
        {
            if (equipNewItems)
            {
                equipNewItems = false;
                EquipArmor();
            }
        }

        public void EquipArmor()
        {
            LoadHeadEquipment(player.playerInventoryManager.headEquipmentItem);

            LoadBodyEquipment(player.playerInventoryManager.bodyEquipmentItem);

            LoadLegEquipment(player.playerInventoryManager.legEquipmentItem);

            LoadHandEquipment(player.playerInventoryManager.handEquipmentItem);
        }
        public void InitializeArmorModels()
        {

        }
        public void LoadHeadEquipment(HeadEquipmentItem equipment)
        {
            UnloadHeadEquipmentModels();

            if(equipment == null)
            {
                if (player.IsOwner)
                    player.playerNetworkManager.headEquipmentID.Value = -1;

                player.playerInventoryManager.headEquipmentItem = null;
                return;
            }

            player.playerInventoryManager.headEquipmentItem = equipment;

            switch (equipment.headEquipmentType)
            {
                case HeadEquipmentType.FullHelmet:
                    player.playerBodyManager.DisableHair();
                    player.playerBodyManager.DisableHead();
                    break;
                case HeadEquipmentType.Hat:
                    break;
                case HeadEquipmentType.Hood:
                    player.playerBodyManager.DisableHair();
                    break;
                case HeadEquipmentType.FaceCover:
                    player.playerBodyManager.DisableFacialHair();
                    break;
                default:
                    break;
            }

            foreach (var model in equipment.equipmentModels)
            {
                model.LoadModel(player, player.playerNetworkManager.isMale.Value);
            }

            player.playerStatsManager.CalculateTotalArmorAbsorption();

            if (player.IsOwner)
                player.playerNetworkManager.headEquipmentID.Value = equipment.itemID;
        }
        private void UnloadHeadEquipmentModels()
        {
            foreach (var model in maleHeadFullHelmets)
            {
                model.SetActive(false);
            }
            foreach (var model in femaleHeadFullHelmets)
            {
                model.SetActive(false);
            }
            foreach (var model in hats)
            {
                model.SetActive(false);
            }
            foreach (var model in faceCovers)
            {
                model.SetActive(false);
            }
            foreach (var model in hoods)
            {
                model.SetActive(false);
            }
            foreach (var model in helmetAccesories)
            {
                model.SetActive(false);
            }
            player.playerStatsManager.CalculateTotalArmorAbsorption();
            player.playerBodyManager.EnableHead();
            player.playerBodyManager.EnableHair();
        }
        public void LoadBodyEquipment(BodyEquipmentItem equipment)
        {
            UnloadBodyEquipmentModels();

            if (equipment == null)
            {
                if (player.IsOwner)
                    player.playerNetworkManager.bodyEquipmentID.Value = -1;

                player.playerInventoryManager.bodyEquipmentItem = null;
                return;
            }

            player.playerInventoryManager.bodyEquipmentItem = equipment;
            player.playerBodyManager.DisableBody();
            foreach (var model in equipment.equipmentModels)
            {
                model.LoadModel(player, player.playerNetworkManager.isMale.Value);
            }

            player.playerStatsManager.CalculateTotalArmorAbsorption();

            if (player.IsOwner)
                player.playerNetworkManager.bodyEquipmentID.Value = equipment.itemID;
        }
        private void UnloadBodyEquipmentModels()
        {
            foreach (var model in rightShoulder)
            {
                model.SetActive(false);
            }
            foreach (var model in rightElbow)
            {
                model.SetActive(false);
            }

            foreach (var model in leftShoulder)
            {
                model.SetActive(false);
            }
            foreach (var model in leftElbow)
            {
                model.SetActive(false);
            }

            foreach (var model in backAccesories)
            {
                model.SetActive(false);
            }
            foreach (var model in maleBodies)
            {
                model.SetActive(false);
            }

            foreach (var model in maleRightUpperArms)
            {
                model.SetActive(false);
            }
            foreach (var model in maleLeftUpperArms)
            {
                model.SetActive(false);
            }

            foreach (var model in femaleBodies)
            {
                model.SetActive(false);
            }

            foreach (var model in femaleRightUpperArms)
            {
                model.SetActive(false);
            }
            foreach (var model in femaleLeftUpperArms)
            {
                model.SetActive(false);
            }
            player.playerStatsManager.CalculateTotalArmorAbsorption();
            player.playerBodyManager.EnableBody();
        }
        public void LoadLegEquipment(LegEquipmentItem equipment)
        {
            UnloadLegEquipmentModels();

            if (equipment == null)
            {
                if (player.IsOwner)
                    player.playerNetworkManager.legEquipmentID.Value = -1;

                player.playerInventoryManager.legEquipmentItem = null;
                return;
            }

            player.playerInventoryManager.legEquipmentItem = equipment;
            player.playerBodyManager.DisableLegs();
            foreach (var model in equipment.equipmentModels)
            {
                model.LoadModel(player, player.playerNetworkManager.isMale.Value);
            }

            player.playerStatsManager.CalculateTotalArmorAbsorption();

            if (player.IsOwner)
                player.playerNetworkManager.legEquipmentID.Value = equipment.itemID;
        }
        private void UnloadLegEquipmentModels()
        {
            foreach (var model in hipAccesories)
            {
                model.SetActive(false);
            }
            foreach (var model in rightKnee)
            {
                model.SetActive(false);
            }
            foreach (var model in leftKnee)
            {
                model.SetActive(false);
            }
            foreach (var model in malehips)
            {
                model.SetActive(false);
            }
            foreach (var model in maleRightLegs)
            {
                model.SetActive(false);
            }
            foreach (var model in maleLeftLegs)
            {
                model.SetActive(false);
            }
            foreach (var model in femalehips)
            {
                model.SetActive(false);
            }
            foreach (var model in femaleRightLegs)
            {
                model.SetActive(false);
            }
            foreach (var model in femaleLeftLegs)
            {
                model.SetActive(false);
            }
            player.playerStatsManager.CalculateTotalArmorAbsorption();
            player.playerBodyManager.EnableLegs();
        }
        public void LoadHandEquipment(HandEquipmentItem equipment)
        {
            UnloadHandEquipmentModels();

            if (equipment == null)
            {
                if (player.IsOwner)
                    player.playerNetworkManager.handEquipmentID.Value = -1;

                player.playerInventoryManager.handEquipmentItem = null;
                return;
            }

            player.playerInventoryManager.handEquipmentItem = equipment;
            player.playerBodyManager.DisableHands();
            foreach (var model in equipment.equipmentModels)
            {
                
                model.LoadModel(player, player.playerNetworkManager.isMale.Value);
            }

            player.playerStatsManager.CalculateTotalArmorAbsorption();

            if (player.IsOwner)
                player.playerNetworkManager.handEquipmentID.Value = equipment.itemID;
        }
        private void UnloadHandEquipmentModels()
        {
            foreach (var model in maleRightLowerArms)
            {
                model.SetActive(false);
            }
            foreach (var model in maleRightHands)
            {
                model.SetActive(false);
            }
            foreach (var model in maleLefttLowerArms)
            {
                model.SetActive(false);
            }
            foreach (var model in maleLefttHands)
            {
                model.SetActive(false);
            }
            foreach (var model in femaleRightLowerArms)
            {
                model.SetActive(false);
            }
            foreach (var model in femaleRightHands)
            {
                model.SetActive(false);
            }
            foreach (var model in femaleLefttLowerArms)
            {
                model.SetActive(false);
            }
            foreach (var model in femaleLefttHands)
            {
                model.SetActive(false);
            }
            player.playerStatsManager.CalculateTotalArmorAbsorption();
            player.playerBodyManager.EnableHands();
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
