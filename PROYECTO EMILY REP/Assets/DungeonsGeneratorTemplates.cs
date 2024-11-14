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
        [SerializeField] public GameObject closedRooms;
    }
}
