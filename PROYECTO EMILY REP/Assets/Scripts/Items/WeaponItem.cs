 using UnityEngine;

namespace KC
{
    public class WeaponItem : Item
    {
        [Header("Aniamtions")]
        public AnimatorOverrideController weaponAnimator;

        [Header("Model Instantiation")]
        public WeaponModelType weaponModelType;

        [Header("Weapon Model")]
        public GameObject weaponModel;

        [Header("Weapon Requirements")]
        public int strengthREQ = 0;
        public int dexREQ = 0;
        public int intREQ = 0;
        public int faithREQ = 0;

        [Header("Weapon Base Damage")]
        public int physicalDamage = 0;
        public int magicDamage = 0;
        public int fireDamage = 0;
        public int holyDamage = 0;
        public int lightningDamage = 0;

        //Items de bloqueo (En un futuro para bloquear el daño)

        [Header("Weapon Poise")]
        public float poiseDamage = 10;

        [Header("Attack Modifiers")]
        public float light_Attack_01_Modifier = 1.0f;
        public float light_Attack_02_Modifier = 1.2f;
        public float heavy_Attack_01_Modifier = 1.4f;
        public float heavy_Attack_02_Modifier = 1.6f;
        public float charged_Attack_01_Modifier = 2.0f;
        public float charged_Attack_02_Modifier = 2.2f;
        public float running_Attack_01_Modifier = 1.1f;
        public float rolling_Attack_01_Modifier = 1.2f;
        public float backstep_Attack_01_Modifier = 1.1f;
        //Modificadores de armas

        //Efectos de arama para criticos

        [Header("Stamina Costs Modifiers")]
        public int baseStaminaCost = 20;
        public float lightAttackStaminaCostMultiplier = 1.0f;
        public float heavyAttackStaminaCostMultiplier = 1.3f;
        public float chargedAttackStaminaCostMultiplier = 1.5f;
        public float runningAttackStaminaCostMultiplier = 1.1f;
        public float rollingAttackStaminaCostMultiplier = 1.2f;
        public float backstepAttackStaminaCostMultiplier = 1.1f;

        [Header("Weapon Blocking Absorption")]
        public float physicalBaseDamagedAbsorption = 50;
        public float magicBaseDamagedAbsorption = 50;
        public float fireBaseDamagedAbsorption = 50;
        public float holyBaseDamagedAbsorption = 50;
        public float lightningBaseDamagedAbsorption = 50;
        public float stability = 50; //Esto reduce la perdida de estamina para el bloqueo


        [Header("Actions")]
        public WeaponItemActions oh_RB_Action;
        public WeaponItemActions oh_RT_Action; //Trigguer de accion para una mano
        public WeaponItemActions oh_LB_Action;

        //Efectos de sonido armas o items
        [Header("SFX")]
        public AudioClip[] whooshes;
        public AudioClip[] blocking;
    }
}
