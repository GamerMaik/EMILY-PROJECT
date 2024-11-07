using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
namespace KC
{
    public class UI_EquipmentInventorySlot : MonoBehaviour
    {
        public Image itemIcon;
        public Image highLightedIcon;
        [SerializeField] public Item currentItem;


        public void AddItem(Item item)
        {
            if (item == null)
            {
                itemIcon.enabled = false;
                return;
            }

            itemIcon.enabled = true;

            currentItem = item;
            itemIcon.sprite = item.itemIcon;
        }

        public void SelectSlot()
        {
            highLightedIcon.enabled = true;
        }

        public void DeselectSlot()
        {
            highLightedIcon.enabled = false;
        }

        public void EquipItem()
        {
            PlayerManager player = NetworkManager.Singleton.LocalClient.PlayerObject.GetComponent<PlayerManager>();
            switch (PlayerUIManager.instance.playerUIEquipmentManager.currentSelectedEquipmentSlot)
            {
                case EquipmentType.RightWeapon01:
                    WeaponItem currentWeapon = player.playerInventoryManager.weaponsRightHandSlots[0];

                    if (currentWeapon.itemID != WorldItemDatabase.Instance.unarmedWeapon.itemID)
                    {
                        player.playerInventoryManager.AddItemsToInventory(currentWeapon, 1);
                    }
                    player.playerInventoryManager.weaponsRightHandSlots[0] = currentItem as WeaponItem;
                    player.playerInventoryManager.RemoveItemsFromInventory(currentItem);

                    //Equipar una nueva arma si tenemos el arma actual en esta ranura
                    if (player.playerInventoryManager.rightHandWeaponIndex == 0)
                        player.playerNetworkManager.currentRightHandWeaponID.Value = currentItem.itemID;

                    //Refrescamos la vista
                    PlayerUIManager.instance.playerUIEquipmentManager.OpenEquipmentManagerMenu();

                    break;
                case EquipmentType.RightWeapon02:
                    break;
                case EquipmentType.RightWeapon03:
                    break;
                case EquipmentType.LeftWeapon01:
                    break;
                case EquipmentType.LeftWeapon02:
                    break;
                case EquipmentType.LeftWeapon03:
                    break;
                default:
                    break;
            }
        }
    }
}
