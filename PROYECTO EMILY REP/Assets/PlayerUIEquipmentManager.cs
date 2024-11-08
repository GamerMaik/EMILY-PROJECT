using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Unity.Netcode;

namespace KC
{
    public class PlayerUIEquipmentManager : MonoBehaviour
    {
        [Header("Menu")]
        [SerializeField] GameObject menu;

        [Header("Weapon Slots")]
        [SerializeField] Image rightHandSlot01;
        [SerializeField] Image rightHandSlot02;
        [SerializeField] Image rightHandSlot03;
        [SerializeField] Image leftHandSlot01;
        [SerializeField] Image leftHandSlot02;
        [SerializeField] Image leftHandSlot03;

        [Header("Equip Inventory")]
        public EquipmentType currentSelectedEquipmentSlot;
        [SerializeField] GameObject equipmentInventoryWindow;
        [SerializeField] GameObject equipmentInventorySlotPrefab;
        [SerializeField] Transform equipmentInventoryContentWindow;
        [SerializeField] Item currentSelectedItem;

        public void OpenEquipmentManagerMenu()
        {
            PlayerUIManager.instance.menuWindowIsOpen = true;
            menu.SetActive(true);
            equipmentInventoryWindow.SetActive(false);
            
        }

        public void RefreshMenu()
        {
            ClearEquipmentInventry();
            RefreshWeaponSlotIcons();
        }

        public void SelectLastSelectedEquipmentSlot()
        {
            Button lastSelectedButton = null;
            switch (currentSelectedEquipmentSlot)
            {
                case EquipmentType.RightWeapon01:
                    lastSelectedButton =  rightHandSlot01.GetComponentInParent<Button>();
                    break;
                case EquipmentType.RightWeapon02:
                    lastSelectedButton = rightHandSlot02.GetComponentInParent<Button>();
                    break;
                case EquipmentType.RightWeapon03:
                    lastSelectedButton = rightHandSlot03.GetComponentInParent<Button>();
                    break;
                case EquipmentType.LeftWeapon01:
                    lastSelectedButton = leftHandSlot01.GetComponentInParent<Button>();
                    break;
                case EquipmentType.LeftWeapon02:
                    lastSelectedButton = leftHandSlot02.GetComponentInParent<Button>();
                    break;
                case EquipmentType.LeftWeapon03:
                    lastSelectedButton = leftHandSlot03.GetComponentInParent<Button>();
                    break;
                default:
                    break;
            }

            if (lastSelectedButton != null)
            {
                lastSelectedButton.Select();
                lastSelectedButton.OnSelect(null);
            }
        }
        public void CloseEquipmentManagerMenu()
        {
            PlayerUIManager.instance.menuWindowIsOpen = false;
            menu.SetActive(false);
        }

        private void RefreshWeaponSlotIcons()
        {
            PlayerManager player = NetworkManager.Singleton.LocalClient.PlayerObject.GetComponent<PlayerManager>();

            //Right Weapon 01
            WeaponItem rightHandWeapon01 = player.playerInventoryManager.weaponsRightHandSlots[0];
            if (rightHandWeapon01.itemIcon != null)
            {
                rightHandSlot01.enabled = true;
                rightHandSlot01.sprite = rightHandWeapon01.itemIcon;
            }
            else
            {
                rightHandSlot01.enabled = false;
            }

            //Right Weapon 02
            WeaponItem rightHandWeapon02 = player.playerInventoryManager.weaponsRightHandSlots[1];
            if (rightHandWeapon02.itemIcon != null)
            {
                rightHandSlot02.enabled = true;
                rightHandSlot02.sprite = rightHandWeapon02.itemIcon;
            }
            else
            {
                rightHandSlot02.enabled = false;
            }

            //Right Weapon 03
            WeaponItem rightHandWeapon03 = player.playerInventoryManager.weaponsRightHandSlots[2];
            if (rightHandWeapon03.itemIcon != null)
            {
                rightHandSlot03.enabled = true;
                rightHandSlot03.sprite = rightHandWeapon03.itemIcon;
            }
            else
            {
                rightHandSlot03.enabled = false;
            }

            //Left Weapon 01
            WeaponItem leftHandWeapon01 = player.playerInventoryManager.weaponsLeftHandSlots[0];
            if (leftHandWeapon01.itemIcon != null)
            {
                leftHandSlot01.enabled = true;
                leftHandSlot01.sprite = leftHandWeapon01.itemIcon;
            }
            else
            {
                leftHandSlot01.enabled = false;
            }

            //Left Weapon 02
            WeaponItem leftHandWeapon02 = player.playerInventoryManager.weaponsLeftHandSlots[1];
            if (leftHandWeapon02.itemIcon != null)
            {
                leftHandSlot02.enabled = true;
                leftHandSlot02.sprite = leftHandWeapon02.itemIcon;
            }
            else
            {
                leftHandSlot02.enabled = false;
            }

            //Left Weapon 03
            WeaponItem leftHandWeapon03 = player.playerInventoryManager.weaponsLeftHandSlots[2];
            if (leftHandWeapon03.itemIcon != null)
            {
                leftHandSlot03.enabled = true;
                leftHandSlot03.sprite = leftHandWeapon03.itemIcon;
            }
            else
            {
                leftHandSlot03.enabled = false;
            }
        }

