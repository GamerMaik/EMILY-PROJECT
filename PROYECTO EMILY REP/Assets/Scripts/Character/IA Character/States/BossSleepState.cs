using UnityEngine;

namespace KC
{
    [CreateAssetMenu(menuName = "A.I/States/Boss Sleep")]
    public class BossSleepState : AIState
    {
        public override AIState Tick(AICharacterManager aiCharacter)
        {
            return base.Tick(aiCharacter);
        }
    }
}
