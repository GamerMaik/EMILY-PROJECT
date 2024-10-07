using UnityEngine;
using Unity.Netcode;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace KC
{
    public class WorldAIManager : MonoBehaviour
    {
        public static WorldAIManager instance;

        [Header("Characters")]
        [SerializeField] List<AICharacterSpawner> aiCharacterSpawners;
        [SerializeField] List<GameObject> spawnedInCharacters; //Lista de los personajes que ya se generaron

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        } 

        public void SpawnCharacters(AICharacterSpawner aiCharacterSpawner)
        {
            if (NetworkManager.Singleton.IsServer)
            {
                aiCharacterSpawners.Add(aiCharacterSpawner);
                aiCharacterSpawner.AttemptToSpawnCharacter();
            }
        }

        //private void SpawnAllCharacter()
        //{
        //    foreach (var character in aiCharacters)
        //    {
        //        GameObject instantiatedCharacter = Instantiate(character);
        //        instantiatedCharacter.GetComponent<NetworkObject>().Spawn();
        //        spawnedInObjects.Add(instantiatedCharacter);
        //    } 
        //}

        private void DespawnAllCharacters()
        {
            foreach (var character in spawnedInCharacters)
            {
                character.GetComponent<NetworkObject>().Despawn();
            }
        }

        private void DisableAllCharacters()
        {
            //Usar la tecnica de object pulling para evitar la carga de memoria o de caida de FPS activando o desactivando objetos a cargados


        }
    }
}
