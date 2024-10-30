using UnityEngine;

namespace KC
{
    public class StaticCharacterEffects : ScriptableObject
    {
        [Header("Effect I.D.")]
        public int staticEffectID;

        public virtual void ProcessStaticEffect(CharacterManager character)
        {

        }

        public virtual void RemoveStaticEffect(CharacterManager character) 
        {

        }
    }
}
