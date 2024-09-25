using UnityEngine;
using UnityEngine.TextCore.Text;

namespace KC
{
    public class AICharacterLocomotionManager : CharacterLocomotionManager
    {
        //[HideInInspector] CharacterManager character;
        //protected override void Awake()
        //{
        //    base.Awake();
        //    character = GetComponent<CharacterManager>();
        //    if (character == null)
        //    {
        //        Debug.LogError("CharacterManager no está asignado");
        //    }
        //}
        public void RotateTowarsAgent(AICharacterManager aiCharacter)
        {
            if (aiCharacter.aiCharacterNetworkManager.isMoving.Value)
            {
                aiCharacter.transform.rotation = aiCharacter.navMeshAgent.transform.rotation;
            }
        }
    }
}
