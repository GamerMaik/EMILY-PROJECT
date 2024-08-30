using UnityEngine;

namespace KC
{
    public class AICharacterLocomotionManager : CharacterLocomotionManager
    {
        public void RotateTowarsAgent(AICharacterManager aiCharacter)
        {
            if (aiCharacter.aiCharacterNetworkManager.isMoving.Value)
            {
                aiCharacter.transform.rotation = aiCharacter.navMeshAgent.transform.rotation;
            }
        }
    }
}
