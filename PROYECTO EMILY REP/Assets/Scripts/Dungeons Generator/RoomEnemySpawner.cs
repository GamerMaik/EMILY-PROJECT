using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace KC
{
    public class RoomEnemySpawner : MonoBehaviour
    {
        public static RoomEnemySpawner instance;

        [Header("Enemy Configuration")]
        [SerializeField] private float spawnDelay = 2f;
        [SerializeField] private List<GameObject> spawners;


        [Header("Room Configure")]
        [SerializeField] public bool spawnersActivated = false;
        [SerializeField] public GameObject chestsContainerObject;

        [Header("Room Data")]
        public bool enemiesInRoom = false;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && !spawnersActivated)
            {
                Debug.Log("Jugador detectado en la habitación.");
                chestsContainerObject.SetActive(false);
                StartCoroutine(ActivateSpawners());
            }       
        }
        private void Update()
        {
            SpawnRewardChest();
        }
        private IEnumerator ActivateSpawners()
        {
            spawnersActivated = true;
            yield return new WaitForSeconds(spawnDelay);

            // Generar un número aleatorio de spawners a activar
            int spawnersToActivate = Random.Range(1, spawners.Count + 1);
            Debug.Log($"Se activarán {spawnersToActivate} spawners.");

            // Crear una lista temporal para seleccionar spawners aleatorios
            List<GameObject> spawnersCopy = new List<GameObject>(spawners);
            List<GameObject> selectedSpawners = new List<GameObject>();
            WorldLevelManager.instance.AddEnemiesInRoom(spawnersToActivate);
            // Seleccionar spawners aleatorios
            for (int i = 0; i < spawnersToActivate; i++)
            {
                int randomIndex = Random.Range(0, spawnersCopy.Count);
                selectedSpawners.Add(spawnersCopy[randomIndex]);
                spawnersCopy.RemoveAt(randomIndex);
            }

            // Activar los spawners seleccionados
            foreach (GameObject spawner in selectedSpawners)
            {
                if (spawner != null)
                {
                    spawner.SetActive(true);
                }
            }
        }
        private void SpawnRewardChest()
        {
            if (WorldLevelManager.instance.CheckEnemiesInRoom())
            {
                chestsContainerObject.SetActive(false);
            }
            else
            {
                chestsContainerObject.SetActive(true);
            }
        }
    }
}
