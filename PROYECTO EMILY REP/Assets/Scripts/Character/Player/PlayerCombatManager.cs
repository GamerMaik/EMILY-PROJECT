using Unity.Netcode;
using UnityEngine;

namespace KC
{
    public class PlayerCombatManager : CharacterCombatManager
    {
        PlayerManager player;
        //arma que se esta usando actualmente
        public WeaponItem currentWeaponBeingUsed;
      
        protected override void Awake()
        {
            base.Awake();

            player = GetComponent<PlayerManager>();
        }

        public void PerformWeaponBasedAction(WeaponItemActions weaponAction, WeaponItem weaponPerformingAction)
        {
            if (player.IsOwner)
            {
                //Realizar Accion
                weaponAction.AttempToPerformAction(player, weaponPerformingAction);

                //Notificar a los demas jugadores que se hizo una accion mediante la red

                player.playerNetworkManager.NotifyTheServerOfWeaponActionServerRpc(NetworkManager.Singleton.LocalClientId, weaponAction.actionID, weaponPerformingAction.itemID);
            }
        }

        public virtual void DrainsStaminaBaseOnAttack()
        {
            if (!player.IsOwner)
                return;

            if (currentWeaponBeingUsed == null)
                return;

            float staminaDeducted = 0;

            switch (currentAttackType)
            {
                case AttackType.LightAttack01:
                    staminaDeducted = currentWeaponBeingUsed.baseStaminaCost * currentWeaponBeingUsed.lightAttackStaminaCostMultiplier;
                    break;
                default:
                    break;
            }

            player.playerNetworkManager.currentStamina.Value -= Mathf.RoundToInt(staminaDeducted);

        }
    }
}
