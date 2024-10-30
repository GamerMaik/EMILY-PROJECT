using UnityEngine;

namespace KC
{
    public class ArmorItem : EquipmentItem
    {
        [Header("Equipment Absorption Bonus")]
        public float physicalDamageAbsroption;
        public float magicDamageAbsorption;
        public float fireDamageAbsorption;
        public float lightningDamageAbsorption;
        public float holyDamageAbsorption;

        [Header("Equipment Resistance Bonus")]
        public float immunity; //Resistencia contra el veneno
        public float robustness; //Resistencia al sangrado
        public float focus; //Resistencia a la locura y al sueño
        public float vitality; //Resistencia a la acumulacion de muerte

        [Header("Poise")]
        public float poise;

        public EquipmentModel[] equipmentModels;
 
    }
}
