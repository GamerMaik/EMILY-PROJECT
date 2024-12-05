using System.Collections;
using Unity.Netcode;
using UnityEngine;

namespace KC
{
    public class SiteInitialDungeon : Interactable
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
        [SerializeField] DialogObjectText funDialog;

        [Header("Colliders Doors Initial")]
        [SerializeField] Collider Door1;
        [SerializeField] Collider Door2;
        [SerializeField] Collider Door3;

        [Header("Chest Initial")]
        [SerializeField] GameObject chestInitial;
        private bool isShowingDialog = false;

        [Range(0, 100)]
        [SerializeField] private int funDialogChancePercentage = 20;
        protected override void Start()
        {
            base.Start();
            CursorManager.instance.HideCursor();
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

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            // Escuchar cambios en isActivated
            isActivated.OnValueChanged += OnIsActivatedChanged;

            if (IsOwner)
            {
                if (!WorldSaveGameManager.instance.currentCharacterData.sitesOfGrace.ContainsKey(siteOfGraceID))
                {
                    WorldSaveGameManager.instance.currentCharacterData.sitesOfGrace.Add(siteOfGraceID, isActivated.Value);
                }
                else
                {
                    isActivated.Value = WorldSaveGameManager.instance.currentCharacterData.sitesOfGrace[siteOfGraceID];
                }
            }
        }

        public override void OnNetworkDespawn()
        {
            base.OnNetworkDespawn();

            // Cancelar la suscripción
            isActivated.OnValueChanged -= OnIsActivatedChanged;
        }

        public override void Interact(PlayerManager player)
        {
            base.Interact(player);

            if (isShowingDialog) return;

            isShowingDialog = true;

            if (!isActivated.Value)
            {
                DialogManager.instance.StartDialog(initialDialog, () =>
                {
                    RestoreSiteOfGrace(player);
                    isShowingDialog = false;
                });
            }
            else
            {
                int randomValue = Random.Range(0, 100);
                if (randomValue < funDialogChancePercentage)
                {
                    DialogManager.instance.StartDialog(funDialog, () =>
                    {
                        RestAtSiteOfGrace(player);
                        isShowingDialog = false;
                    });
                }
                else
                {
                    DialogManager.instance.StartDialog(activatedDialog, () =>
                    {
                        RestAtSiteOfGrace(player);
                        isShowingDialog = false;
                    });
                }
            }
        }

        private void RestoreSiteOfGrace(PlayerManager player)
        {
            isActivated.Value = true;
            
            Door1.enabled = true;
            Door2.enabled = true;
            Door3.enabled = true;
            if (WorldSaveGameManager.instance.currentCharacterData.sitesOfGrace.ContainsKey(siteOfGraceID))
                WorldSaveGameManager.instance.currentCharacterData.sitesOfGrace.Remove(siteOfGraceID);

            WorldSaveGameManager.instance.currentCharacterData.sitesOfGrace.Add(siteOfGraceID, true);
            WorldSaveGameManager.instance.SaveGame();
            player.playerAnimatorManager.PlayerTargetActionAnimation("Activate_Site_Of_Grace_01", true);
            PlayerUIManager.instance.playerUIPopUpManager.SendbGraceRestoredPopUp("SAL DE LA MAZMORRA");

            StartCoroutine(WaitForAnimationAndPopUpThenRestoreCollider());
        }

        private void RestAtSiteOfGrace(PlayerManager player)
        {
            Door1.enabled = true;
            Door2.enabled = true;
            Door3.enabled = true;

            interactableCollider.enabled = true;
            player.playerNetworkManager.currentHealth.Value = player.playerNetworkManager.maxHealth.Value;
            player.playerNetworkManager.currentStamina.Value = player.playerNetworkManager.maxStamina.Value;
            if (chestInitial.transform.childCount > 0)
            {
                chestInitial.SetActive(true);
            }
            //WorldAIManager.instance.ResetAllCharacters();
        }

        private IEnumerator WaitForAnimationAndPopUpThenRestoreCollider()
        {
            yield return new WaitForSeconds(3);
            interactableCollider.enabled = true;
            chestInitial.SetActive(true);
        }

        private void OnIsActivatedChanged(bool oldStatus, bool newStatus)
        {
            if (newStatus)
            {
                // Mostrar partículas activadas
                activedParticles.SetActive(true);
                interactableText = activatedInteractionText;
            }
            else
            {
                // Ocultar partículas desactivadas
                activedParticles.SetActive(false);
                interactableText = unactivatedInteractionText;
            }
        }
    }
}
