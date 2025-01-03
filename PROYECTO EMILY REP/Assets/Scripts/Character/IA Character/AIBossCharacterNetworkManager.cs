using KC;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace KC
{
    public class AIBossCharacterNetworkManager : AICharacterNetworkManager
    {
        AIBossCharacterManager aiBossCharacter;

        protected override void Awake()
        {
            base.Awake();

            aiBossCharacter = GetComponent<AIBossCharacterManager>();
        }
        public override void CheckHP(float oldValue, float newValue)
        {
            base.CheckHP(oldValue, newValue);

            if (aiBossCharacter.IsOwner)
            {
                if (currentHealth.Value <= 0)
                    return;

                float healNeedForShift = maxHealth.Value * (aiBossCharacter.minimumHealthPorcentageToShift / 100);

                if (currentHealth.Value <= healNeedForShift)
                {
                    aiBossCharacter.PhaseShift();
                }
            }

        }
    }
}
