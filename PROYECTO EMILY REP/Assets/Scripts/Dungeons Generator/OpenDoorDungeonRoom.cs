using System.Collections;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.TextCore.Text;

namespace KC
{
    public class OpenDoorDungeonRoom : Interactable
    {
        [Header("Door Configuration")]
        [SerializeField] private float speed = 2f;
        [SerializeField] private float angleClosed = 0f;
        [SerializeField] private float angleOpened = 90f;
        [SerializeField] private int questionChancePercentage = 50; // Probabilidad de mostrar pregunta
        private bool isOpen = false;
        private Coroutine doorCoroutine;

        [Header("Character Effects")]
        [SerializeField] private TakeDamageQuestionIncorrectEffect incorrectAnswerDamageEffect;

        protected override void Start()
        {
            base.Start();
            transform.rotation = Quaternion.Euler(0, angleClosed, 0);
        }

        public override void Interact(PlayerManager player)
        {
            base.Interact(player);

            if (Random.Range(0, 100) < questionChancePercentage)
            {
                //Debug.Log("Mostrando pregunta para abrir la puerta.");
                ShowRandomQuestionsManager.instance.LoadRandomQuestion(OnAnswerReceived);
            }
            else
            {
                ToggleDoor();
            }
        }

        private void OnAnswerReceived(bool isCorrect)
        {
            if (isCorrect)
            {
                Debug.Log("Respuesta correcta, abriendo la puerta.");
                CameraSlowMotionManager.instance.DeactivateSlowMotion();
                CursorManager.instance.HideCursor();
                ToggleDoor();
            }
            else
            {
                Debug.Log("Respuesta incorrecta, aplicando daño al personaje.");
                CameraSlowMotionManager.instance.DeactivateSlowMotion();
                CursorManager.instance.HideCursor();
                interactableCollider.enabled = true;
                ApplyDamageToPlayer();
            }
        }

        private void ToggleDoor()
        {
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

            isOpen = !isOpen;
        }

        private IEnumerator RotateDoor(float startAngle, float endAngle)
        {
            float elapsedTime = 0f;
            float duration = Mathf.Abs(endAngle - startAngle) / speed;
            Quaternion initialRotation = Quaternion.Euler(0, startAngle, 0);
            Quaternion targetRotation = Quaternion.Euler(0, endAngle, 0);

            while (elapsedTime < duration)
            {
                transform.rotation = Quaternion.Lerp(initialRotation, targetRotation, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            interactableCollider.enabled = true;
            transform.rotation = targetRotation;
        }
        private void ApplyDamageToPlayer()
        {
            PlayerManager player = NetworkManager.Singleton.LocalClient.PlayerObject.GetComponent<PlayerManager>();
            if (player != null && incorrectAnswerDamageEffect != null)
            {
                // Configurar el daño si es necesario
                incorrectAnswerDamageEffect.healthDamage = 10; // Puedes ajustar el daño dinámicamente

                // Procesar el efecto en el jugador
                player.characterEffectsManager.ProccessInstantEffect(incorrectAnswerDamageEffect);
                player.characterEffectsManager.PlayBloodSplatterVFX(player.transform.position);
                AudioClip physicalDamageSFX = WorldSoundFXManager.instance.ChooseRandomSFXFromArray(WorldSoundFXManager.instance.physicalDamageSFX);
                player.characterSoundFXManager.PlaySoundFX(physicalDamageSFX);
                player.characterSoundFXManager.PlayDamageGrunt();
            }
        }
    }
}
