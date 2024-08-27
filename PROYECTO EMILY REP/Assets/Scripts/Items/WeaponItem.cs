using UnityEngine;

namespace KC
{
    public class WeaponItem : Item
    {
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
        //Modificadores de armas
        //Efectos de arama para criticos

        [Header("Stamina Costs Modifiers")]
        public int baseStaminaCost = 20;
        public float lightAttackStaminaCostMultiplier = 1;

        [Header("Actions")]
        public WeaponItemActions oh_RB_Action;
        public WeaponItemActions oh_RT_Action; //Trigguer de accion para una mano
        //Efectos de sonido de bloqueo
    }
}
