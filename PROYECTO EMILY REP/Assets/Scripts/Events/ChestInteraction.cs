using System.Collections;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace KC
{
    public class ChestInteraction : Interactable
    {
        [Header("VFX")]
        [SerializeField] GameObject openVFX;
        [SerializeField] GameObject smokeVFX;

        [Header("Items")]
        [SerializeField] Item[] droppableItems;
        [SerializeField] GameObject chestLid; // Tapa del cofre
        [SerializeField] float lidOpenDuration = 1.5f; // Tiempo para abrir la tapa

        [Header("Physics")]
        [SerializeField] float launchForce = 5f; // Fuerza del lanzamiento hacia arriba
        [SerializeField] float randomHorizontalForce = 2f; // Fuerza horizontal aleatoria

        public override void Interact(PlayerManager player)
        {
            base.Interact(player);
            ActiveVFXAndSFXOpen();
            StartCoroutine(OpenChestAndDropItems());
        }

        public void ActiveVFXAndSFXOpen()
        {
            openVFX.SetActive(true);
        }

        public void DropItemChest()
        {
            if (droppableItems == null || droppableItems.Length == 0)
            {
                Debug.LogWarning("No hay ítems en la lista de dropeo.");
                return;
            }

            Item generatedItem = droppableItems[Random.Range(0, droppableItems.Length)];

            if (generatedItem == null)
                return;

            GameObject itemPickUpInteractableGameObject = Instantiate(WorldItemDatabase.Instance.pickUpItemsPrefab);
            PickUpItemInteractable pickUpItemInteractable = itemPickUpInteractableGameObject.GetComponent<PickUpItemInteractable>();

            // Asigna el ID del ítem y la posición de spawn
            pickUpItemInteractable.itemID.Value = generatedItem.itemID;
            itemPickUpInteractableGameObject.transform.position = transform.position;

            // Añadir Rigidbody y configurar colisiones
            Rigidbody rb = itemPickUpInteractableGameObject.AddComponent<Rigidbody>();
            rb.useGravity = true;
            rb.mass = 0.5f;
            rb.collisionDetectionMode = CollisionDetectionMode.Continuous; // Mejorar detección de colisiones

            SphereCollider sphereCollider = itemPickUpInteractableGameObject.AddComponent<SphereCollider>();
            sphereCollider.radius = 0.3f; // Ajustar el tamaño según sea necesario
            sphereCollider.isTrigger = false; // Asegurarnos de que sea un Collider sólido
            

            // Fuerza de lanzamiento
            Vector3 launchDirection = Vector3.up * launchForce +
                                       Vector3.right * Random.Range(-randomHorizontalForce, randomHorizontalForce);
            rb.AddForce(launchDirection, ForceMode.Impulse);

            // Spawnear el objeto en la red
            itemPickUpInteractableGameObject.GetComponent<NetworkObject>().Spawn();
        }

        public IEnumerator StartItemDropTime()
        {
            yield return new WaitForSeconds(2);
            DropItemChest();
            Destroy(this.gameObject);
        }

        private IEnumerator OpenChestAndDropItems()
        {
            // 1. Abrir la tapa del cofre
            yield return StartCoroutine(OpenChestLid());

            // 2. Activar el efecto de humo
            if (smokeVFX != null)
            {
                smokeVFX.SetActive(true);
            }

            // 3. Esperar un momento antes de dropear los ítems
            yield return new WaitForSeconds(1f);

            // 4. Dropear los ítems
            DropItemChest();

            // 5. Destruir el cofre después de un breve tiempo
            yield return new WaitForSeconds(2f);
            Destroy(this.gameObject);
        }

        private IEnumerator OpenChestLid()
        {
            if (chestLid == null)
            {
                Debug.LogError("La tapa del cofre no está asignada.");
                yield break;
            }

            Quaternion initialRotation = chestLid.transform.localRotation;
            Quaternion finalRotation = Quaternion.Euler(-120f, 0f, 0f); // Rotación final

            float elapsedTime = 0f;
            while (elapsedTime < lidOpenDuration)
            {
                elapsedTime += Time.deltaTime;
                chestLid.transform.localRotation = Quaternion.Slerp(initialRotation, finalRotation, elapsedTime / lidOpenDuration);
                yield return null;
            }

            chestLid.transform.localRotation = finalRotation;
        }
    }
}
