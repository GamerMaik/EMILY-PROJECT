using UnityEngine;

namespace KC
{
    public class AttackState : AIState
    {
        [Header("Current Attack")]
        [HideInInspector] public AICharacterAttackAction currentAttack;
        [HideInInspector] public bool willPerformCombo = false;

        [Header("State Flags")]
        protected bool hasPerformedAttack = false;
        protected bool hasPerformedCombo = false;

        [Header("Pivot After Attack")]
        [SerializeField] protected bool pivotAfterAttack = false;

        public override AIState Tick(AICharacterManager aiCharacter)
        {
            if (aiCharacter.aICharacterCombatManager.currentTarget == null)
                return SwitchState(aiCharacter, aiCharacter.idle);

            if (aiCharacter.aICharacterCombatManager.currentTarget.isDead.Value)
                return SwitchState(aiCharacter, aiCharacter.idle);

            aiCharacter.characterAnimatorManager.UpdateAnimatorMovementParameters(0, 0, false);
            //Realizar un combo

            if(willPerformCombo && !hasPerformedCombo)
            {
                if(currentAttack.comboAction != null)
                {
                    hasPerformedCombo = true;
                    currentAttack.comboAction.AttempToperformAction(aiCharacter);
                }
            }

            if (!hasPerformedAttack)
            {

                if (aiCharacter.aICharacterCombatManager.actionRecoveryTimer > 0)
                    return this;

                if (aiCharacter.isPerformingAction)
                    return this;

                PerformAttack(aiCharacter);

                return this;
            }

            if (pivotAfterAttack)
                aiCharacter.aICharacterCombatManager.PivotTowardsTarget(aiCharacter);

            return SwitchState(aiCharacter, aiCharacter.combatStance);
        }

        protected void  PerformAttack(AICharacterManager aiCharacter)
        {

            hasPerformedAttack = true;
            currentAttack.AttempToperformAction(aiCharacter);
            aiCharacter.aICharacterCombatManager.actionRecoveryTimer = currentAttack.actionRecoveryTime;
        }
    }
}
