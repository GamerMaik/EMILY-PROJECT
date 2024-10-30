using UnityEngine;

namespace KC
{
    [CreateAssetMenu(menuName ="Character Effects/Stattic Effects/Two Handing Effect")]
    public class TwoHandingEffects : StaticCharacterEffects
    {
        [SerializeField] int strengthGainedFromTwoHandingWeapon;

        public override void ProcessStaticEffect(CharacterManager character)
        {
            base.ProcessStaticEffect(character);

            if (character.IsOwner)
            {
                strengthGainedFromTwoHandingWeapon = Mathf.RoundToInt(character.characterNetworkManager.strength.Value / 2);
                Debug.Log("Strength gained" + strengthGainedFromTwoHandingWeapon);
                character.characterNetworkManager.strengthModifier.Value += strengthGainedFromTwoHandingWeapon;
            }
        }
        public override void RemoveStaticEffect(CharacterManager character)
        {
            base.RemoveStaticEffect(character);

            if (character.IsOwner)
            {
                character.characterNetworkManager.strengthModifier.Value -= strengthGainedFromTwoHandingWeapon;
            }
        }
    }
}
