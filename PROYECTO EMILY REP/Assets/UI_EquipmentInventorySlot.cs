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
            WeaponItem currentWeapon;

            switch (PlayerUIManager.instance.playerUIEquipmentManager.currentSelectedEquipmentSlot)
            {
                case EquipmentType.RightWeapon01:
                    currentWeapon = player.playerInventoryManager.weaponsRightHandSlots[0];

                    if (currentWeapon.itemID != WorldItemDatabase.Instance.unarmedWeapon.itemID)
                    {
                        player.playerInventoryManager.AddItemsToInventory(currentWeapon);
                    }
                    player.playerInventoryManager.weaponsRightHandSlots[0] = currentItem as WeaponItem;
                    player.playerInventoryManager.RemoveItemsFromInventory(currentItem);

                    //Equipar una nueva arma si tenemos el arma actual en esta ranura
                    if (player.playerInventoryManager.rightHandWeaponIndex == 0)
                        player.playerNetworkManager.currentRightHandWeaponID.Value = currentItem.itemID;

                    //Refrescamos la vista
                    PlayerUIManager.instance.playerUIEquipmentManager.RefreshMenu();

                    break;
                case EquipmentType.RightWeapon02:
                    currentWeapon = player.playerInventoryManager.weaponsRightHandSlots[1];

                    if (currentWeapon.itemID != WorldItemDatabase.Instance.unarmedWeapon.itemID)
                    {
                        player.playerInventoryManager.AddItemsToInventory(currentWeapon);
                    }
                    player.playerInventoryManager.weaponsRightHandSlots[1] = currentItem as WeaponItem;
                    player.playerInventoryManager.RemoveItemsFromInventory(currentItem);

                    //Equipar una nueva arma si tenemos el arma actual en esta ranura
                    if (player.playerInventoryManager.rightHandWeaponIndex == 1)
                        player.playerNetworkManager.currentRightHandWeaponID.Value = currentItem.itemID;

                    //Refrescamos la vista
                    PlayerUIManager.instance.playerUIEquipmentManager.RefreshMenu();
                    break;
                case EquipmentType.RightWeapon03:
                    currentWeapon = player.playerInventoryManager.weaponsRightHandSlots[2];

                    if (currentWeapon.itemID != WorldItemDatabase.Instance.unarmedWeapon.itemID)
                    {
                        player.playerInventoryManager.AddItemsToInventory(currentWeapon);
                    }
                    player.playerInventoryManager.weaponsRightHandSlots[2] = currentItem as WeaponItem;
                    player.playerInventoryManager.RemoveItemsFromInventory(currentItem);

                    //Equipar una nueva arma si tenemos el arma actual en esta ranura
                    if (player.playerInventoryManager.rightHandWeaponIndex == 2)
                        player.playerNetworkManager.currentRightHandWeaponID.Value = currentItem.itemID;

                    //Refrescamos la vista
                    PlayerUIManager.instance.playerUIEquipmentManager.RefreshMenu();

                    break;
                case EquipmentType.LeftWeapon01:
                    currentWeapon = player.playerInventoryManager.weaponsLeftHandSlots[0];

                    if (currentWeapon.itemID != WorldItemDatabase.Instance.unarmedWeapon.itemID)
                    {
                        player.playerInventoryManager.AddItemsToInventory(currentWeapon);
                    }
                    player.playerInventoryManager.weaponsLeftHandSlots[0] = currentItem as WeaponItem;
                    player.playerInventoryManager.RemoveItemsFromInventory(currentItem);

                    //Equipar una nueva arma si tenemos el arma actual en esta ranura
                    if (player.playerInventoryManager.leftHandWeaponIndex == 0)
                        player.playerNetworkManager.currentLeftHandWeaponID.Value = currentItem.itemID;

                    //Refrescamos la vista
                    PlayerUIManager.instance.playerUIEquipmentManager.RefreshMenu();


                    break;
                case EquipmentType.LeftWeapon02:
                    currentWeapon = player.playerInventoryManager.weaponsLeftHandSlots[1];

                    if (currentWeapon.itemID != WorldItemDatabase.Instance.unarmedWeapon.itemID)
                    {
                        player.playerInventoryManager.AddItemsToInventory(currentWeapon);
                    }
                    player.playerInventoryManager.weaponsLeftHandSlots[1] = currentItem as WeaponItem;
                    player.playerInventoryManager.RemoveItemsFromInventory(currentItem);

                    //Equipar una nueva arma si tenemos el arma actual en esta ranura
                    if (player.playerInventoryManager.leftHandWeaponIndex == 1)
                        player.playerNetworkManager.currentLeftHandWeaponID.Value = currentItem.itemID;

                    //Refrescamos la vista
                    PlayerUIManager.instance.playerUIEquipmentManager.RefreshMenu();
                    break;
                case EquipmentType.LeftWeapon03:
                    currentWeapon = player.playerInventoryManager.weaponsLeftHandSlots[2];

                    if (currentWeapon.itemID != WorldItemDatabase.Instance.unarmedWeapon.itemID)
                    {
                        player.playerInventoryManager.AddItemsToInventory(currentWeapon);
                    }
                    player.playerInventoryManager.weaponsLeftHandSlots[2] = currentItem as WeaponItem;
                    player.playerInventoryManager.RemoveItemsFromInventory(currentItem);

                    //Equipar una nueva arma si tenemos el arma actual en esta ranura
                    if (player.playerInventoryManager.leftHandWeaponIndex == 2)
                        player.playerNetworkManager.currentLeftHandWeaponID.Value = currentItem.itemID;

                    //Refrescamos la vista
                    PlayerUIManager.instance.playerUIEquipmentManager.RefreshMenu();
                    break;
                default:
                    break;
            }

            PlayerUIManager.instance.playerUIEquipmentManager.SelectLastSelectedEquipmentSlot();
        }
    }
}
