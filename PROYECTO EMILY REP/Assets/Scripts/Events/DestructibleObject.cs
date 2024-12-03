using UnityEngine;

namespace KC
{
    public class DestructibleObject : MonoBehaviour
    {
        [Header("Destructible")]
        [SerializeField] GameObject destructibleObjectModel;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Instantiate(destructibleObjectModel, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
        }
    }
}