        private void ClearEquipmentInventry()
        {
            foreach (Transform item in equipmentInventoryContentWindow)
            {
                Destroy(item.gameObject);
            }
        }

        public void LoadEquipmentInventory()
        {
            equipmentInventoryWindow.SetActive(true);

            switch (currentSelectedEquipmentSlot)
            {
                case EquipmentType.RightWeapon01:
                    LoadWeaponInventory();
                    break;
                case EquipmentType.RightWeapon02:
                    LoadWeaponInventory();
                    break;
                case EquipmentType.RightWeapon03:
                    LoadWeaponInventory();
                    break;
                case EquipmentType.LeftWeapon01:
                    LoadWeaponInventory();
                    break;
                case EquipmentType.LeftWeapon02:
                    LoadWeaponInventory();
                    break;
                case EquipmentType.LeftWeapon03:
                    LoadWeaponInventory();
                    break;
                default:
                    break;
            }
        }

        public void LoadWeaponInventory()
        {
            PlayerManager player =  NetworkManager.Singleton.LocalClient.PlayerObject.GetComponent<PlayerManager>();
            List<WeaponItem> weaponsInInventory = new List<WeaponItem>();


            //Buscamos entre todas nuestros objetos en el inventario y si es un arama lo agregamos a nuestra lista de armas
            for (int i = 0; i < player.playerInventoryManager.itemsInventory.Count; i++)
            {
                WeaponItem weapon = player.playerInventoryManager.itemsInventory[i] as WeaponItem;
                
                if(weapon != null)
                    weaponsInInventory.Add(weapon); 
            }

            if(weaponsInInventory.Count <= 0)
            {
                RefreshMenu();
                return;
            }

            bool hasSelectedFirstInventorySlot = false;

            for (int i = 0; i < weaponsInInventory.Count; i++)
            {
                GameObject inventorySlotGameObject = Instantiate(equipmentInventorySlotPrefab, equipmentInventoryContentWindow);
                UI_EquipmentInventorySlot equipmentInventorySlot = inventorySlotGameObject.GetComponent<UI_EquipmentInventorySlot>();
                equipmentInventorySlot.AddItem(weaponsInInventory[i]);


                //Seleccionará el primer boton en la lista
                if (!hasSelectedFirstInventorySlot)
                {
                    hasSelectedFirstInventorySlot = true;
                    Button inventorySlotButton = inventorySlotGameObject.GetComponent<Button>();
                    inventorySlotButton.Select();
                    inventorySlotButton.OnSelect(null);
                }
            }
        }

        public void SelectEquipmentSlot(int equipmentSlot)
        {
            currentSelectedEquipmentSlot = (EquipmentType)equipmentSlot;
        }

