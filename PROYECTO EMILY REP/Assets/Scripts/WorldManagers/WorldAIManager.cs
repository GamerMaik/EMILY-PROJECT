using UnityEngine;
using Unity.Netcode;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine.AI;

namespace KC
{
    public class WorldAIManager : MonoBehaviour
    {
        public static WorldAIManager instance;

        [Header("Characters")]
        [SerializeField] private List<AICharacterSpawner> aiCharacterSpawners = new List<AICharacterSpawner>();
        [SerializeField] private List<AICharacterManager> spawnedInCharacters = new List<AICharacterManager>();

        [Header("Bosses")]
        [SerializeField] private List<AIBossCharacterManager> spawnedInBosses = new List<AIBossCharacterManager>();

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

        public void AddCharacterToSpawnedCharacterList(AICharacterManager character)
        {
            if (spawnedInCharacters.Contains(character))
                return;

            spawnedInCharacters.Add(character);

            AIBossCharacterManager bossCharacter = character as AIBossCharacterManager;
            if (bossCharacter != null)
            {
                if (spawnedInBosses.Contains(bossCharacter))
                    return;

                spawnedInBosses.Add(bossCharacter);
            }
        }

        public AIBossCharacterManager GetBossCharacterByID(int ID)
        {
            return spawnedInBosses.FirstOrDefault(boss => boss.bossID == ID);
        }

        public void ResetAllCharacters()
        {
            DespawnAllCharacters();

            foreach (var spawner in aiCharacterSpawners)
            {
                spawner.AttemptToSpawnCharacter();
            }
        }

        private void DespawnAllCharacters()
        {
            foreach (var character in spawnedInCharacters)
            {
                character.GetComponent<NetworkObject>().Despawn();
            }

            spawnedInCharacters.Clear();
        }

        private void DisableAllCharacters()
        {
            foreach (var character in spawnedInCharacters)
            {
                character.gameObject.SetActive(false);
            }
        }
    }
}
