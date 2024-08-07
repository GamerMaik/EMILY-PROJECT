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
            base.ProccessEffect(character);
            //Si el presonaje est� muerto no se procesa ningun efecto de da�o
            if (character.isDead.Value)
                return;

            //Si el personaje tiene invulerabilidad

            //Calcular el da�o
            CalculateDamage(character);

            //Verificar de donde vino el da�o
            //Determinar la animacion que se reproducir�
            //Comprobar las acumulaciones cuando sea aplicable (veneno, sangrado)
            //Reproducir alg�n sonido 

            //Comprobar si el personaje es IA, Verificar (Esta en fase beta aun XD)
        }

        private void CalculateDamage(CharacterManager character) 
        {
            if (!character.IsOwner)
                return;

            if (characterCausingDamage != null)
            {
                //Verificar si hay modificadores de da�o en ese personaje
            }

            //Verificar si el personaje tienen una reduccion de vida fija

            //Verificar si el personaje est� equipado con alguna armadura o algo que redusca el da�o 

            //Por ultimo se procesa todo el da�o total y se aplica el da�o final 

            finalDamageDealt = Mathf.RoundToInt(physicalDamage + magicDamage + fireDamage + lightningDamage + holyDamage);

            if (finalDamageDealt <= 0)
            {
                finalDamageDealt = 1;
            }

            Debug.Log("Final damage" + finalDamageDealt);

            character.characterNetworkManager.currentHealth.Value -= finalDamageDealt;
            
            //Calcular Pise damage 
        }

    }
}
