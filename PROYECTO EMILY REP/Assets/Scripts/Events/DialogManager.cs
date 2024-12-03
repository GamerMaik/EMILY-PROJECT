using System.Collections;
using Unity.Netcode;
using UnityEngine;

namespace KC
{
    public class DialogManager : MonoBehaviour
    {
        public static DialogManager instance;

        [Header("Audio Source")]
        [SerializeField] private AudioSource audioSource;
        private Coroutine dialogCoroutine;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                AudioSource playerAudioSource = NetworkManager.Singleton.LocalClient.PlayerObject.GetComponent<AudioSource>();
                audioSource = playerAudioSource;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void StartDialog(DialogObjectText dialogObject, System.Action onDialogComplete = null)
        {
            if (dialogCoroutine != null)
            {
                StopCoroutine(dialogCoroutine);
            }

            dialogCoroutine = StartCoroutine(HandleDialog(dialogObject, onDialogComplete));
        }

        private IEnumerator HandleDialog(DialogObjectText dialogObject, System.Action onDialogComplete)
        {
            for (int i = 0; i < dialogObject.dialogLines.Count; i++)
            {
                // Mostrar el texto en el UI
                PlayerUIManager.instance.playerUIPopUpManager.SendDialogPopUp(dialogObject.dialogLines[i]);

                // Reproducir el audio correspondiente, si existe
                if (dialogObject.dialogAudioClips != null &&
                    i < dialogObject.dialogAudioClips.Count &&
                    dialogObject.dialogAudioClips[i] != null)
                {
                    audioSource.Stop();
                    audioSource.PlayOneShot(dialogObject.dialogAudioClips[i]);

                    // Esperar a que termine el audio
                    yield return new WaitWhile(() => audioSource.isPlaying);
                }
                else
                {
                    // Si no hay audio, esperar un tiempo predeterminado
                    yield return new WaitForSeconds(2f); // Tiempo predeterminado entre líneas
                }
            }

            // Cerrar el pop-up al final del diálogo
            PlayerUIManager.instance.playerUIPopUpManager.closeAllPopUpWindows();

            // Llamar al callback (si existe)
            onDialogComplete?.Invoke();
        }

        public void StopDialog()
        {
            if (dialogCoroutine != null)
            {
                StopCoroutine(dialogCoroutine);
            }
            PlayerUIManager.instance.playerUIPopUpManager.closeAllPopUpWindows();
            audioSource.Stop();
        }
    }
}
