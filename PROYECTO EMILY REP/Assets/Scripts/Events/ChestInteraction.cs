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

        [Header("Question Configuration")]
        [Range(0, 100)]
        [SerializeField] private int questionChancePercentage = 20;
        [SerializeField] private TakeDamageQuestionIncorrectEffect incorrectAnswerDamageEffect;

        public override void Interact(PlayerManager player)
        {
            base.Interact(player);
            int randomValue = Random.Range(0, 100); // Generar un número aleatorio entre 0 y 99

            if (questionChancePercentage == 0)
            {
                StartCoroutine(OpenChestAndDropItems());
            }
            else if (randomValue < questionChancePercentage)
            {
                ShowRandomQuestionsManager.instance.LoadRandomQuestion(OnAnswerReceived);
            }

            
        }

        private void OnAnswerReceived(bool isCorrect)
        {
            if (isCorrect)
            {
                Debug.Log("Respuesta correcta");
                WorldLevelManager.instance.AddCountQuestionsAnswers(true);
                CameraSlowMotionManager.instance.DeactivateSlowMotion();
                CursorManager.instance.HideCursor();
                StartCoroutine(OpenChestAndDropItems());
                ActiveVFXAndSFXOpen(); 
            }
            else
            {
                interactableCollider.enabled = true;
                Debug.Log("Respuesta incorrecta, aplicando daño al personaje.");
                WorldLevelManager.instance.AddCountQuestionsAnswers(false);
                CursorManager.instance.HideCursor();
                CameraSlowMotionManager.instance.DeactivateSlowMotion();
                ApplyDamageToPlayer();
            }
        }

        private void ApplyDamageToPlayer()
        {
            PlayerManager player = NetworkManager.Singleton.LocalClient.PlayerObject.GetComponent<PlayerManager>();
            if (player != null && incorrectAnswerDamageEffect != null)
            {
                // Configurar el daño si es necesario
                incorrectAnswerDamageEffect.healthDamage = 50; // Puedes ajustar el daño dinámicamente

                // Procesar el efecto en el jugador
                player.characterEffectsManager.ProccessInstantEffect(incorrectAnswerDamageEffect);
                player.characterEffectsManager.PlayBloodSplatterVFX(player.transform.position);
                AudioClip physicalDamageSFX = WorldSoundFXManager.instance.ChooseRandomSFXFromArray(WorldSoundFXManager.instance.physicalDamageSFX);
                player.characterSoundFXManager.PlaySoundFX(physicalDamageSFX);
                player.characterSoundFXManager.PlayDamageGrunt();
            }
        }
        public void ActiveVFXAndSFXOpen()
        {
            openVFX.SetActive(true);
        }

        public void DropItemChest()
        {
            int itemsToDrop = 2; // Cantidad de ítems a dropear
            for (int i = 0; i < itemsToDrop; i++)
            {
                Item generatedItem = droppableItems[Random.Range(0, droppableItems.Length)];

                if (generatedItem == null)
                    continue;

                GameObject itemPickUpInteractableGameObject = Instantiate(WorldItemDatabase.Instance.pickUpItemsPrefab);
                PickUpItemInteractable pickUpItemInteractable = itemPickUpInteractableGameObject.GetComponent<PickUpItemInteractable>();
                itemPickUpInteractableGameObject.GetComponent<NetworkObject>().Spawn();
                pickUpItemInteractable.itemID.Value = generatedItem.itemID;

                Rigidbody rb = itemPickUpInteractableGameObject.AddComponent<Rigidbody>();
                rb.useGravity = true;
                rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

                SphereCollider sphereCollider = itemPickUpInteractableGameObject.AddComponent<SphereCollider>();
                sphereCollider.radius = 0.1f; // Ajustar el tamaño según sea necesario
                sphereCollider.isTrigger = false;

                // Ajustar fuerza de lanzamiento con dirección lateral
                Vector3 launchDirection = Vector3.up * launchForce +
                                           transform.right * Random.Range(-randomHorizontalForce, randomHorizontalForce);
                rb.AddForce(launchDirection, ForceMode.Impulse);

                // Posicionar el ítem en el mismo lugar que el cofre
                itemPickUpInteractableGameObject.transform.position = transform.position;
            }
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
