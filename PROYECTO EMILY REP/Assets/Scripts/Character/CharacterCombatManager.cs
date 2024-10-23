using UnityEngine;
using Unity.Netcode;

namespace KC
{
    public class CharacterCombatManager : NetworkBehaviour
    {
        protected CharacterManager character;

        [Header("Lats Attack Animation Performed")]
        public string lastAttackAnimationPerformed;

        [Header("Attack Target")] 
        public CharacterManager currentTarget;

        [Header("Attack Type")]
        public AttackType currentAttackType;

        [Header("Look On Transform")]
        public Transform lookOnTransform;

        [Header("Attack Flags")]
        public bool canPerformRollingAttack = false;
        public bool canPerformBackStepAttack = false;
        public bool canBlock = true;

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

        public void EnableIsInvulnerable()
        {
            if (character.IsOwner)
                character.characterNetworkManager.isInvulnerable.Value = true;
        }
        public void DisableIsInvulnerable()
        {
            if (character.IsOwner)
                character.characterNetworkManager.isInvulnerable.Value = false;
        }
        public void EnableCanDoRollingAttack()
        {
            if (character.IsOwner)
                canPerformRollingAttack = true;
        }
        public void DisableCanDoRollingAttack()
        {
            if (character.IsOwner)
                canPerformRollingAttack = false;
        }
        public void EnableCanDoBackstepAttack()
        {
            if (character.IsOwner)
                canPerformBackStepAttack = true;
        }
        public void DisableCanDoBackstepAttack()
        {
            if (character.IsOwner)
                canPerformBackStepAttack = false;
        }
        public virtual void EnableCanDoCombo()
        {

        }

        public virtual void DisableCanDoCombo()
        {

        }
    }
}
