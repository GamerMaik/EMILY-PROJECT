using UnityEngine;
using Unity.Netcode;

namespace KC
{
    public class Interactable : NetworkBehaviour
    {
        //Que es un interactuable? (Cualquier cosa con la que puedas o quieras interactuar)

        public string interactableText; //Este es el texto que aparece cuando interactuas con el objeto
        [SerializeField] protected Collider interactableCollider;
        [SerializeField] protected bool hostOnlyInteractable = true; //Solo el jugador host puede interactuar y no los jugadores que se unieron

        protected virtual void Awake()
        {
            if (interactableCollider == null)
                interactableCollider = GetComponent<Collider>();
        }

        protected virtual void Start()
        {

        }
        
        public virtual void Interact(PlayerManager player)
        {
            Debug.Log("Boton precionado");

            if (!player.IsOwner)
                return;

            interactableCollider.enabled = false;
            player.playerInteractionManager.RemoveInteractionFromList(this);
            PlayerUIManager.instance.playerUIPopUpManager.closeAllPopUpWindows();
        }

        public virtual void OnTriggerEnter(Collider other)
        {
            PlayerManager player = other.GetComponent<PlayerManager>();

            if (player != null)
            {
                if (!player.playerNetworkManager.IsHost && hostOnlyInteractable)
                    return;

                if (!player.IsOwner)
                    return;

                //Pasar a la interaccion con el objeto
                player.playerInteractionManager.AddInteractionToList(this);
            }
        }
        public virtual void OnTriggerExit(Collider other)
        {
            PlayerManager player = other.GetComponent<PlayerManager>();

            if (player != null)
            {
                if (!player.playerNetworkManager.IsHost && hostOnlyInteractable)
                    return;

                if (!player.IsOwner)
                    return;

                //Remover la interaccion con el objeto

                player.playerInteractionManager.RemoveInteractionFromList(this);

                PlayerUIManager.instance.playerUIPopUpManager.closeAllPopUpWindows();
            }
        }
    }
}
