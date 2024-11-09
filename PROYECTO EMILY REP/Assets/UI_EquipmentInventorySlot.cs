using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
namespace KC
{
    public class UI_EquipmentInventorySlot : MonoBehaviour
    {
        PlayerUIEquipmentManager playerUIEquipmentManager;
        public Image itemIcon;
        public Image highLightedIcon;
        [SerializeField] public Item currentItem;

        private void Awake()
        {
            playerUIEquipmentManager = GetComponentInParent<PlayerUIEquipmentManager>();
        }
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
            Item equipmentItem;

            switch (PlayerUIManager.instance.playerUIEquipmentManager.currentSelectedEquipmentSlot)
            {
                case EquipmentType.RightWeapon01:
                    equipmentItem = player.playerInventoryManager.weaponsRightHandSlots[0];

                    if (equipmentItem.itemID != WorldItemDatabase.Instance.unarmedWeapon.itemID)
                    {
                        player.playerInventoryManager.AddItemsToInventory(equipmentItem);
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
                    equipmentItem = player.playerInventoryManager.weaponsRightHandSlots[1];

                    if (equipmentItem.itemID != WorldItemDatabase.Instance.unarmedWeapon.itemID)
                    {
                        player.playerInventoryManager.AddItemsToInventory(equipmentItem);
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
                    equipmentItem = player.playerInventoryManager.weaponsRightHandSlots[2];

                    if (equipmentItem.itemID != WorldItemDatabase.Instance.unarmedWeapon.itemID)
                    {
                        player.playerInventoryManager.AddItemsToInventory(equipmentItem);
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
                    equipmentItem = player.playerInventoryManager.weaponsLeftHandSlots[0];

                    if (equipmentItem.itemID != WorldItemDatabase.Instance.unarmedWeapon.itemID)
                    {
                        player.playerInventoryManager.AddItemsToInventory(equipmentItem);
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
                    equipmentItem = player.playerInventoryManager.weaponsLeftHandSlots[1];

                    if (equipmentItem.itemID != WorldItemDatabase.Instance.unarmedWeapon.itemID)
                    {
                        player.playerInventoryManager.AddItemsToInventory(equipmentItem);
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
                    equipmentItem = player.playerInventoryManager.weaponsLeftHandSlots[2];

                    if (equipmentItem.itemID != WorldItemDatabase.Instance.unarmedWeapon.itemID)
                    {
                        player.playerInventoryManager.AddItemsToInventory(equipmentItem);
                    }
                    player.playerInventoryManager.weaponsLeftHandSlots[2] = currentItem as WeaponItem;
                    player.playerInventoryManager.RemoveItemsFromInventory(currentItem);

                    //Equipar una nueva arma si tenemos el arma actual en esta ranura
                    if (player.playerInventoryManager.leftHandWeaponIndex == 2)
                        player.playerNetworkManager.currentLeftHandWeaponID.Value = currentItem.itemID;
                     
                    //Refrescamos la vista
                    PlayerUIManager.instance.playerUIEquipmentManager.RefreshMenu();
                    break;
                case EquipmentType.head:
                    equipmentItem = player.playerInventoryManager.headEquipmentItem;

                    if (equipmentItem != null)
                    {
                        player.playerInventoryManager.AddItemsToInventory(equipmentItem);
                    }
                    player.playerInventoryManager.headEquipmentItem = currentItem as HeadEquipmentItem;
                    player.playerInventoryManager.RemoveItemsFromInventory(currentItem);

                    //Equipar la armadura si tenemos la armadura en esta ranura
                    player.playerEquipmentManager.LoadHeadEquipment(player.playerInventoryManager.headEquipmentItem);
                    //Refrescamos la vista
                    PlayerUIManager.instance.playerUIEquipmentManager.RefreshMenu();
                    break;
                case EquipmentType.Body:
                    equipmentItem = player.playerInventoryManager.bodyEquipmentItem;

                    if (equipmentItem != null)
                    {
                        player.playerInventoryManager.AddItemsToInventory(equipmentItem);
                    }
                    player.playerInventoryManager.bodyEquipmentItem = currentItem as BodyEquipmentItem;
                    player.playerInventoryManager.RemoveItemsFromInventory(currentItem);

                    //Equipar la armadura si tenemos la armadura en esta ranura
                    player.playerEquipmentManager.LoadBodyEquipment(player.playerInventoryManager.bodyEquipmentItem);
                    //Refrescamos la vista
                    PlayerUIManager.instance.playerUIEquipmentManager.RefreshMenu();
                    break;
                case EquipmentType.Legs:
                    equipmentItem = player.playerInventoryManager.legEquipmentItem;

                    if (equipmentItem != null)
                    {
                        player.playerInventoryManager.AddItemsToInventory(equipmentItem);
                    }
                    player.playerInventoryManager.legEquipmentItem = currentItem as LegEquipmentItem;
                    player.playerInventoryManager.RemoveItemsFromInventory(currentItem);

                    //Equipar la armadura si tenemos la armadura en esta ranura
                    player.playerEquipmentManager.LoadLegEquipment(player.playerInventoryManager.legEquipmentItem);
                    //Refrescamos la vista
                    PlayerUIManager.instance.playerUIEquipmentManager.RefreshMenu();
                    break;
                case EquipmentType.Hands:
                    equipmentItem = player.playerInventoryManager.handEquipmentItem;

                    if (equipmentItem != null)
                    {
                        player.playerInventoryManager.AddItemsToInventory(equipmentItem);
                    }
                    player.playerInventoryManager.handEquipmentItem = currentItem as HandEquipmentItem;
                    player.playerInventoryManager.RemoveItemsFromInventory(currentItem);

                    //Equipar la armadura si tenemos la armadura en esta ranura
                    player.playerEquipmentManager.LoadHandEquipment(player.playerInventoryManager.handEquipmentItem);
                    //Refrescamos la vista
                    PlayerUIManager.instance.playerUIEquipmentManager.RefreshMenu();
                    break;
                default:
                    break;
            }
            playerUIEquipmentManager.CloseEquipmentInventoryWindow();
            PlayerUIManager.instance.playerUIEquipmentManager.SelectLastSelectedEquipmentSlot();
        }
    }
}
