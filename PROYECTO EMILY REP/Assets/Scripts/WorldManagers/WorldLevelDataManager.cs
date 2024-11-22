using System.Collections;
using UnityEngine;

namespace KC
{
    public class WorldLevelDataManager : Interactable
    {
        [Header("Level Configuration")]
        [SerializeField] GameObject WorldQuestionManager;
        [SerializeField] LevelType levelType;

        protected override void Start()
        {
            base.Start();
            interactableText = "Cargar nivel de " + levelType.ToString();
        }

        public override void Interact(PlayerManager player)
        {
            base.Interact(player);
            LoadQuestionLevel(levelType, player);
        }

        public void LoadQuestionLevel(LevelType typeLoadLevel, PlayerManager player)
        {
            WorldSessionManager.Instance.GetQuestionsLevleBasedType(typeLoadLevel);
            Debug.Log("Se cargaron las preguntas de: " + typeLoadLevel);
            player.playerAnimatorManager.PlayerTargetActionAnimation("Level_Selector_01", true);

            StartCoroutine(WaitForAnimationAndRestoreColliderLevelSelector());
        }

        private IEnumerator WaitForAnimationAndRestoreColliderLevelSelector()
        {
            yield return new WaitForSeconds(3);

            interactableCollider.enabled = true;
        }
    }
}