        public void UnEquipSelectedItem()
        {
            PlayerManager player = NetworkManager.Singleton.LocalClient.PlayerObject.GetComponent<PlayerManager>();
            Item unequippedItem;
            switch (currentSelectedEquipmentSlot)
            {
                case EquipmentType.RightWeapon01:
                    unequippedItem = player.playerInventoryManager.weaponsRightHandSlots[0];
                    if (unequippedItem !=null)
                    {
                        player.playerInventoryManager.weaponsRightHandSlots[0] = Instantiate(WorldItemDatabase.Instance.unarmedWeapon);

                        if (unequippedItem.itemID != WorldItemDatabase.Instance.unarmedWeapon.itemID)
                            player.playerInventoryManager.AddItemsToInventory(unequippedItem);
                    }

                    if(player.playerInventoryManager.rightHandWeaponIndex == 0)
                        player.playerNetworkManager.currentRightHandWeaponID.Value = WorldItemDatabase.Instance.unarmedWeapon.itemID;

                    break;
                case EquipmentType.RightWeapon02:
                    unequippedItem = player.playerInventoryManager.weaponsRightHandSlots[1];
                    if (unequippedItem != null)
                    {
                        player.playerInventoryManager.weaponsRightHandSlots[1] = Instantiate(WorldItemDatabase.Instance.unarmedWeapon);

                        if (unequippedItem.itemID != WorldItemDatabase.Instance.unarmedWeapon.itemID)
                            player.playerInventoryManager.AddItemsToInventory(unequippedItem);
                    }

                    if (player.playerInventoryManager.rightHandWeaponIndex == 1)
                        player.playerNetworkManager.currentRightHandWeaponID.Value = WorldItemDatabase.Instance.unarmedWeapon.itemID;

                    break;
                case EquipmentType.RightWeapon03:
                    unequippedItem = player.playerInventoryManager.weaponsRightHandSlots[2];
                    if (unequippedItem != null)
                    {
                        player.playerInventoryManager.weaponsRightHandSlots[2] = Instantiate(WorldItemDatabase.Instance.unarmedWeapon);

                        if (unequippedItem.itemID != WorldItemDatabase.Instance.unarmedWeapon.itemID)
                            player.playerInventoryManager.AddItemsToInventory(unequippedItem);
                    }

                    if (player.playerInventoryManager.rightHandWeaponIndex == 2)
                        player.playerNetworkManager.currentRightHandWeaponID.Value = WorldItemDatabase.Instance.unarmedWeapon.itemID;

                    break;
                case EquipmentType.LeftWeapon01:
                    unequippedItem = player.playerInventoryManager.weaponsLeftHandSlots[0];
                    if (unequippedItem != null)
                    {
                        player.playerInventoryManager.weaponsLeftHandSlots[0] = Instantiate(WorldItemDatabase.Instance.unarmedWeapon);

                        if (unequippedItem.itemID != WorldItemDatabase.Instance.unarmedWeapon.itemID)
                            player.playerInventoryManager.AddItemsToInventory(unequippedItem);
                    }

                    if (player.playerInventoryManager.leftHandWeaponIndex == 0)
                        player.playerNetworkManager.currentLeftHandWeaponID.Value = WorldItemDatabase.Instance.unarmedWeapon.itemID;

                    break;
                case EquipmentType.LeftWeapon02:
                    unequippedItem = player.playerInventoryManager.weaponsLeftHandSlots[1];
                    if (unequippedItem != null)
                    {
                        player.playerInventoryManager.weaponsLeftHandSlots[1] = Instantiate(WorldItemDatabase.Instance.unarmedWeapon);

                        if (unequippedItem.itemID != WorldItemDatabase.Instance.unarmedWeapon.itemID)
                            player.playerInventoryManager.AddItemsToInventory(unequippedItem);
                    }

                    if (player.playerInventoryManager.leftHandWeaponIndex == 1)
                        player.playerNetworkManager.currentLeftHandWeaponID.Value = WorldItemDatabase.Instance.unarmedWeapon.itemID;
                    break;
                case EquipmentType.LeftWeapon03:
                    unequippedItem = player.playerInventoryManager.weaponsLeftHandSlots[2];
                    if (unequippedItem != null)
                    {
                        player.playerInventoryManager.weaponsLeftHandSlots[2] = Instantiate(WorldItemDatabase.Instance.unarmedWeapon);

                        if (unequippedItem.itemID != WorldItemDatabase.Instance.unarmedWeapon.itemID)
                            player.playerInventoryManager.AddItemsToInventory(unequippedItem);
                    }

                    if (player.playerInventoryManager.leftHandWeaponIndex == 2)
                        player.playerNetworkManager.currentLeftHandWeaponID.Value = WorldItemDatabase.Instance.unarmedWeapon.itemID;
                    break;
                default:
                    break;
            }

            //Refrescar Interfaz de Usuario
            RefreshMenu();
        }
    }
}
