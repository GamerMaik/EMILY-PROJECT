using UnityEngine;
using System.Collections.Generic;

namespace KC
{
    public class AIXtremeCombatManager : AICharacterCombatManager
    {
        AIXtremeCharacterManager aiXtremeManager;

        [Header("Damage Collider")]
        [SerializeField] XtremeWeaponDamageCollider xtremeDamageCollider;
        [SerializeField] XtremeStompCollider stompCollider;



        public float stompAttackRadius = 1.5f;

        [Header("Damage")]
        [SerializeField] int baseDamage = 25;
        public float stompAttack = 25f; // Pisotón
        [SerializeField] int basePoiseDamage = 25;
        [SerializeField] float attack02DamageModifier = 1.1f;
        [SerializeField] float attack03DamageModifier = 2.0f;
        [SerializeField] float attack04DamageModifier = 3.0f;
        [SerializeField] float attack05DamageModifier = 4.0f;

        [Header("VFX")]
        public GameObject xtremeImpactVFX;

        protected override void Awake()
        {
            base.Awake();
            aiXtremeManager = GetComponent<AIXtremeCharacterManager>();
        }

        public void StompAttack()
        {
            stompCollider.StompAttack();
        }

        public void SettAttack02Damage()
        {
            aiCharacter.characterSoundFXManager.PlayAttackGrunt();
            xtremeDamageCollider.physicalDamage = baseDamage * attack02DamageModifier;
            xtremeDamageCollider.poiseDamage = basePoiseDamage * attack02DamageModifier;
        }

        public void SettAttack03Damage()
        {
            aiCharacter.characterSoundFXManager.PlayAttackGrunt();
            xtremeDamageCollider.physicalDamage = baseDamage * attack03DamageModifier;
            xtremeDamageCollider.poiseDamage = basePoiseDamage * attack03DamageModifier;
        }
        public void SettAttack04Damage()
        {
            aiCharacter.characterSoundFXManager.PlayAttackGrunt();
            xtremeDamageCollider.physicalDamage = baseDamage * attack04DamageModifier;
            xtremeDamageCollider.poiseDamage = basePoiseDamage * attack04DamageModifier;
        }
        public void SettAttack05Damage()
        {
            aiCharacter.characterSoundFXManager.PlayAttackGrunt();
            xtremeDamageCollider.physicalDamage = baseDamage * attack05DamageModifier;
            xtremeDamageCollider.poiseDamage = basePoiseDamage * attack05DamageModifier;
        }

        public void OpenSwordDamageCollider()
        {

            xtremeDamageCollider.EnableDamageCollider();
            aiXtremeManager.characterSoundFXManager.PlaySoundFX(WorldSoundFXManager.instance.ChooseRandomSFXFromArray(aiXtremeManager.xtremeSoundFXManager.xtremeSwordWhooshes));
        
        }

        public void CloseSwordDamageCollider()
        {
            xtremeDamageCollider.DisableDamageCollider();
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
