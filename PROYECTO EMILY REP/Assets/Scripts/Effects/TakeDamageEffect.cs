using UnityEngine;

namespace KC {
    [CreateAssetMenu(menuName = "Character Effect/Instant Effects/ Take Damage")]
    public class TakeDamageEffect : InstantCharacterEffect
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
            //Si el presonaje está muerto no se procesa ningun efecto de daño
            if (character.isDead.Value)
                return;
            //Calcular el daño
            CalculateDamage(character);
            //Verificar de donde vino el daño
            //Determinar la animacion que se reproducirá
            PlayDirectionalBaseDamgeAnimation(character);
            //Comprobar las acumulaciones cuando sea aplicable (veneno, sangrado)
            //Reproducir algún sonido 
            PlayDamageSFX(character);
            //Reproducir algun efecto de sangre
            PlayDamageVFX(character);
            //Comprobar si el personaje es IA, Verificar (Esta en fase beta aun XD)
        }

        private void CalculateDamage(CharacterManager character)
        {
            if (!character.IsOwner)
                return;

            if (characterCausingDamage != null)
            {
                // Verificar si hay modificadores de daño en ese personaje
                Debug.Log("Hay modificadores");
            }

            // Verificar si el personaje tienen una reduccion de vida fija
            Debug.Log("Daño Original: " + physicalDamage);

            // Aplicar la absorción de daño como reducción proporcional
            physicalDamage *= (1 - character.characterStatManager.armorPhysicalDamageAbsroption / 100f);
            magicDamage *= (1 - character.characterStatManager.armorMagicDamageAbsorption / 100f);
            fireDamage *= (1 - character.characterStatManager.armorFireDamageAbsorption / 100f);
            lightningDamage *= (1 - character.characterStatManager.armorLightningDamageAbsorption / 100f);
            holyDamage *= (1 - character.characterStatManager.armorHolyDamageAbsorption / 100f);

            Debug.Log("Daño con armadura: " + physicalDamage);

            // Calcular el daño total y redondearlo
            finalDamageDealt = Mathf.RoundToInt(physicalDamage + magicDamage + fireDamage + lightningDamage + holyDamage);
            Debug.Log("Daño final: " + finalDamageDealt);

            if (finalDamageDealt <= 0)
            {
                finalDamageDealt = 1;
            }

            // Aplicar el daño a la salud del personaje
            character.characterNetworkManager.currentHealth.Value -= finalDamageDealt;

            //Calcular Poise Damage
            character.characterStatManager.totalPoiseDamage -= poiseDamage;

            float remainingPoise = character.characterStatManager.basePoiseDefense +
                character.characterStatManager.offensivePoiseBinus +
                character.characterStatManager.totalPoiseDamage;

            if (remainingPoise <= 0)
                poiseIsBroken = true;

            //Si el personaje ah sido golpeado reiniciamos el temporizador
            character.characterStatManager.poiseResetTimer = character.characterStatManager.defaultPoiseResetTime;
        }

        private void PlayDamageVFX(CharacterManager character)
        {
            //si en un futuro aplicamos daño por fuego se reproduce las particulas de fuego

            character.characterEffectsManager.PlayBloodSplatterVFX(contactPoint);
        }

        private void PlayDamageSFX(CharacterManager character)
        {
            AudioClip physicalDamageSFX = WorldSoundFXManager.instance.ChooseRandomSFXFromArray(WorldSoundFXManager.instance.physicalDamageSFX);

            character.characterSoundFXManager.PlaySoundFX(physicalDamageSFX);
            character.characterSoundFXManager.PlayDamageGrunt();
        }

        private void PlayDirectionalBaseDamgeAnimation(CharacterManager character)
        {
            if (!character.IsOwner)
                return;

            if (character.isDead.Value)
                return;

            if (poiseIsBroken)
            {
                if (angleHitFrom >= 145 && angleHitFrom <= 180)
                {
                    damageAnimation = character.characterAnimatorManager.hit_Forward_Medium_01;
                    //Reproducir animacion de daño frontal
                }
                else if (angleHitFrom <= -145 && angleHitFrom >= -180)
                {
                    //Reproducir animacion de daño frontal
                    damageAnimation = character.characterAnimatorManager.hit_Forward_Medium_01;
                }
                else if (angleHitFrom >= -45 && angleHitFrom <= 45)
                {
                    //Reproducir animacion de daño espalda
                    damageAnimation = character.characterAnimatorManager.hit_Backward_Medium_01;
                }
                else if (angleHitFrom >= -144 && angleHitFrom <= -45)
                {
                    //Reproducir animacion de daño Izquierda
                    damageAnimation = character.characterAnimatorManager.hit_Left_Medium_01;
                }
                else if (angleHitFrom >= 45 && angleHitFrom <= 144)
                {
                    //Reproducir animacion de daño Derecha
                    damageAnimation = character.characterAnimatorManager.hit_Right_Medium_01;
                }
            }
            else
            {
                if (angleHitFrom >= 145 && angleHitFrom <= 180)
                {
                    damageAnimation = character.characterAnimatorManager.hit_Forward_Ping_01;
                    //Reproducir animacion de daño frontal
                }
                else if (angleHitFrom <= -145 && angleHitFrom >= -180)
                {
                    //Reproducir animacion de daño frontal
                    damageAnimation = character.characterAnimatorManager.hit_Forward_Ping_01;
                }
                else if (angleHitFrom >= -45 && angleHitFrom <= 45)
                {
                    //Reproducir animacion de daño espalda
                    damageAnimation = character.characterAnimatorManager.hit_Backward_Ping_01;
                }
                else if (angleHitFrom >= -144 && angleHitFrom <= -45)
                {
                    //Reproducir animacion de daño Izquierda
                    damageAnimation = character.characterAnimatorManager.hit_Left_Ping_01;
                }
                else if (angleHitFrom >= 45 && angleHitFrom <= 144)
                {
                    //Reproducir animacion de daño Derecha
                    damageAnimation = character.characterAnimatorManager.hit_Right_Ping_01;
                }
            }

            if (poiseIsBroken)
            {
                //Si estamos aturdidos no permitimos movimiento ni acciones
                character.characterAnimatorManager.PlayerTargetActionAnimation(damageAnimation, true);  
            }
            else
            {
                //Si no estamos aturdidos permitimos movimiento y acciones
                character.characterAnimatorManager.PlayerTargetActionAnimation(damageAnimation, false, false, true, true);
            }
        }

    }
}
