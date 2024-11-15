using System.Collections.Generic;
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

        public List<GameObject> rooms;

        public GameObject finalEnemy;
        public GameObject simpleEnemy;
        public GameObject treasure;  // Agrega la referencia al objeto del tesoro
        public GameObject key;       // Agrega la referencia al objeto de la llave

        private void Start()
        {
            Invoke("SpawnObjects", 3f);
        }

        void SpawnObjects()
        {
            // Instanciar el jefe final en la última habitación
            Instantiate(finalEnemy, rooms[rooms.Count - 1].transform.position, Quaternion.identity);

            // Instanciar enemigos en todas las salas excepto la última
            for (int i = 0; i < rooms.Count - 1; i++)
            {
                Instantiate(simpleEnemy, rooms[i].transform.position, Quaternion.identity);
            }

            // Instanciar tesoros en dos habitaciones aleatorias
            for (int i = 0; i < 3; i++)
            {
                int randIndex = Random.Range(0, rooms.Count - 1);
                Instantiate(treasure, rooms[randIndex].transform.position, Quaternion.identity);
            }

            // Instanciar la llave en una habitación aleatoria, lejos de la última habitación
            int keyRoomIndex;
            do
            {
                keyRoomIndex = Random.Range(0, rooms.Count - 1);
            } while (Vector3.Distance(rooms[keyRoomIndex].transform.position, rooms[rooms.Count - 1].transform.position) < 5); // Ajustar la distancia a lo que querramos

            Instantiate(key, rooms[keyRoomIndex].transform.position, Quaternion.identity);
        }
    }
}
