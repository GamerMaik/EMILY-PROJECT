using UnityEngine;
using System.Collections.Generic;
namespace KC
{
    public class PlayerInventoryManager : CharacterInventoryManager
    {
        [Header("Weapons")]
        public WeaponItem currentRightHandWeapon;
        public WeaponItem currentLeftHandWeapon;
        public WeaponItem currentTwoHandWeapon;

        [Header("QuickSlots")]
        public WeaponItem[] weaponsRightHandSlots = new WeaponItem[3];
        public int rightHandWeaponIndex = 0;

        public WeaponItem[] weaponsLeftHandSlots = new WeaponItem[3];
        public int leftHandWeaponIndex = 0;

        [Header("Armor")]
        public HeadEquipmentItem headEquipmentItem;
        public BodyEquipmentItem bodyEquipmentItem;
        public LegEquipmentItem legEquipmentItem;
        public HandEquipmentItem handEquipmentItem;

        [Header("Inventory")]
        public List<Item> itemsInventory;

        public void AddItemsToInventory(Item item, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                itemsInventory.Add(item); 
            }
        }
        public void AddItemsToInventory(Item item)
        {
            itemsInventory.Add(item);
        }

        public void RemoveItemsFromInventory(Item item)
        {
            itemsInventory.Remove(item);

            for (int i = itemsInventory.Count - 1 ; i > -1; i--)
            {
                if (itemsInventory[i] == null)
                {
                    itemsInventory.RemoveAt(i);
                }
            }
        }
    }
}
