using UnityEngine;

namespace KC
{
    [CreateAssetMenu(menuName = "Character Effect/Instant Effects/ Take Stamina Damage")]
    public class TakeStaminaDamageEffect : InstantCharacterEffect
    {
        public float staminaDamage;
        public override void ProccessEffect(CharacterManager character)
        {
            CalculateStaminaDamage(character);
        }

        private void CalculateStaminaDamage(CharacterManager character) 
        {
            if (character.IsOwner)
            {
                character.characterNetworkManager.currentStamina.Value -= staminaDamage;
            }
        }
    }
}
