using UnityEngine;

namespace KC
{
    public class RoomSpawnerManager : MonoBehaviour
    {
        public static RoomSpawnerManager instance;
        [Header("Rooms Configuration")]
        [SerializeField] public int openSide;
        private DungeonsGeneratorTemplates templates;
        private int randomRoom;
        private bool spawnedComplete = false;

        [Header("Dungeon Configuration")]
        [SerializeField] private int minimumRooms = 10; // Mínimo de habitaciones requeridas
        [SerializeField] private int maxAttempts = 60; // Máximo de intentos para generar habitaciones
        private static int totalRoomsGenerated = 0; // Contador global
        private static int attempts = 0; // Intentos actuales

        private void Start()
        {
            templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<DungeonsGeneratorTemplates>();
            Invoke("SpawnRooms", 0.1f);
        }

        public void SpawnRooms()
        {
            if (!spawnedComplete)
            {
                // Evitar que se generen más habitaciones si se exceden los intentos máximos
                if (attempts >= maxAttempts)
                {
                    Debug.LogWarning("Se alcanzó el máximo de intentos de generación.");
                    return;
                }

                attempts++;

                // Generar la habitación en función del lado abierto
                if (openSide == 1)
                {
                    randomRoom = Random.Range(0, templates.bottomRooms.Length);
                    Instantiate(templates.bottomRooms[randomRoom], transform.position, templates.bottomRooms[randomRoom].transform.rotation);
                }
                else if (openSide == 2)
                {
                    randomRoom = Random.Range(0, templates.topRooms.Length);
                    Instantiate(templates.topRooms[randomRoom], transform.position, templates.topRooms[randomRoom].transform.rotation);
                }
                else if (openSide == 3)
                {
                    randomRoom = Random.Range(0, templates.leftRooms.Length);
                    Instantiate(templates.leftRooms[randomRoom], transform.position, templates.leftRooms[randomRoom].transform.rotation);
                }
                else if (openSide == 4)
                {
                    randomRoom = Random.Range(0, templates.rightRooms.Length);
                    Instantiate(templates.rightRooms[randomRoom], transform.position, templates.rightRooms[randomRoom].transform.rotation);
                }

                totalRoomsGenerated++;
                spawnedComplete = true;
                //WorldDungeonManager.instance.GenerateNavmesh();

                // Si se alcanzó el número mínimo de habitaciones, cierra el proceso de generación
                if (totalRoomsGenerated >= minimumRooms)
                {
                    Debug.Log("Se alcanzó el número mínimo de habitaciones generadas.");
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("SpawnPoint"))
            {
                RoomSpawnerManager otherSpawner = other.GetComponent<RoomSpawnerManager>();
                if (otherSpawner != null && otherSpawner.spawnedComplete == false && spawnedComplete == false)
                {
                    // Si aún no se ha alcanzado el mínimo, no cierres la mazmorra
                    if (totalRoomsGenerated < minimumRooms)
                    {
                        return;
                    }

                    Instantiate(templates.closedRoom, transform.position, Quaternion.identity);
                    Destroy(gameObject);
                }

                spawnedComplete = true;
            }
        }
    }
}
