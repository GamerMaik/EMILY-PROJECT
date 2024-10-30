using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace KC
{
    public class WorldItemDatabase : MonoBehaviour
    {
        public static WorldItemDatabase Instance;

        public WeaponItem unarmedWeapon;

        [Header("Weapons")]
        [SerializeField] List<WeaponItem> weapons = new List<WeaponItem>();

        [Header("Head Equipment")]
        [SerializeField] List<HeadEquipmentItem> headEquipment = new List<HeadEquipmentItem>();

        [Header("Body Equipment")]
        [SerializeField] List<BodyEquipmentItem> bodyEquipment = new List<BodyEquipmentItem>();

        [Header("Leg Equipment")]
        [SerializeField] List<LegEquipmentItem> legEquipment = new List<LegEquipmentItem>();

        [Header("Hand Equipment")]
        [SerializeField] List<HandEquipmentItem> handEquipment = new List<HandEquipmentItem>();

        [Header("Items")]
        private List<Item> items = new List<Item>();

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            foreach (var weapon in weapons)
            {
                items.Add(weapon);
            }
            foreach (var item in headEquipment)
            {
                items.Add(item);
            }
            foreach (var item in bodyEquipment)
            {
                items.Add(item);
            }
            foreach (var item in legEquipment)
            {
                items.Add(item);
            }
            foreach (var item in handEquipment)
            {
                items.Add(item);
            }

            //Se agrega un ID a cada Item por cada valor de i
            for (int i = 0; i < items.Count; i++)
            {
                items[i].itemID = i;
            }

        }

        public WeaponItem GetWeaponById(int ID)
        {
            return weapons.FirstOrDefault(weapon => weapon.itemID == ID);
        }

        public HeadEquipmentItem GetHeadEquipmentById(int ID)
        {  
            return headEquipment.FirstOrDefault(equipment => equipment.itemID == ID);
        }
        public BodyEquipmentItem GetBodyEquipmentById(int ID)
        {
            return bodyEquipment.FirstOrDefault(equipment => equipment.itemID == ID);
        }
        public LegEquipmentItem GetLegEquipmentById(int ID)
        {
            return legEquipment.FirstOrDefault(equipment => equipment.itemID == ID);
        }
        public HandEquipmentItem GetHandEquipmentById(int ID)
        {
            return handEquipment.FirstOrDefault(equipment => equipment.itemID == ID);
        }
    }
}
