using TMPro;
using UnityEngine;

namespace KC
{
    public class UI_Character_Save_Slot : MonoBehaviour
    {
        SaveFileDataWriter saveFileWriter;

        [Header("Game Slot")]
        public CharacterSlots characterSlots;

        [Header("Character Info")]
        public TextMeshProUGUI characterName;
        public TextMeshProUGUI timedPlayed;

        private void OnEnable()
        {
            LoadSaveSlots();
        }
        private void LoadSaveSlots()
        {
            saveFileWriter = new SaveFileDataWriter();
            saveFileWriter.saveDataDirectoryPath = Application.persistentDataPath;

            //Save Slot 01

            if (characterSlots == CharacterSlots.CharacterSlot_01)
            {
                saveFileWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlots);

                //Si el archivo existe se obtienen la informacion de este y si no se desabilita el objeto
                if (saveFileWriter.CheckToSeeFileExist())
                {
                    characterName.text = WorldSaveGameManager.instance.characterSlot01.characterName;
                }
                else
                {
                    gameObject.SetActive(false);
                }
            }

            //Save Slot 02

            else if (characterSlots == CharacterSlots.CharacterSlot_02)
            {
                saveFileWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlots);

                //Si el archivo existe se obtienen la informacion de este y si no se desabilita el objeto
                if (saveFileWriter.CheckToSeeFileExist())
                {
                    characterName.text = WorldSaveGameManager.instance.characterSlot02.characterName;
                }
                else
                {
                    gameObject.SetActive(false);
                }
            }

            //Save Slot 03

            else if(characterSlots == CharacterSlots.CharacterSlot_03)
            {
                saveFileWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlots);

                //Si el archivo existe se obtienen la informacion de este y si no se desabilita el objeto
                if (saveFileWriter.CheckToSeeFileExist())
                {
                    characterName.text = WorldSaveGameManager.instance.characterSlot03.characterName;
                }
                else
                {
                    gameObject.SetActive(false);
                }
            }

            //Save Slot 04

            else if (characterSlots == CharacterSlots.CharacterSlot_04)
            {
                saveFileWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlots);

                //Si el archivo existe se obtienen la informacion de este y si no se desabilita el objeto
                if (saveFileWriter.CheckToSeeFileExist())
                {
                    characterName.text = WorldSaveGameManager.instance.characterSlot04.characterName;
                }
                else
                {
                    gameObject.SetActive(false);
                }
            }

            //Save Slot 05

            else if (characterSlots == CharacterSlots.CharacterSlot_05)
            {
                saveFileWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlots);

                //Si el archivo existe se obtienen la informacion de este y si no se desabilita el objeto
                if (saveFileWriter.CheckToSeeFileExist())
                {
                    characterName.text = WorldSaveGameManager.instance.characterSlot05.characterName;
                }
                else
                {
                    gameObject.SetActive(false);
                }
            }

            //Save Slot 06

            else if (characterSlots == CharacterSlots.CharacterSlot_06)
            {
                saveFileWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlots);

                //Si el archivo existe se obtienen la informacion de este y si no se desabilita el objeto
                if (saveFileWriter.CheckToSeeFileExist())
                {
                    characterName.text = WorldSaveGameManager.instance.characterSlot06.characterName;
                }
                else
                {
                    gameObject.SetActive(false);
                }
            }

            //Save Slot 07

            else if (characterSlots == CharacterSlots.CharacterSlot_07)
            {
                saveFileWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlots);

                //Si el archivo existe se obtienen la informacion de este y si no se desabilita el objeto
                if (saveFileWriter.CheckToSeeFileExist())
                {
                    characterName.text = WorldSaveGameManager.instance.characterSlot07.characterName;
                }
                else
                {
                    gameObject.SetActive(false);
                }
            }

            //Save Slot 08

            else if (characterSlots == CharacterSlots.CharacterSlot_08)
            {
                saveFileWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlots);

                //Si el archivo existe se obtienen la informacion de este y si no se desabilita el objeto
                if (saveFileWriter.CheckToSeeFileExist())
                {
                    characterName.text = WorldSaveGameManager.instance.characterSlot08.characterName;
                }
                else
                {
                    gameObject.SetActive(false);
                }
            }

            //Save Slot 09

            else if (characterSlots == CharacterSlots.CharacterSlot_09)
            {
                saveFileWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlots);

                //Si el archivo existe se obtienen la informacion de este y si no se desabilita el objeto
                if (saveFileWriter.CheckToSeeFileExist())
                {
                    characterName.text = WorldSaveGameManager.instance.characterSlot09.characterName;
                }
                else
                {
                    gameObject.SetActive(false);
                }
            }

            //Save Slot 10

            else if (characterSlots == CharacterSlots.CharacterSlot_10)
            {
                saveFileWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlots);

                //Si el archivo existe se obtienen la informacion de este y si no se desabilita el objeto
                if (saveFileWriter.CheckToSeeFileExist())
                {
                    characterName.text = WorldSaveGameManager.instance.characterSlot10.characterName;
                }
                else
                {
                    gameObject.SetActive(false);
                }
            }
        }

        public void LoadGameFromCharactersSlot()
        {
            WorldSaveGameManager.instance.currentCharacterSlotsBeingUsed = characterSlots;
            WorldSaveGameManager.instance.LoadGame();
        }

        public void SelectedCurrentSlot()
        {
            TitleScreenManager.Instance.SelectedCharacterSlot(characterSlots);
        }
    }
}
