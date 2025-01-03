using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace KC
{
    public class DungeonsGeneratorTemplates : MonoBehaviour
    {
        public static DungeonsGeneratorTemplates instance;

        [Header("Rooms Prefabs")]
        [SerializeField] public GameObject[] topRooms;
        [SerializeField] public GameObject[] bottomRooms;
        [SerializeField] public GameObject[] rightRooms;
        [SerializeField] public GameObject[] leftRooms;
        [SerializeField] public GameObject closedRoom;

        [Header("List Rooms")]
        public List<GameObject> rooms;

        [Header("Generation Elements Room")]
        public GameObject finalEnemy;
        public GameObject simpleEnemy;
        public GameObject treasure;  // Agrega la referencia al objeto del tesoro
        public GameObject key;       // Agrega la referencia al objeto de la llave
        public GameObject fogWall;


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

        private void Start()
        {
            Invoke("SpawnObjects", 3f);
        }

        public void SpawnObjects()
        {
            GameObject lastRoom = rooms[rooms.Count - 1];
            Transform fogWallGenerationTransform = lastRoom.transform.Find("Fog Wall Generation");

            if (fogWallGenerationTransform != null)
            {
                Vector3 positionFogwall = fogWallGenerationTransform.position;

                // Instanciar tu prefab de pared de niebla en esa posición
                GameObject fogWallInstance = Instantiate(fogWall, positionFogwall, fogWallGenerationTransform.rotation);
                // Asegurarse de que el objeto sea registrado en la red
                NetworkObject networkObject = fogWallInstance.GetComponent<NetworkObject>();
                if (networkObject != null && NetworkManager.Singleton.IsServer) // Solo el servidor puede hacer Spawn()
                {
                    networkObject.Spawn();
                }
                // Instanciar el enemigo final
                Instantiate(finalEnemy, lastRoom.transform.position, Quaternion.identity);
            }
            else
            {
                Debug.LogWarning("Fog Wall Generation no encontrado en la última habitación.");
            }

            // Desactivar "Area IA Spawner" en la última habitación
            Transform areaIASpawner = lastRoom.transform.Find("Area IA Spawner");
            if (areaIASpawner != null)
            {
                areaIASpawner.gameObject.SetActive(false);
                Debug.Log("Area IA Spawner desactivado en la última habitación.");
            }
            else
            {
                Debug.LogWarning("Area IA Spawner no encontrado en la última habitación.");
            }

            // Instanciar la llave en una habitación aleatoria, lejos de la última habitación
            int keyRoomIndex;
            do
            {
                keyRoomIndex = Random.Range(0, rooms.Count - 1);
            } while (Vector3.Distance(rooms[keyRoomIndex].transform.position, rooms[rooms.Count - 1].transform.position) < 5);

            Instantiate(key, rooms[keyRoomIndex].transform.position, Quaternion.identity);
        }
    }
}
