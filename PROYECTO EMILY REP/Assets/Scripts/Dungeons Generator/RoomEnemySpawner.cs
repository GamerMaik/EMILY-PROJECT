using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace KC
{
    public class RoomEnemySpawner : MonoBehaviour
    {
        [Header("Enemy Configuration")]
        [SerializeField] private float spawnDelay = 3f;
        [SerializeField] private List<GameObject> spawners;

        [Header("Room Configure")]
        [SerializeField] public bool spawnersActivated = false;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && !spawnersActivated)
            {
                Debug.Log("Jugador detectado en la habitación.");
                StartCoroutine(ActivateSpawners());
            }
        }

        private IEnumerator ActivateSpawners()
        {
            spawnersActivated = true; // Evitar que se active de nuevo
            yield return new WaitForSeconds(spawnDelay);

            // Generar un número aleatorio de spawners a activar
            int spawnersToActivate = Random.Range(1, spawners.Count + 1); // Incluye 1 al tamaño total
            Debug.Log($"Se activarán {spawnersToActivate} spawners.");

            // Crear una lista temporal para seleccionar spawners aleatorios
            List<GameObject> spawnersCopy = new List<GameObject>(spawners);
            List<GameObject> selectedSpawners = new List<GameObject>();

            // Seleccionar spawners aleatorios
            for (int i = 0; i < spawnersToActivate; i++)
            {
                int randomIndex = Random.Range(0, spawnersCopy.Count);
                selectedSpawners.Add(spawnersCopy[randomIndex]);
                spawnersCopy.RemoveAt(randomIndex); // Evitar repetir spawners
            }

            // Activar los spawners seleccionados
            foreach (GameObject spawner in selectedSpawners)
            {
                if (spawner != null)
                {
                    spawner.SetActive(true);
                    Debug.Log($"Spawner activado: {spawner.name}");
                }
            }

            Debug.Log($"Se activaron {spawnersToActivate} spawners.");
        }
    }
}
