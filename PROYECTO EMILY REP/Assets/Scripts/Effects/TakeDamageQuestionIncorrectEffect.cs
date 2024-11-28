using KC;
using UnityEngine;

namespace KC
{
    [CreateAssetMenu(menuName = "Character Effect/Instant Effects/Take Answer Incorrect Damage")]
    public class TakeDamageQuestionIncorrectEffect : InstantCharacterEffect
    {
        public int healthDamage;

        public override void ProccessEffect(CharacterManager character)
        {
            CalculateIncorrectAnswerDamage(character);
        }

        private void CalculateIncorrectAnswerDamage(CharacterManager character)
        {
            if (character.IsOwner)
            {
                character.characterNetworkManager.currentHealth.Value -= healthDamage;
            }
        }
    }
}
