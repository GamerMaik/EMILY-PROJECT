using KC;
using UnityEngine;

public class InfoShowTrigguer : MonoBehaviour
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
        PlayerUIManager.instance.playerUIPopUpManager.MessagePopUp(message);
        interactableCollider.enabled = false;
    }
}
