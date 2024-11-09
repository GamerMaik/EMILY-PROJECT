using UnityEngine;

namespace KC
{
    public class PlayerUIToggleHud : MonoBehaviour
    {
        private void OnEnable()
        {
            PlayerUIManager.instance.playerUIHudManager.ToggleHUD(false);
        }

        private void OnDisable()
        {
            PlayerUIManager.instance.playerUIHudManager.ToggleHUD(true);
        }
    }
}
