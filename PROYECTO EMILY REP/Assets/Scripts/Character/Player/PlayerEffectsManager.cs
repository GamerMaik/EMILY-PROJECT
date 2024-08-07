using UnityEngine;

namespace KC
{
    public class PlayerEffectsManager : CharacterEffectsManager
    {
        [Header("Debug Delete Later")]
        [SerializeField] InstantCharacterEffect effectToTest;
        [SerializeField] bool proccessEffect = false;

        private void Update()
        {
            if (proccessEffect)
            {
                proccessEffect = false;
                InstantCharacterEffect effect = Instantiate(effectToTest);
                ProccessInstantEffect(effect);
            }
        }
    }
}
