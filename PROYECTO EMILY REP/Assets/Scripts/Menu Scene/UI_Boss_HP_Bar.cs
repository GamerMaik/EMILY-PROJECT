using TMPro;
using UnityEngine;

namespace KC
{
    public class UI_Boss_HP_Bar : UI_StatBar
    {
        [SerializeField] AIBossCharacterManager bossCharacter;
        public void EnableBossHPBar(AIBossCharacterManager boss)
        {
            bossCharacter = boss;
            bossCharacter.aiCharacterNetworkManager.currentHealth.OnValueChanged += OnBossChanged;
            SetMaxStat(bossCharacter.characterNetworkManager.maxHealth.Value);
            SetStat(bossCharacter.aiCharacterNetworkManager.currentHealth.Value);
            GetComponentInChildren<TextMeshProUGUI>().text = bossCharacter.characterName;
        }
        private void OnDestroy()
        {
            bossCharacter.aiCharacterNetworkManager.currentHealth.OnValueChanged -= OnBossChanged;
        }
        private void OnBossChanged(int oldValue, int newValue)
        {
            SetStat(newValue);

            if (newValue <= 0)
            {
                RemoveHpBar(2.5f);
            }
        }

        public void RemoveHpBar(float time)
        {
            Destroy(gameObject, time);
        }
    }
}
