using UnityEngine;


namespace KC
{
    public class PlayerStatsManager : CharacterStatManager
    {
        PlayerManager player;
        protected override void Awake()
        {
            base.Awake();
            player = GetComponent<PlayerManager>();
        }

        protected override void Start()
        {
            base.Start();

            //Calculamos los valores aqui por que cuando creemos el menú de creacion de personajes
            //ajustaremos los valores dependiendo a la clase 
            //
            CalculateHealthBasedOnVitalityLevel(player.playerNetworkManager.vitality.Value);
            CalculateStaminaBasedOnEnduranceLevel(player.playerNetworkManager.endurance.Value);
        }
        public void CalculateTotalArmorAbsorption()
        {
            armorPhysicalDamageAbsroption = 0;
            armorMagicDamageAbsorption = 0;
            armorFireDamageAbsorption = 0;
            armorHolyDamageAbsorption = 0;
            armorLightningDamageAbsorption = 0;

            armorRobustness = 0;
            armorVitality = 0;
            armorImmunity = 0;
            armorFocus = 0;

            basePoiseDefense = 0;

            //Hand Equipment
            if (player.playerInventoryManager.headEquipmentItem != null)
            {
                //Resistencia al daño
                armorPhysicalDamageAbsroption += player.playerInventoryManager.headEquipmentItem.physicalDamageAbsroption;
                armorMagicDamageAbsorption += player.playerInventoryManager.headEquipmentItem.magicDamageAbsorption;
                armorFireDamageAbsorption += player.playerInventoryManager.headEquipmentItem.fireDamageAbsorption;
                armorHolyDamageAbsorption += player.playerInventoryManager.headEquipmentItem.holyDamageAbsorption;
                armorLightningDamageAbsorption += player.playerInventoryManager.headEquipmentItem.lightningDamageAbsorption;

                //Resistencia a los efectos de daño
                armorRobustness += player.playerInventoryManager.headEquipmentItem.robustness;
                armorVitality += player.playerInventoryManager.headEquipmentItem.vitality;
                armorImmunity += player.playerInventoryManager.headEquipmentItem.immunity;
                armorFocus += player.playerInventoryManager.headEquipmentItem.focus;

                //Equilibrio
                basePoiseDefense += player.playerInventoryManager.headEquipmentItem.poise;
            }
            //Body Equipment
            if (player.playerInventoryManager.bodyEquipmentItem != null)
            {
                //Resistencia al daño
                armorPhysicalDamageAbsroption += player.playerInventoryManager.bodyEquipmentItem.physicalDamageAbsroption;
                armorMagicDamageAbsorption += player.playerInventoryManager.bodyEquipmentItem.magicDamageAbsorption;
                armorFireDamageAbsorption += player.playerInventoryManager.bodyEquipmentItem.fireDamageAbsorption;
                armorHolyDamageAbsorption += player.playerInventoryManager.bodyEquipmentItem.holyDamageAbsorption;
                armorLightningDamageAbsorption += player.playerInventoryManager.bodyEquipmentItem.lightningDamageAbsorption;

                //Resistencia a los efectos de daño
                armorRobustness += player.playerInventoryManager.bodyEquipmentItem.robustness;
                armorVitality += player.playerInventoryManager.bodyEquipmentItem.vitality;
                armorImmunity += player.playerInventoryManager.bodyEquipmentItem.immunity;
                armorFocus += player.playerInventoryManager.bodyEquipmentItem.focus;

                //Equilibrio
                basePoiseDefense += player.playerInventoryManager.bodyEquipmentItem.poise;
            }
            //Leg Equipment
            if (player.playerInventoryManager.legEquipmentItem != null)
            {
                //Resistencia al daño
                armorPhysicalDamageAbsroption += player.playerInventoryManager.legEquipmentItem.physicalDamageAbsroption;
                armorMagicDamageAbsorption += player.playerInventoryManager.legEquipmentItem.magicDamageAbsorption;
                armorFireDamageAbsorption += player.playerInventoryManager.legEquipmentItem.fireDamageAbsorption;
                armorHolyDamageAbsorption += player.playerInventoryManager.legEquipmentItem.holyDamageAbsorption;
                armorLightningDamageAbsorption += player.playerInventoryManager.legEquipmentItem.lightningDamageAbsorption;

                //Resistencia a los efectos de daño
                armorRobustness += player.playerInventoryManager.legEquipmentItem.robustness;
                armorVitality += player.playerInventoryManager.legEquipmentItem.vitality;
                armorImmunity += player.playerInventoryManager.legEquipmentItem.immunity;
                armorFocus += player.playerInventoryManager.legEquipmentItem.focus;

                //Equilibrio
                basePoiseDefense += player.playerInventoryManager.legEquipmentItem.poise;
            }
            //Hand Equipment
            if (player.playerInventoryManager.handEquipmentItem != null)
            {
                //Resistencia al daño
                armorPhysicalDamageAbsroption += player.playerInventoryManager.handEquipmentItem.physicalDamageAbsroption;
                armorMagicDamageAbsorption += player.playerInventoryManager.handEquipmentItem.magicDamageAbsorption;
                armorFireDamageAbsorption += player.playerInventoryManager.handEquipmentItem.fireDamageAbsorption;
                armorHolyDamageAbsorption += player.playerInventoryManager.handEquipmentItem.holyDamageAbsorption;
                armorLightningDamageAbsorption += player.playerInventoryManager.handEquipmentItem.lightningDamageAbsorption;

                //Resistencia a los efectos de daño
                armorRobustness += player.playerInventoryManager.handEquipmentItem.robustness;
                armorVitality += player.playerInventoryManager.handEquipmentItem.vitality;
                armorImmunity += player.playerInventoryManager.handEquipmentItem.immunity;
                armorFocus += player.playerInventoryManager.handEquipmentItem.focus;

                //Equilibrio
                basePoiseDefense += player.playerInventoryManager.handEquipmentItem.poise;
            }
        }
    }
}
