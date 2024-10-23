using UnityEngine;

namespace KC
{
    [CreateAssetMenu(menuName ="Character Actions/Weapon Actions/Off Hand Meele Attack Action")]
    public class OffHandMeeleAction : WeaponItemActions
    {
        public override void AttempToPerformAction(PlayerManager playerPerformingAction, WeaponItem weaponPerformingAction)
        {
            base.AttempToPerformAction(playerPerformingAction, weaponPerformingAction);

            if (!playerPerformingAction.playerCombatManager.canBlock)
                return;

            if (playerPerformingAction.playerNetworkManager.isAttacking.Value)
            {
                if (playerPerformingAction.IsOwner)
                    playerPerformingAction.playerNetworkManager.isBlocking.Value = false;

                return;
            }

            if (playerPerformingAction.playerNetworkManager.isBlocking.Value)
                return;

            if (playerPerformingAction.IsOwner)
                playerPerformingAction.playerNetworkManager.isBlocking.Value = true;
            
        }
    }
}
