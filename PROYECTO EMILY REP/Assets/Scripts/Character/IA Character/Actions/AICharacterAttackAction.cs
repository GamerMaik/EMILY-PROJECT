using UnityEngine;

namespace KC
{
    [CreateAssetMenu(menuName = "A.I/Actions/Attack")]
    public class AICharacterAttackAction : ScriptableObject
    {
        [Header("Attack Animation")]
        [SerializeField] private string attackAnimation;

        [Header("Combo Action")]
        public AICharacterAttackAction comboAction;
        [Header("Action Values")]
        [SerializeField] AttackType attackType;
        public int attackWeight = 50;

        public float actionRecoveryTime = 1.5f; //El tiempo que le toma a los personajes para volver a realizar otro ataque
        public float minimumAttackAngle = -35;
        public float maximumAttackAngle = 35;
        public float minimumAttackDistance = 0;
        public float maximumAttackDistance = 2;

        public void AttempToperformAction(AICharacterManager aiCharacter)
        {
            aiCharacter.characterAnimatorManager.PlayerTargetAttackActionAnimation(attackType,attackAnimation, true);
        }
    }
}
