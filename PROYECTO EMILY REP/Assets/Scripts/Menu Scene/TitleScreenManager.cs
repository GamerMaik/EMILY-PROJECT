using PlayFab.PfEditor.EditorModels;
using System;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.UI;

namespace KC
{
    public class TitleScreenManager : MonoBehaviour
    {
        public static TitleScreenManager Instance;
        [Header("Title Screens")]
        [SerializeField] GameObject titleScreenMainMenu;
        [SerializeField] GameObject titleScreenLoadMenu;

        [Header("Pop Ups")]
        [SerializeField] GameObject alertPopUpOk;
        [SerializeField] GameObject deleteCharacterSlotPopUp;
        [SerializeField] GameObject nameAlertErrorPopUp;

        [Header("Buttons")]
        [SerializeField] Button noCharacterSlotsOkButton;
        [SerializeField] Button deleteCharacterSlotPopUpOkButton;
        [SerializeField] Button cancelDeleteCharacterButton;

        [Header("Inputs")]
        [SerializeField] TMP_InputField characterName;
        [SerializeField] TextMeshProUGUI genderText;
        [SerializeField] bool isMale = true;

        [Header("Alert Pop Up")]
        [SerializeField] GameObject PopUpMessageAlert;
        [SerializeField] TextMeshProUGUI titleAlertPopUp;
        [SerializeField] TextMeshProUGUI textAlertPopUp;
        [SerializeField] Button OkButtonPopUpAlert;

        [Header("Character Slots")]
        public CharacterSlots currentSelectedSlot = CharacterSlots.NO_SLOT;

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
        }
        public void StartNetworkAsHost()
        {
            NetworkManager.Singleton.StartHost();
        }
        public void StartNewGame()
        {
            bool validText = ValidateInputText(characterName.text);
            if (validText)
            {
                WorldSaveGameManager.instance.AttempToCreateNewGame(characterName.text.ToUpper(), isMale);
            }
            else
            {
                ShowNameAlerPopUp();
            }
        }
        public void OpenLoadGameMenu()
        {
            //Cerra el Menu principal
            titleScreenMainMenu.SetActive(false);
            //Abrir el menu de Carga
            titleScreenLoadMenu.SetActive(true);

            //Buscar el primer guardado
        }
        public void CloseLoadMenu()
        {
            //cerrar el menu de Carga
            titleScreenLoadMenu.SetActive(false);
            //abrir el Menu principal
            titleScreenMainMenu.SetActive(true);
        }
        public void DisplayNoFreeCharactersSlotsPopUp()
        {
            alertPopUpOk.SetActive(true);
            noCharacterSlotsOkButton.Select();
        }
        public void CloseNoFreeCharactersSlotsPopUp()
        {
            alertPopUpOk.SetActive(false);
        }
        public void ShowNameAlerPopUp()
        {
            nameAlertErrorPopUp.SetActive(true);
        }
        public void CloseNameAlerPopUp()
        {
            nameAlertErrorPopUp.SetActive(false);
        }
        //CHARACTER SLOT
        public void SelectedCharacterSlot(CharacterSlots characterSlot)
        {
            currentSelectedSlot = characterSlot;
        }
        public void SelectNoSlot()
        {
            currentSelectedSlot = CharacterSlots.NO_SLOT;
        }
        public void AttemptToDeleteCharacterSlot()
        {
            if (currentSelectedSlot != CharacterSlots.NO_SLOT)
            {
                deleteCharacterSlotPopUp.SetActive(true); 
                deleteCharacterSlotPopUpOkButton.Select();
            }
        }
        public void DeleteCharacterSlot()
        {
            deleteCharacterSlotPopUp.SetActive(false);
            WorldSaveGameManager.instance.DeleteGame(currentSelectedSlot);

            //desactivamos y activamos el menu para refrescar la pantalle de partidas guardadas despues de eliminar uno
            titleScreenLoadMenu.SetActive(false);
            titleScreenLoadMenu.SetActive(true);
            cancelDeleteCharacterButton.Select();
        }
        public void CloseDeleteCharacterPopUp()
        {
            deleteCharacterSlotPopUp.SetActive(false);
            cancelDeleteCharacterButton.Select();
        }
        //Validations
        private bool ValidateInputText(string characterName)
        {
            if (characterName.Length > 10 || characterName.Length == 0)
                return false;

            return true;
        }
        public void ToggleGender()
        {
            PlayerManager player = NetworkManager.Singleton.LocalClient.PlayerObject.GetComponent<PlayerManager>();
            isMale = !isMale;
            if (isMale)
            {
                genderText.text = "MASCULINO";
                player.playerNetworkManager.isMale.Value = true;
            }
            else
            {
                genderText.text = "FEMENINO";
                player.playerNetworkManager.isMale.Value = false;
            }
        }

        //Alert Globalers
        public void ShowAlertPopUp(string title, string message)
        {
            titleAlertPopUp.text = title;
            textAlertPopUp.text = message;
            PopUpMessageAlert.SetActive(true);
        }

        public void CloseAlerPopUp()
        {
            titleAlertPopUp.text = "";
            textAlertPopUp.text = "";
            PopUpMessageAlert.SetActive(false);
        }
    }
}
