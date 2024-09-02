using UnityEngine;

namespace KC {
    public class PlayerAnimatorManager : CharacterAnimatorManager
    {
        PlayerManager player;
        protected override void Awake()
        {
            base.Awake();
            player = GetComponent<PlayerManager>();
        }

        private void OnAnimatorMove()
        {
          
            if (applyRootMotion)
            {
                Vector3 velocity = player.animator.deltaPosition;

                player.characterController.Move(velocity);
                player.transform.rotation *= player.animator.deltaRotation;
            }
        }

        //Animation events call
        public override void EnableCanDoCombo()
        {
            if (player.playerNetworkManager.isUsingRightHand.Value)
            {
                player.playerCombatManager.canComboWithMainHandWeapon = true;
            }
            else
            {
                //Enable off hand combo
            }
        }

        public override void DisableCanDoCombo()
        {
            player.playerCombatManager.canComboWithMainHandWeapon = false;
            //canComboWithOffHandWeapon = false;
        }
    } 
}
