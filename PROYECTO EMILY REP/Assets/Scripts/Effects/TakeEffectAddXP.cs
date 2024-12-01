using UnityEngine;

namespace KC
{
    [CreateAssetMenu(menuName = "Character Effect/Instant Effects/Take Add XP")]
    public class TakeEffectAddXP : InstantCharacterEffect
    {
        public int ammountXP;

        public override void ProccessEffect(CharacterManager character)
        {
            base.ProccessEffect(character);
            CalculateAmmountAddXP(character);
        }

        private void CalculateAmmountAddXP(CharacterManager character)
        {
            if (character.IsOwner)
            {
                character.characterNetworkManager.vitality.Value = character.characterNetworkManager.vitality.Value + ammountXP;
            }
        }
    }
}
