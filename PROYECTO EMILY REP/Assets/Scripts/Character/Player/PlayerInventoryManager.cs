using UnityEngine;

namespace KC
{
    public class PlayerInventoryManager : CharacterInventoryManager
    {
        public WeaponItem currentRightHandWeapon;
        public WeaponItem currentLeftHandWeapon;

        [Header("QuickSlots")]
        public WeaponItem[] weaponsRightHandSlots = new WeaponItem[3];
        public int rightHandWeaponIndex = 0;

        public WeaponItem[] weaponsLeftHandSlots = new WeaponItem[3];
        public int leftHandWeaponIndex = 0;
        

    }
}
