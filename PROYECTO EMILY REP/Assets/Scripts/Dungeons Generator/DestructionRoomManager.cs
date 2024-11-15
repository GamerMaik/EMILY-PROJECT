using UnityEngine;

namespace KC
{
    public class DestructionRoomManager : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag("ClosedRoom"))
                return;

            Destroy(other.gameObject);
        }
    }
}
