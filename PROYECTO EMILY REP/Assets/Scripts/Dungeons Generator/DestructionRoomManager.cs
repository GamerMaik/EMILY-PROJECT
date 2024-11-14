using UnityEngine;

namespace KC
{
    public class DestructionRoomManager : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "ClosedRoom")
            {
                Destroy(other.gameObject);
            }
        }
    }
}
