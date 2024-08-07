using UnityEngine;
using System;
using System.IO;

namespace KC
{
    public class SaveFileDataWriter
    {
        public string saveDataDirectoryPath = "";
        public string saveFileName = "";
        
        //antes de crear un archivo nuevo debemos comprobar si este ya existe (Max 5 espacios)
        public bool CheckToSeeFileExist()
        {
            if (File.Exists(Path.Combine(saveDataDirectoryPath, saveFileName)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Se usa para eliminar los datos de un espacio del jugador
        public void DeleteSaveFile() 
        {
            File.Delete(Path.Combine(saveDataDirectoryPath, saveFileName));
        }

        //Se usara para crear un archivo guardado o al iniciar un nuevo juego
        public void CreateNewCharacterSaveFile(CharacterSaveData characterData)
        {
            //primero creamos la ruta donde se guardará el archivo
            string savePath = Path.Combine(saveDataDirectoryPath, saveFileName);

            try
            {
                //Verifiacar el directorio para el archivo y si no existe crearlo
                Directory.CreateDirectory(Path.GetDirectoryName(savePath));
                Debug.Log("Creando el archivo, Guardado en "+ savePath);

                //Convertir el archivo de datos de C# a un JSON

                string dataToStore = JsonUtility.ToJson(characterData, true);

                //Una vez convertido a JSON lo guardamos

                using (FileStream stream = new FileStream(savePath, FileMode.Create))
                {
                    using (StreamWriter fileWriter = new StreamWriter(stream))
                    {
                        fileWriter.Write(dataToStore);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("Error al intentar guardar los datos del jugador, JUEGO NO GUARDADO" +savePath + "\n" + ex);
            }
        }
        
        //Se usa para cargar un archivo ya guardado o existente de un juego anterior
        public CharacterSaveData LoadSaveGameData()
        {
            CharacterSaveData characterData = null;

            //primero cargamos la ruta donde se guardó el archivo
            string loadPath = Path.Combine(saveDataDirectoryPath, saveFileName);

            //Comprobamos si el archivo existe

            if (File.Exists(loadPath))
            {
                try
                {
                    string dataToLoad = "";

                    using (FileStream stream = new FileStream(loadPath, FileMode.Open))
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            dataToLoad = reader.ReadToEnd();
                        }
                    }

                    //Decodificar los archivos de tipo Json a un archivo entendible para Unity C#

                    characterData = JsonUtility.FromJson<CharacterSaveData>(dataToLoad);
                }
                catch (Exception ex)
                {
                    Debug.LogError("No se pudo leer el archivo de juego" + "\n" + ex);
                }
            }
            return characterData;
        }
    }
}
