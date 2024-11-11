using Unity.Netcode;
using UnityEngine;

namespace KC
{
    public class AICharacterNetworkManager : CharacterNetworkManager
    {
        AICharacterManager aiCharacter;
        protected override void Awake()
        {
            base.Awake();

            aiCharacter =  GetComponent<AICharacterManager>();
        }
        public override void OnIsDeadChange(bool oldStatus, bool newStatus)
        {
            base.OnIsDeadChange(oldStatus, newStatus);

            aiCharacter.aICharacterInventoryManager.DropItem();

        }
    }
}
