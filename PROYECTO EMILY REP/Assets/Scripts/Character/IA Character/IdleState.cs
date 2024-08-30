using UnityEngine;
using UnityEngine.TextCore.Text;

namespace KC
{
    [CreateAssetMenu(menuName = "A.I/States/Idle")]
    public class IdleState : AIState
    {
        public override AIState Tick(AICharacterManager aiCharacter)
        {
            if (aiCharacter.characterCombatManager.currentTarget != null)
            {
                return SwitchState(aiCharacter, aiCharacter.pursueTarget);
            }
            else
            {
                aiCharacter.aICharacterCombatManager.FindTargetViaLineOffSight(aiCharacter);
                return this;
            }

        }
    }
}
