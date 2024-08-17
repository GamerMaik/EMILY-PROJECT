using UnityEngine;
using Unity.Netcode;

namespace KC
{
    public class CharacterCombatManager : NetworkBehaviour
    {
        CharacterManager character;

        [Header("Attack Target")] 
        public CharacterManager currentTarget;

        [Header("Attack Type")]
        public AttackType currentAttackType;

        [Header("Look On Transform")]
        public Transform lookOnTransform;
        

        protected virtual void Awake()
        {
            character = GetComponent<CharacterManager>();
        }

        public virtual void SetTarget(CharacterManager newTarget)
        {
            if (character.IsOwner)
            {
                if(newTarget != null)
                {
                    currentTarget = newTarget;
                    character.characterNetworkManager.currentTargetNetworkObjetID.Value = newTarget.GetComponent<NetworkObject>().NetworkObjectId;
                }
                else
                {
                    currentTarget= null;
                }
            }
        }
    }
}
