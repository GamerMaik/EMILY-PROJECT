using UnityEngine;
using Unity.Netcode;

namespace KC
{
    public class AICharacterInventoryManager : CharacterInventoryManager
    {
        AICharacterManager aiCharacter;
        [Header("Loot Chance")]
        public int dropItemChance = 10;
        [SerializeField] Item[] droppableItems;

        protected override void Awake()
        {
            base.Awake();
            aiCharacter = GetComponent<AICharacterManager>();
        }

        public void DropItem()
        {
            if (!aiCharacter.IsOwner)
                return;

            bool willDropItem = false;
            int itemChanceRoll = Random.Range(0, 100);

            if(itemChanceRoll <= dropItemChance)
                willDropItem = true;

            if (!willDropItem)
                return;

            Item generatedItem = droppableItems[Random.Range(0, droppableItems.Length)];

            if (generatedItem == null)
                return;

            GameObject itemPickUpInteractableGameObject = Instantiate(WorldItemDatabase.Instance.pickUpItemsPrefab);
            PickUpItemInteractable pickUpItemInteractable = itemPickUpInteractableGameObject.GetComponent<PickUpItemInteractable>();
            itemPickUpInteractableGameObject.GetComponent<NetworkObject>().Spawn();
            pickUpItemInteractable.itemID.Value = generatedItem.itemID;
            pickUpItemInteractable.networkPosition.Value = transform.position;
            pickUpItemInteractable.droppingCreaturId.Value = aiCharacter.NetworkObjectId;

        }
    }
}
