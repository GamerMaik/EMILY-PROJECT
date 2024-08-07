using UnityEngine;

namespace KC
{
    public class InstantCharacterEffect : ScriptableObject
    {
        [Header("Effect ID")]
        public int instantEffectID;

        public virtual void ProccessEffect(CharacterManager character)
        {

        }
    }
}
