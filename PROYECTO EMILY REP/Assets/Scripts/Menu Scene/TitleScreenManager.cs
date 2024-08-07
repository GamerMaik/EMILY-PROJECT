using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

namespace KC
{
    public class TitleScreenManager : MonoBehaviour
    {
        public static TitleScreenManager Instance;
        [SerializeField] GameObject titleScreenMainMenu;
        [SerializeField] GameObject titleScreenLoadMenu;

        [Header("Pop Ups")]
        [SerializeField] GameObject noCharacterSlotsPopUp;
        [SerializeField] GameObject deleteCharacterSlotPopUp;

        [Header("Buttons")]
        [SerializeField] Button noCharacterSlotsOkButton;
        [SerializeField] Button deleteCharacterSlotPopUpOkButton;
        [SerializeField] Button cancelDeleteCharacterButton;

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
            WorldSaveGameManager.instance.AttempToCreateNewGame();
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
            noCharacterSlotsPopUp.SetActive(true);
            noCharacterSlotsOkButton.Select();
        }
        public void CloseNoFreeCharactersSlotsPopUp()
        {
            noCharacterSlotsPopUp.SetActive(false);
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
    }
}
