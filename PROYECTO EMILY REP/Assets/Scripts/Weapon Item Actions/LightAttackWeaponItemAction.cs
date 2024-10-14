using UnityEngine;

namespace KC
{
    [CreateAssetMenu(menuName = "Character Actions/Weapon Actions/Light Attack Action")]
    public class LightAttackWeaponItemAction : WeaponItemActions
    {
        [Header("Light Attacks")]
        [SerializeField] string light_Attack_01 = "Main_Light_Attack_01";
        [SerializeField] string light_Attack_02 = "Main_Light_Attack_02";

        [Header("Actions Attacks")]
        [SerializeField] string run_Attack_01 = "Main_Run_Attack_01";
        [SerializeField] string roll_Attack_01 = "Main_Roll_Attack_01";
        [SerializeField] string backstep_Attack_01 = "Main_Backstep_Attack_01";

        public override void AttempToPerformAction(PlayerManager playerPerformingAction, WeaponItem weaponPerformingAction)
        {
            base.AttempToPerformAction(playerPerformingAction, weaponPerformingAction);

            if (!playerPerformingAction.IsOwner)
                return;
            
            if (playerPerformingAction.playerNetworkManager.currentStamina.Value <= 0)
                return;

            if (!playerPerformingAction.characterLocomotionManager.isGrounded)
                return;

            //Si está corriendo realice un ataque con carrera
            if (playerPerformingAction.characterNetworkManager.isSprinting.Value)
            {
                PerformRunningAttack(playerPerformingAction, weaponPerformingAction);
                return;
            }

            //Si está Rodando realice un ataque despues de rodar
            if (playerPerformingAction.characterCombatManager.canPerformRollingAttack)
            {
                PerformRollingAttack(playerPerformingAction, weaponPerformingAction);
                return;
            }
            //Si está dando un paso atras realice un ataque despues de dar un paso atras
            if (playerPerformingAction.characterCombatManager.canPerformBackStepAttack)
            {
                PerformBackStepAttack(playerPerformingAction, weaponPerformingAction);
                return;
            }

            PerformLightAttack(playerPerformingAction, weaponPerformingAction);
        }

        private void PerformLightAttack(PlayerManager playerPerformingAction, WeaponItem weaponPerformingAction)
        {
            if (playerPerformingAction.playerCombatManager.canComboWithMainHandWeapon && playerPerformingAction.isPerformingAction)
            {
                playerPerformingAction.playerCombatManager.canComboWithMainHandWeapon = false;

                //Realizar un ataque en base al ataque anterior
                if (playerPerformingAction.characterCombatManager.lastAttackAnimationPerformed == light_Attack_01)
                {
                    playerPerformingAction.playerAnimatorManager.PlayerTargetAttackActionAnimation(AttackType.LightAttack02, light_Attack_02, true);
                }
                else
                {
                    playerPerformingAction.playerAnimatorManager.PlayerTargetAttackActionAnimation(AttackType.LightAttack01, light_Attack_01, true);
                }
            }
            else if (!playerPerformingAction.isPerformingAction)
            {
                playerPerformingAction.playerAnimatorManager.PlayerTargetAttackActionAnimation(AttackType.LightAttack01, light_Attack_01, true);
            }

        }

        private void PerformRunningAttack(PlayerManager playerPerformingAction, WeaponItem weaponPerformingAction)
        {
            //Si tenemos el ataqu de dos manos reproducimos el ataque de dos manos
            // Y si no, realizamos el ataque ligero
            playerPerformingAction.playerAnimatorManager.PlayerTargetAttackActionAnimation(AttackType.RunningAttack01, run_Attack_01, true);


        }

        private void PerformRollingAttack(PlayerManager playerPerformingAction, WeaponItem weaponPerformingAction)
        {
            //Si tenemos el ataqu de dos manos reproducimos el ataque de dos manos
            // Y si no, realizamos el ataque ligero
            playerPerformingAction.playerCombatManager.canPerformRollingAttack = false;
            playerPerformingAction.playerAnimatorManager.PlayerTargetAttackActionAnimation(AttackType.RollingAttack01, roll_Attack_01, true);
        }

        private void PerformBackStepAttack(PlayerManager playerPerformingAction, WeaponItem weaponPerformingAction)
        {
            //Si tenemos el ataqu de dos manos reproducimos el ataque de dos manos
            // Y si no, realizamos el ataque ligero
            playerPerformingAction.playerCombatManager.canPerformBackStepAttack = false;
            playerPerformingAction.playerAnimatorManager.PlayerTargetAttackActionAnimation(AttackType.BackstepAttack01, backstep_Attack_01, true);
        }
    }
}
