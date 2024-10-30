using UnityEngine;

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
    }
}
