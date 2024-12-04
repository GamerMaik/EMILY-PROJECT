using UnityEngine;
using Unity.Netcode;
using UnityEngine.AI;

namespace KC
{
    public class AICharacterSpawner : MonoBehaviour
    {
        [Header("Character")]
        [SerializeField] GameObject characterGameObject;
        [SerializeField] GameObject instantiateGameObject;
        private void Awake()
        {
             
        }

        private void Start()
        {
            WorldAIManager.instance.SpawnCharacters(this);
            gameObject.SetActive(false);
        }

        public void AttemptToSpawnCharacter()
        {
            if (characterGameObject != null)
            {
                instantiateGameObject = Instantiate(characterGameObject, transform.position, Quaternion.identity);
                AICharacterManager character = instantiateGameObject.GetComponent<AICharacterManager>();
                character.aiCharacterNetworkManager.currentHealth.Value = 150;
                character.aiCharacterNetworkManager.maxHealth.Value = 150;
                instantiateGameObject.transform.position = transform.position;
                instantiateGameObject.transform.rotation = transform.rotation;
                instantiateGameObject.GetComponent<NetworkObject>().Spawn();
                WorldAIManager.instance.AddCharacterToSpawnedCharacterList(character);
            }
        }
    }
}
