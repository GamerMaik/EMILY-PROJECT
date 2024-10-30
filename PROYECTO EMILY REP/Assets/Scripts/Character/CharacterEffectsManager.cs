using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

namespace KC
{
    public class CharacterEffectsManager : MonoBehaviour
    {
        //EFECTOS INSTANTANEOS (Curacion, recibir daño)

        //EFECTOS QUE PASAN CON EL PASAR DEL TIEMPO (veneno, nivel de xp)

        //EFECTOS ESTATICOS (Añadir o eliminar mejoras)

        CharacterManager character;
        [Header("VFX")]
        [SerializeField] GameObject bloodSplatterVFX;

        [Header("Static Effects")]
        public List<StaticCharacterEffects> staticEffects = new List<StaticCharacterEffects>();

        protected virtual void Awake()
        {
            character = GetComponent<CharacterManager>();
        }

        public virtual void ProccessInstantEffect(InstantCharacterEffect effect)
        {
            effect.ProccessEffect(character);
        }

        public void PlayBloodSplatterVFX(Vector3 contactPoint)
        {
            if(bloodSplatterVFX != null)
            {
                GameObject bloodSplatter = Instantiate(bloodSplatterVFX, contactPoint, Quaternion.identity);
            }
            else
            {
                GameObject bloodSplatter = Instantiate(WorldCharacterEffectsManager.instance.bloodSplatterVFX, contactPoint, Quaternion.identity);
            }
        }

        public void AddStaticEffects(StaticCharacterEffects effect)
        {
            staticEffects.Add(effect);
            effect.ProcessStaticEffect(character);

            for (int i = staticEffects.Count - 1; i > -1; i--)
            {
                if (staticEffects[i] == null)
                    staticEffects.RemoveAt(i);
            }
        }
        public void RemoveStaticEffects(int effectID)
        {
            StaticCharacterEffects effect;

            for (int i = 0; i < staticEffects.Count; i++)
            {
                if (staticEffects[i] != null)
                {
                    if (staticEffects[i].staticEffectID == effectID)
                    {
                        effect = staticEffects[i];
                        effect.RemoveStaticEffect(character);
                        staticEffects.Remove(effect);
                    }
                }
            }

            for (int i = staticEffects.Count - 1; i > -1; i--)
            {
                if (staticEffects[i] == null)
                    staticEffects.RemoveAt(i);
            }
        }
    }
}
