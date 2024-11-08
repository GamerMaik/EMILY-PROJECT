using UnityEngine;

namespace KC
{
    public class PlayerUIEquipmentManagerInputManager : MonoBehaviour
    {
        PlayerControls playerControls;

        PlayerUIEquipmentManager PlayerUIEquipmentManager;

        [Header("Inputs")]
        [SerializeField] bool unequipItemInput;

        private void Awake()
        {
            PlayerUIEquipmentManager = GetComponentInParent<PlayerUIEquipmentManager>();
        }

        private void OnEnable()
        {
            if (playerControls == null) 
            {
                playerControls = new PlayerControls();
                playerControls.PlayerActions.UseItem.performed += i => unequipItemInput = true;
            }

            playerControls.Enable();
        }

        private void OnDisable()
        {
            playerControls.Disable();
        }

        private void Update()
        {
            HandlePLayerUIEquipmentManagerInputs();
        }

        private void HandlePLayerUIEquipmentManagerInputs()
        {
            if (unequipItemInput)
            {
                unequipItemInput = false;
                PlayerUIEquipmentManager.UnEquipSelectedItem();
            }
        }
    }
}
