using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Netcode;

namespace KC
{
    public class WorldSaveGameManager : MonoBehaviour
    {
        public static WorldSaveGameManager instance;

        public PlayerManager player;

        [Header("SAVE/LOAD")]
        [SerializeField] bool saveGame;
        [SerializeField] bool loadGame;

        [Header("World Scene Index")]
        [SerializeField] int worldSceneIndex = 1;

        [Header("Save Data Writer")]
        private SaveFileDataWriter saveFileDataWriter;

        [Header("Current Character Data")]
        public CharacterSlots currentCharacterSlotsBeingUsed;
        public CharacterSaveData currentCharacterData;
        private string saveFileName = "";

        [Header("Character Slots")]
        public CharacterSaveData characterSlot01;
        public CharacterSaveData characterSlot02;
        public CharacterSaveData characterSlot03;
        public CharacterSaveData characterSlot04;
        public CharacterSaveData characterSlot05;
        public CharacterSaveData characterSlot06;
        public CharacterSaveData characterSlot07;
        public CharacterSaveData characterSlot08;
        public CharacterSaveData characterSlot09;
        public CharacterSaveData characterSlot10;

        private void Awake()
        {
            //Solo puede haber uno de estos administradores de juegos guardados del mundo en la escena a la vez, Si existe otro, se destruye
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        private void Start()
        {
            DontDestroyOnLoad(gameObject);
            LoadAllCharactersProfiles();
        }
        private void Update()
        {
            if (saveGame)
            {
                saveGame = false;
                SaveGame();
            }
            if (loadGame)
            {
                loadGame = false;
                LoadGame();
            }
        }
        public string DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlots characterSlot)
        {
            string filename = "";

            switch (characterSlot)
            {
                case CharacterSlots.CharacterSlot_01:
                    filename = "CharacterSlot_01";
                    break;
                case CharacterSlots.CharacterSlot_02:
                    filename = "CharacterSlot_02";
                    break;
                case CharacterSlots.CharacterSlot_03:
                    filename = "CharacterSlot_03";
                    break;
                case CharacterSlots.CharacterSlot_04:
                    filename = "CharacterSlot_04";
                    break;
                case CharacterSlots.CharacterSlot_05:
                    filename = "CharacterSlot_05";
                    break;
                case CharacterSlots.CharacterSlot_06:
                    filename = "CharacterSlot_06";
                    break;
                case CharacterSlots.CharacterSlot_07:
                    filename = "CharacterSlot_07";
                    break;
                case CharacterSlots.CharacterSlot_08:
                    filename = "CharacterSlot_08";
                    break;
                case CharacterSlots.CharacterSlot_09:
                    filename = "CharacterSlot_09";
                    break;
                case CharacterSlots.CharacterSlot_10:
                    filename = "CharacterSlot_10";
                    break;
                default:
                    break;
            }

            return filename;
        }
        public void AttempToCreateNewGame()
        {
            saveFileDataWriter = new SaveFileDataWriter();
            saveFileDataWriter.saveDataDirectoryPath = Application.persistentDataPath;

            saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlots.CharacterSlot_01);
            if (!saveFileDataWriter.CheckToSeeFileExist())
            {
                //Comprobamos si este espacio está disponible
                currentCharacterSlotsBeingUsed = CharacterSlots.CharacterSlot_01;
                currentCharacterData = new CharacterSaveData();
                NewGame();
                return;
            }
            //Creamos el archivo con el nombre de Slot que se seleccionó
            saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlots.CharacterSlot_02);
            if (!saveFileDataWriter.CheckToSeeFileExist())
            {
                currentCharacterSlotsBeingUsed = CharacterSlots.CharacterSlot_02;
                currentCharacterData = new CharacterSaveData();
                NewGame();
                return;
            }

            saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlots.CharacterSlot_03);
            if (!saveFileDataWriter.CheckToSeeFileExist())
            {
                currentCharacterSlotsBeingUsed = CharacterSlots.CharacterSlot_03;
                currentCharacterData = new CharacterSaveData();
                NewGame();
                return;
            }

            saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlots.CharacterSlot_04);
            if (!saveFileDataWriter.CheckToSeeFileExist())
            {
                currentCharacterSlotsBeingUsed = CharacterSlots.CharacterSlot_04;
                currentCharacterData = new CharacterSaveData();
                NewGame();
                return;
            }

            saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlots.CharacterSlot_05);
            if (!saveFileDataWriter.CheckToSeeFileExist())
            {
                currentCharacterSlotsBeingUsed = CharacterSlots.CharacterSlot_05;
                currentCharacterData = new CharacterSaveData();
                NewGame();
                return;
            }

            saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlots.CharacterSlot_06);
            if (!saveFileDataWriter.CheckToSeeFileExist())
            {
                currentCharacterSlotsBeingUsed = CharacterSlots.CharacterSlot_06;
                currentCharacterData = new CharacterSaveData();
                NewGame();
                return;
            }

            saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlots.CharacterSlot_07);
            if (!saveFileDataWriter.CheckToSeeFileExist())
            {
                currentCharacterSlotsBeingUsed = CharacterSlots.CharacterSlot_07;
                currentCharacterData = new CharacterSaveData();
                NewGame();
                return;
            }

            saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlots.CharacterSlot_08);
            if (!saveFileDataWriter.CheckToSeeFileExist())
            {
                currentCharacterSlotsBeingUsed = CharacterSlots.CharacterSlot_08;
                currentCharacterData = new CharacterSaveData();
                NewGame();
                return;
            }

            saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlots.CharacterSlot_09);
            if (!saveFileDataWriter.CheckToSeeFileExist())
            {
                currentCharacterSlotsBeingUsed = CharacterSlots.CharacterSlot_09;
                currentCharacterData = new CharacterSaveData();
                NewGame();
                return;
            }

            saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlots.CharacterSlot_10);
            if (!saveFileDataWriter.CheckToSeeFileExist())
            {
                currentCharacterSlotsBeingUsed = CharacterSlots.CharacterSlot_10;
                currentCharacterData = new CharacterSaveData();
                NewGame();
                return;
            }

            TitleScreenManager.Instance.DisplayNoFreeCharactersSlotsPopUp();
        }

        private void NewGame()
        {
            //Temporal para probar el daño
            player.playerNetworkManager.vitality.Value = 15;
            player.playerNetworkManager.endurance.Value = 10;


            SaveGame();
            LoadWorldScene(worldSceneIndex);
        }

        public void LoadGame()
        {
            //Cargamos el juego dependiendo al nombre de archivo de slot que se seleccionó
            saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(currentCharacterSlotsBeingUsed);

            saveFileDataWriter = new SaveFileDataWriter();
            saveFileDataWriter.saveDataDirectoryPath = Application.persistentDataPath;
            saveFileDataWriter.saveFileName = saveFileName;
            currentCharacterData = saveFileDataWriter.LoadSaveGameData();

            LoadWorldScene(worldSceneIndex);
        }
        public void SaveGame()
        {
            saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(currentCharacterSlotsBeingUsed);

            saveFileDataWriter = new SaveFileDataWriter();

            saveFileDataWriter.saveDataDirectoryPath = Application.persistentDataPath;
            saveFileDataWriter.saveFileName = saveFileName;

            //pasar la informacion obtenida al jugador
            player.SaveGameDataCurrentCharacterData(ref currentCharacterData);

            saveFileDataWriter.CreateNewCharacterSaveFile(currentCharacterData);
        }

        public void DeleteGame(CharacterSlots characterSlot)
        {
            //Elegir un archivo para eliminar por nombre
            saveFileDataWriter = new SaveFileDataWriter();
            saveFileDataWriter.saveDataDirectoryPath = Application.persistentDataPath;
            saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

            saveFileDataWriter.DeleteSaveFile();
        }
        //CARGAR TODOS LOS PERFILES EN EL DISPOSITIVO AL CARGAR EL JUEGO
        private void LoadAllCharactersProfiles()
        {
            saveFileDataWriter = new SaveFileDataWriter();
            saveFileDataWriter.saveDataDirectoryPath = Application.persistentDataPath;

            saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlots.CharacterSlot_01);
            characterSlot01 = saveFileDataWriter.LoadSaveGameData();

            saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlots.CharacterSlot_02);
            characterSlot02 = saveFileDataWriter.LoadSaveGameData();

            saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlots.CharacterSlot_03);
            characterSlot03 = saveFileDataWriter.LoadSaveGameData();

            saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlots.CharacterSlot_04);
            characterSlot04 = saveFileDataWriter.LoadSaveGameData();

            saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlots.CharacterSlot_05);
            characterSlot05 = saveFileDataWriter.LoadSaveGameData();

            saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlots.CharacterSlot_06);
            characterSlot06 = saveFileDataWriter.LoadSaveGameData();

            saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlots.CharacterSlot_07);
            characterSlot07 = saveFileDataWriter.LoadSaveGameData();

            saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlots.CharacterSlot_08);
            characterSlot08 = saveFileDataWriter.LoadSaveGameData();

            saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlots.CharacterSlot_09);
            characterSlot09 = saveFileDataWriter.LoadSaveGameData();

            saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlots.CharacterSlot_10);
            characterSlot10 = saveFileDataWriter.LoadSaveGameData();
        }
        public void LoadWorldScene(int buildIndex)
        {

            string worldScene = SceneUtility.GetScenePathByBuildIndex(buildIndex);
            NetworkManager.Singleton.SceneManager.LoadScene(worldScene, LoadSceneMode.Single);

            //AsyncOperation loadOperation = SceneManager.LoadSceneAsync(worldSceneIndex);

            player.LoadGameDataFromCurrentCharacterData(ref currentCharacterData);
        }
        public int GetWorldSceneIndex()
        {
            return worldSceneIndex;
        }
    }
}
