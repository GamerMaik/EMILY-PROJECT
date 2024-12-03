using UnityEngine;
using Unity.Netcode;
using System.Collections;

namespace KC
{
    public class SiteOfGraceInteractable : Interactable
    {
        [Header("Site of Grace Info")]
        [SerializeField] int siteOfGraceID;
        public NetworkVariable<bool> isActivated = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

        [Header("VFX")]
        [SerializeField] GameObject activedParticles;

        [Header("Interaction Text")]
        [SerializeField] string unactivatedInteractionText = "Restaurar el sitio de gracia";
        [SerializeField] string activatedInteractionText = "Descansar";

        [Header("Dialog Settings")]
        [SerializeField] DialogObjectText initialDialog;
        [SerializeField] DialogObjectText activatedDialog;
        private int currentDialogIndex = 0;
        private bool isShowingDialog = false;
        private DialogObjectText currentDialog;

        protected override void Start()
        {
            base.Start();

            if (IsOwner)
            {

                if (WorldSaveGameManager.instance.currentCharacterData.sitesOfGrace.ContainsKey(siteOfGraceID))
                {
                    isActivated.Value = WorldSaveGameManager.instance.currentCharacterData.sitesOfGrace[siteOfGraceID];
                }
                else
                {
                    isActivated.Value = false;
                }
            }

            interactableText = isActivated.Value ? activatedInteractionText : unactivatedInteractionText;
        }

        private void ShowNextDialog(PlayerManager player)
        {
            // Si se han mostrado todas las líneas del diálogo, finaliza.
            if (currentDialog == null || currentDialogIndex >= currentDialog.dialogLines.Count)
            {
                EndDialog(player);
                return;
            }

            // Muestra la línea de diálogo actual.
            PlayerUIManager.instance.playerUIPopUpManager.SendDialogPopUp(currentDialog.dialogLines[currentDialogIndex]);

            // Detén cualquier audio en curso antes de reproducir el siguiente.
            player.characterSoundFXManager.audioSource.Stop();

            // Reproduce el audio correspondiente a esta línea, si existe.
            if (currentDialog.dialogAudioClips != null &&
                currentDialog.dialogAudioClips.Count > currentDialogIndex &&
                currentDialog.dialogAudioClips[currentDialogIndex] != null)
            {
                player.characterSoundFXManager.PlaySoundFX(
                    currentDialog.dialogAudioClips[currentDialogIndex]);
            }

            // Avanza al siguiente índice para la próxima interacción.
            currentDialogIndex++;

            // Habilita el collider para que el jugador pueda interactuar nuevamente.
            interactableCollider.enabled = true;
        }
        private void EndDialog(PlayerManager player)
        {
            isShowingDialog = false;
            PlayerUIManager.instance.playerUIPopUpManager.closeAllPopUpWindows();

            if (!isActivated.Value)
            {
                RestoreSiteOfGrace(player);
            }
            else
            {
                RestAtSiteOfGrace(player);
            }
        }

        private void StartDialog(PlayerManager player, DialogObjectText dialog)
        {
            isShowingDialog = true;
            currentDialog = dialog;
            currentDialogIndex = 0;
            ShowNextDialog(player);
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            if (!IsOwner)
                OnIsActivatedChanged(false, isActivated.Value);

            if (IsOwner)
            {
                if (!WorldSaveGameManager.instance.currentCharacterData.sitesOfGrace.ContainsKey(siteOfGraceID))
                {
                    WorldSaveGameManager.instance.currentCharacterData.sitesOfGrace.Add(siteOfGraceID, isActivated.Value);
                    isActivated.OnValueChanged += OnIsActivatedChanged;
                }
                else
                {
                    isActivated.Value = WorldSaveGameManager.instance.currentCharacterData.sitesOfGrace[siteOfGraceID];
                    isActivated.OnValueChanged += OnIsActivatedChanged;
                }
            }
        }
        public override void OnNetworkDespawn()
        {
            base.OnNetworkDespawn();
            isActivated.OnValueChanged -= OnIsActivatedChanged;
        }

        private void RestoreSiteOfGrace(PlayerManager player)
        {
            isActivated.Value = true;

            if (WorldSaveGameManager.instance.currentCharacterData.sitesOfGrace.ContainsKey(siteOfGraceID))
                WorldSaveGameManager.instance.currentCharacterData.sitesOfGrace.Remove(siteOfGraceID);

            WorldSaveGameManager.instance.currentCharacterData.sitesOfGrace.Add(siteOfGraceID, true);
            WorldSaveGameManager.instance.SaveGame();
            player.playerAnimatorManager.PlayerTargetActionAnimation("Activate_Site_Of_Grace_01", true);

            PlayerUIManager.instance.playerUIPopUpManager.SendbGraceRestoredPopUp("SITIO DE GRACIA RESTAURADO");

            StartCoroutine(WaitForAnimationAndPopUpThenRestoreCollider());
        }

        private void RestAtSiteOfGrace(PlayerManager player)
        {
            Debug.Log("Descansar");
            interactableCollider.enabled = true;
            player.playerNetworkManager.currentHealth.Value = player.playerNetworkManager.maxHealth.Value;
            player.playerNetworkManager.currentStamina.Value = player.playerNetworkManager.maxStamina.Value;

            //WorldAIManager.instance.ResetAllCharacters();
        }

        private IEnumerator WaitForAnimationAndPopUpThenRestoreCollider()
        {
            yield return new WaitForSeconds(3);

            interactableCollider.enabled =true;
        }

        private void OnIsActivatedChanged(bool oldStatus, bool newStatus)
        {
            if (isActivated.Value)
            {
                //Repdroducir algunos sonidos o efectos
                activedParticles.SetActive(true);
                interactableText = activatedInteractionText;
            }
            else
            {
                interactableText = unactivatedInteractionText;
            }
        }

        public override void Interact(PlayerManager player)
        {
            base.Interact(player);

            if (isShowingDialog)
            {
                ShowNextDialog(player);
            }
            else
            {
                if (!isActivated.Value)
                {
                    StartDialog(player, initialDialog);
                }
                else
                {
                    StartDialog(player, activatedDialog);
                }
            }
        }
    }
}
