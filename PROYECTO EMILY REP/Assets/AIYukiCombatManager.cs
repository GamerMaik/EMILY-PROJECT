using UnityEngine;

namespace KC
{
    public class AIYukiCombatManager : AICharacterCombatManager
    {
        [Header("Damage Collider")]
        [SerializeField] XtremeWeaponDamageCollider yukiSwordDamageCollider;

        [Header("Damage")]
        [SerializeField] int baseDamage = 25;
        [SerializeField] float attack01DamageModifier = 1.0f;
        [SerializeField] float attack02DamageModifier = 1.5f;
        [SerializeField] float attack03DamageModifier = 1.8f;
        [SerializeField] float attack04DamageModifier = 4.0f;

        public void SettAttack01Damage()
        {
            yukiSwordDamageCollider.physicalDamage = baseDamage * attack01DamageModifier;
        }

        public void SettAttack02Damage()
        {
            yukiSwordDamageCollider.physicalDamage = baseDamage * attack02DamageModifier;
        }

        public void SettAttack03Damage()
        {
            yukiSwordDamageCollider.physicalDamage = baseDamage * attack03DamageModifier;
        }
        public void SettAttack04Damage()
        {
            yukiSwordDamageCollider.physicalDamage = baseDamage * attack04DamageModifier;
        }


        public void OpenSwordDamageCollider()
        {
            aiCharacter.characterSoundFXManager.PlayAttackGrunt();
            yukiSwordDamageCollider.EnableDamageCollider();

        }

        public void CloseSwordDamageCollider()
        {
            yukiSwordDamageCollider.DisableDamageCollider();
        }

        public override void PivotTowardsTarget(AICharacterManager aiCharacter)
        {
            //Aca reproducimos la animacion de pivote dependiendo a la direccion del objetivo
            if (aiCharacter.isPerformingAction)
                return;

            if (viewableAngle >= 61 && viewableAngle <= 110)
            {
                aiCharacter.characterAnimatorManager.PlayerTargetActionAnimation("Turn_R 90", true);
            }
            else if (viewableAngle <= -61 && viewableAngle >= -110)
            {
                aiCharacter.characterAnimatorManager.PlayerTargetActionAnimation("Turn_L 90", true);
            }
            if (viewableAngle >= 146 && viewableAngle <= 180)
            {
                aiCharacter.characterAnimatorManager.PlayerTargetActionAnimation("Turn_R 180", true);
            }
            else if (viewableAngle <= -146 && viewableAngle >= -180)
            {
                aiCharacter.characterAnimatorManager.PlayerTargetActionAnimation("Turn_L 180", true);
            }
        }
    }
}
