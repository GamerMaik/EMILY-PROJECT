using UnityEngine;

namespace KC
{
    public class CharacterUIManager : MonoBehaviour
    {
        [Header("UI")]
        public bool hasFloatingHPBar= true;
        public UI_Character_HP_Bar characterHpBar;

        public void OnHPChanged(int oldValue, int newValue)
        {
            characterHpBar.oldHealthValue = oldValue;
            characterHpBar.SetStat(newValue);
            //characterHpBar.SetMaxStat(newValue);
        }
    }
}
