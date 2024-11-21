using System.Collections;
using UnityEngine;

namespace KC
{
    public class OpenDoorDungeonRoom : Interactable
    {
        [Header("Door Configuration")]
        [SerializeField] private float speed = 2f; // Velocidad de apertura/cierre
        [SerializeField] private float angleClosed = 0f; // �ngulo cuando est� cerrada
        [SerializeField] private float angleOpened = 90f; // �ngulo cuando est� abierta
        private bool isOpen = false; // Estado actual de la puerta
        private Coroutine doorCoroutine; // Referencia a la corrutina activa

        protected override void Start()
        {
            base.Start();
            // Asegurar que la puerta est� en el �ngulo cerrado inicial
            transform.rotation = Quaternion.Euler(0, angleClosed, 0);
        }

        public override void Interact(PlayerManager player)
        {
            base.Interact(player);

            if (doorCoroutine != null)
            {
                StopCoroutine(doorCoroutine);
            }

            if (isOpen)
            {
                interactableText = "Abrir";
                doorCoroutine = StartCoroutine(RotateDoor(angleOpened, angleClosed));
            }
            else
            {
                interactableText = "Cerrar";
                doorCoroutine = StartCoroutine(RotateDoor(angleClosed, angleOpened));
            }

            isOpen = !isOpen; // Cambiar el estado de la puerta
        }

        private IEnumerator RotateDoor(float startAngle, float endAngle)
        {
            float elapsedTime = 0f;
            float duration = Mathf.Abs(endAngle - startAngle) / speed; // Duraci�n basada en la diferencia de �ngulos y la velocidad
            Quaternion initialRotation = Quaternion.Euler(0, startAngle, 0);
            Quaternion targetRotation = Quaternion.Euler(0, endAngle, 0);

            while (elapsedTime < duration)
            {
                transform.rotation = Quaternion.Lerp(initialRotation, targetRotation, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            interactableCollider.enabled = true;
            transform.rotation = targetRotation; // Asegurar que termine exactamente en el �ngulo deseado
        }
    }
}
