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
    }
}
