using UnityEngine;

namespace KC
{
    public class WeaponModelInstantiationSlot : MonoBehaviour
    {
        public WeaponModelSlot weaponSlot;
        //que Slot es? (Derecha o izquierda)
        public GameObject currentWeaponModel;

        public void UnloadWeapon()
        {
            if (currentWeaponModel != null )
            {
                Destroy( currentWeaponModel );
            }
        }

        public void PlaceWeaponModelIntoSlot(GameObject weaponModel)
        {
            currentWeaponModel = weaponModel;
            weaponModel.transform.parent = transform;

            weaponModel.transform.localPosition = Vector3.zero;
            weaponModel.transform.localRotation = Quaternion.identity;
            weaponModel.transform.localScale = Vector3.one;

        }

        public void PlaceWeaponModelInUnequipedSlot(GameObject weaponModel, WeaponClass weaponClass, PlayerManager player)
        {
            currentWeaponModel = weaponModel;
            weaponModel.transform.parent = transform;

            switch (weaponClass)
            {
                case WeaponClass.StraightSword:
                    weaponModel.transform.localPosition = new Vector3(-0.255f, 0.056f, -0.2f);
                    weaponModel.transform.localRotation = Quaternion.Euler(-168.3f, -108f, 32.4f);
                    break;
                case WeaponClass.MediumShield:
                    weaponModel.transform.localPosition = new Vector3(0.063f, 0.015f, 0.002f);
                    weaponModel.transform.localRotation = Quaternion.Euler(-271.5f, 183.6f, 279.5f);
                    break;
                default:
                    break;
            }
        }
    }
}
