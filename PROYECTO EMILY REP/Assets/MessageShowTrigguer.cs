using System.Collections;
using TMPro;
using UnityEngine;

namespace KC
{
    public class MessageShowTrigguer : MonoBehaviour
    {
        [Header("Message")]
        [SerializeField] public string messageInPopUp;
        [SerializeField] protected Collider interactableCollider;

        private void Start()
        {
            interactableCollider = GetComponent<Collider>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                ShowMessage(messageInPopUp);
            }
        }

        public void ShowMessage(string message)
        {
            PlayerUIManager.instance.playerUIPopUpManager.EnteringPopUp(message);
            interactableCollider.enabled = false;
        }
    }
}
