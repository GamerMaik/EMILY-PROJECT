using UnityEngine;

namespace KC
{
    [CreateAssetMenu(menuName = "Character Effect/Instant Effects/Take Blocked Damage")]
    public class TakeBlockedDamageEffect : InstantCharacterEffect
    {
        [Header("Character Cuasing Damage")]
        public CharacterManager characterCausingDamage;

        [Header("Damage")]
        public float physicalDamage = 0;
        public float magicDamage = 0;
        public float fireDamage = 0;
        public float lightningDamage = 0;
        public float holyDamage = 0;

        [Header("Final Damage")]
        private int finalDamageDealt = 0;

        [Header("Poise")]
        public float poiseDamage = 0;
        public bool poiseIsBroken = false;

        [Header("Stamina")]
        public float staminaDamage = 0;
        public float finalStaminaDamage = 0;

        [Header("Animation")]
        public bool playerDamageAnimations = true;
        public bool manuallySelectDamageAnimation = false;
        public string damageAnimation;

        [Header("Sound Effects")]
        public bool willPlayDamageSFX = true;
        public AudioClip elementalDamageSoundFX;

        [Header("Direction Damage Taken From")]
        public float angleHitFrom; //usado para determinar la animacion que se reproducira segun el lado proveniente del ataque(Detras, Adelanta, Izquierda o Derecha)
        public Vector3 contactPoint;  //Usado para determinar donde reproducir los efectos de sangrado 


        public override void ProccessEffect(CharacterManager character)
        {
            if (character.characterNetworkManager.isInvulnerable.Value)
                return;

            base.ProccessEffect(character);

            Debug.Log("Hit blocked");
            //Si el presonaje está muerto no se procesa ningun efecto de daño
            if (character.isDead.Value)
                return;

            //Si el personaje tiene invulerabilidad

            //Calcular el daño
            CalculateDamage(character);
            CalculateStaminaDamage(character);
            //Verificar de donde vino el daño
            //Determinar la animacion que se reproducirá
            PlayDirectionalBaseBlockingAnimation(character);
            //Comprobar las acumulaciones cuando sea aplicable (veneno, sangrado)
            //Reproducir algún sonido 
            PlayDamageSFX(character);
            //Reproducir algun efecto de sangre
            PlayDamageVFX(character);
            //Comprobar si el personaje es IA, Verificar (Esta en fase beta aun XD)
            CheckForGuardBreak(character);
        }

        private void CalculateDamage(CharacterManager character)
        {
            if (!character.IsOwner)
                return;

            if (characterCausingDamage != null)
            {
                //Verificar si hay modificadores de daño en ese personaje
            }

            //Verificar si el personaje tienen una reduccion de vida fija

            //Verificar si el personaje está equipado con alguna armadura o algo que redusca el daño 

            //Por ultimo se procesa todo el daño total y se aplica el daño final 

            Debug.Log("Original Physical damage" + physicalDamage);

            physicalDamage -= (physicalDamage * (character.characterStatManager.blockingPhysicalAbsorption / 100));
            magicDamage -= (magicDamage * (character.characterStatManager.blockingMagicAbsorption / 100));
            fireDamage -= (fireDamage * (character.characterStatManager.blockingFireAbsorption / 100));
            lightningDamage -= (lightningDamage * (character.characterStatManager.blockingLightningAbsorption / 100));
            holyDamage -= (holyDamage * (character.characterStatManager.blockingHolyAbsorption / 100));

            finalDamageDealt = Mathf.RoundToInt(physicalDamage + magicDamage + fireDamage + lightningDamage + holyDamage);

            if (finalDamageDealt <= 0)
            {
                finalDamageDealt = 1;
            }

            Debug.Log("Final Physical damage" + physicalDamage);
            //Debug.Log("Final damage" + finalDamageDealt);

            character.characterNetworkManager.currentHealth.Value -= finalDamageDealt;

            //Calcular Pise damage 
        }

        private void CalculateStaminaDamage(CharacterManager character)
        {
            if (!character.IsOwner)
                return;

            finalStaminaDamage = staminaDamage;

            float staminaDamageAbsorption = finalStaminaDamage * (character.characterStatManager.blockingStability / 100);
            float staminaDamageAfterAbsorption = finalStaminaDamage - staminaDamageAbsorption;

            character.characterNetworkManager.currentStamina.Value -= staminaDamageAfterAbsorption;
        }

        private void CheckForGuardBreak(CharacterManager character)
        {
            if (!character.IsOwner)
                return;

            if(character.characterNetworkManager.currentStamina.Value <= 0)
            {
                character.characterAnimatorManager.PlayerTargetActionAnimation("Guard_Break_01", true);
                character.characterNetworkManager.isBlocking.Value = false;
            }
        }

        private void PlayDamageVFX(CharacterManager character)
        {
            //si en un futuro aplicamos daño por fuego se reproduce las particulas de fuego

            //OBTENER EFECTOS SEGUN AL ARMA
            //player.characterEffectsManager.PlayBloodSplatterVFX(contactPoint);
        }

        private void PlayDamageSFX(CharacterManager character)
        {
            //AudioClip physicalDamageSFX = WorldSoundFXManager.instance.ChooseRandomSFXFromArray(WorldSoundFXManager.instance.physicalDamageSFX);

            //player.characterSoundFXManager.PlaySoundFX(physicalDamageSFX);
            //player.characterSoundFXManager.PlayDamageGrunt();
            character.characterSoundFXManager.PlayBlockSoundFX();
        }

        private void PlayDirectionalBaseBlockingAnimation(CharacterManager character)
        {
            if (!character.IsOwner)
                return;

            if (character.isDead.Value)
                return;

            DamageIntensity damageIntensity = WorldUtilityManager.Instance.GetDamageIntensityBasedOnPoiseDamage(poiseDamage);


            //Si estamos usando 2 manos queremos obtener las animaciones del grupo de animaciones de 2 manos
            switch (damageIntensity)
            {
                case DamageIntensity.Ping:
                    damageAnimation = "Block_Ping_01";
                    break;
                case DamageIntensity.Light:
                    damageAnimation = "Block_Light_01";
                    break;
                case DamageIntensity.Medium:
                    damageAnimation = "Block_Medium_01";
                    break;
                case DamageIntensity.Heavy:
                    damageAnimation = "Block_Heavy_01";
                    break;
                case DamageIntensity.Colosal:
                    damageAnimation = "Block_Colosal_01";
                    break;
                default:
                    break;
            }

            character.characterAnimatorManager.PlayerTargetActionAnimation(damageAnimation, true);
        }
    }
}
