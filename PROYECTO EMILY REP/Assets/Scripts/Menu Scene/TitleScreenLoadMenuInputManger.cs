using UnityEngine;

namespace KC
{
    public class TitleScreenLoadMenuInputManger : MonoBehaviour
    {
        PlayerControls playerControls;
        [Header("Title Screen Inputs")]
        [SerializeField] bool deleteCharacterSlots = false;

        private void Update()
        {
            if (deleteCharacterSlots)
            {
                deleteCharacterSlots = false;
                TitleScreenManager.Instance.AttemptToDeleteCharacterSlot();
            }
        }
        private void OnEnable()
        {
            if (playerControls == null)
            {
                playerControls = new PlayerControls();
                playerControls.UI.Delete.performed += i => deleteCharacterSlots = true;
            }
            playerControls.Enable();
        }
        private void OnDisable()
        {
            playerControls.Disable();
        }
    }
}
