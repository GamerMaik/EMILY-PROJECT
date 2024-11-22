using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace KC
{
    public class RoomEnemySpawner : MonoBehaviour
    {
        [Header("Enemy Configuration")]
        [SerializeField] private float spawnDelay = 3f; // Tiempo antes de activar los spawners
        [SerializeField] private List<GameObject> spawners; // Lista de spawners manuales

        [SerializeField] bool spawnersActivated = false; // Evitar activar más de una vez

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && !spawnersActivated)
            {
                Debug.Log("Jugador detectado en la habitación.");
                //WorldDungeonManager.instance.GenerateNavmesh();
                //spawnersActivated = true;
                StartCoroutine(ActivateSpawners());
            }
        }

        private IEnumerator ActivateSpawners()
        {
            yield return new WaitForSeconds(spawnDelay);

            foreach (GameObject spawner in spawners)
            {
                if (spawner != null)
                {
                    spawner.SetActive(true);
                    Debug.Log($"Spawner activado: {spawner.name}");
                }
            }

            Debug.Log("Todos los spawners han sido activados.");
        }
    }
}
