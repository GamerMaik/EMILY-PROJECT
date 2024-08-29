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
                Debug.Log("Tenemos un objetivo");
                return this;
            }
            else
            {
                aiCharacter.aICharacterCombatManager.FindTargetViaLineOffSight(aiCharacter);
                Debug.Log("Buscando  un objetivo");
                return this;
            }

        }
    }
}
