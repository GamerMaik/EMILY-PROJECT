using UnityEngine;

namespace KC
{
    public class RoomSpawnerManager : MonoBehaviour
    {
        public static RoomSpawnerManager instance;
        [Header("Rooms Configuration")]
        [SerializeField] int openSide;
        private DungeonsGeneratorTemplates templates;
        private int randomRoom;
        private bool spawnedComplete = false;

        private void Start()
        {
            templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<DungeonsGeneratorTemplates>();
            Invoke("SpawnRooms",0.1f);
        }

        public void SpawnRooms()
        {
            if (spawnedComplete == false)
            {
                if (openSide == 1)
                {
                    randomRoom = Random.Range(0, templates.bottomRooms.Length);
                    Instantiate(templates.bottomRooms[randomRoom], transform.position, templates.bottomRooms[randomRoom].transform.rotation);
                }
                else if (openSide == 2)
                {
                    //Top
                    randomRoom = Random.Range(0, templates.topRooms.Length);
                    Instantiate(templates.topRooms[randomRoom], transform.position, templates.topRooms[randomRoom].transform.rotation);
                }
                else if (openSide == 3)
                {
                    //Right
                    randomRoom = Random.Range(0, templates.leftRooms.Length);
                    Instantiate(templates.leftRooms[randomRoom], transform.position, templates.leftRooms[randomRoom].transform.rotation);
                }
                else if (openSide == 4)
                {
                    //Left
                    randomRoom = Random.Range(0, templates.rightRooms.Length);
                    Instantiate(templates.rightRooms[randomRoom], transform.position, templates.rightRooms[randomRoom].transform.rotation);
                }
                spawnedComplete = true;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("SpawnPoint"))
            {
                if (other.GetComponent<RoomSpawnerManager>().spawnedComplete == false && spawnedComplete == false)
                {
                    //Instantiate(templates.closedRooms, transform.position, Quaternion.identity);
                    Destroy(gameObject);
                }

                spawnedComplete = true;
            }

        }
    }
}
