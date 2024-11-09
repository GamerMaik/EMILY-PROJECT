using UnityEngine;
using Unity.Netcode;

namespace KC
{
    public class PickUpItemInteractable : Interactable
    {
        public ItemPickUpType pickupType;

        [Header("Item")]
        [SerializeField] Item item;
        [SerializeField] int ammount = 1;

        [Header("World Spawn Pick Up")]
        [SerializeField] int itemID;
        [SerializeField] bool hasBeenLooted = false;

        protected override void Start()
        {
            base.Start();

            if (pickupType == ItemPickUpType.WorldSpawn)
            {
                CheckIfWorldItemWasAlreadyLooted();
            }
        }

        private void CheckIfWorldItemWasAlreadyLooted()
        {
            if (!NetworkManager.Singleton.IsHost)
            {
                gameObject.SetActive(false);
                return;
            }

            if (!WorldSaveGameManager.instance.currentCharacterData.worldItemsLooted.ContainsKey(itemID))
            {
                WorldSaveGameManager.instance.currentCharacterData.worldItemsLooted.Add(itemID, false);
            }

            hasBeenLooted = WorldSaveGameManager.instance.currentCharacterData.worldItemsLooted[itemID];

            if (hasBeenLooted)
                gameObject.SetActive(false);

            //Como por defecto inicia desactivado podemos hacer algo acá para activarlo nuevamente luego
            
        }

        public override void Interact(PlayerManager player)
        {
            base.Interact(player);
            //Reproducir el sonido de item recogido
            player.characterSoundFXManager.PlaySoundFX(WorldSoundFXManager.instance.pickUpItemSFX);

            //Agregar e Item a nuestro inventario
            player.playerInventoryManager.AddItemsToInventory(item, ammount);

            //Mostrar la ventana del Item en la pantalla
            PlayerUIManager.instance.playerUIPopUpManager.SendItemPopUp(item, ammount); 


            //Guardar el estado del Item en los objetos del mundo
            if (pickupType == ItemPickUpType.WorldSpawn)
            {
                if (WorldSaveGameManager.instance.currentCharacterData.worldItemsLooted.ContainsKey((int)itemID))
                {
                    WorldSaveGameManager.instance.currentCharacterData.worldItemsLooted.Remove(itemID);
                }
                WorldSaveGameManager.instance.currentCharacterData.worldItemsLooted.Add(itemID, true);
            }

            //Destruir el Item una vez que se interactue con el 
            Destroy(gameObject);
        }
    }
}
