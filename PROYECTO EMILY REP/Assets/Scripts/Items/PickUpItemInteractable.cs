using UnityEngine;
using Unity.Netcode;
using System.Collections;

namespace KC
{
    public class PickUpItemInteractable : Interactable
    {
        public ItemPickUpType pickupType;

        [Header("Item")]
        [SerializeField] Item item;
        [SerializeField] int ammount = 1;

        [Header("Creature Loot Pick Up")]
        public NetworkVariable<int> itemID = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone,  NetworkVariableWritePermission.Owner);
        public NetworkVariable<Vector3> networkPosition = new NetworkVariable<Vector3>(Vector3.zero, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<ulong> droppingCreaturId = new NetworkVariable<ulong>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public bool trackDroppingCreaturesPosition = true; 

        [Header("World Spawn Pick Up")]
        [SerializeField] int worldSpawnIteractableID;
        [SerializeField] bool hasBeenLooted = false;

        [Header("Drop SFX")]
        [SerializeField] AudioClip itemDropSFX;
        AudioSource audioSource;

        protected override void Awake()
        {
            base.Awake();
            audioSource = GetComponent<AudioSource>();
        }

        protected override void Start()
        {
            base.Start();

            if (pickupType == ItemPickUpType.WorldSpawn)
            {
                CheckIfWorldItemWasAlreadyLooted();
            }
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            itemID.OnValueChanged += OnItemIDChanged;
            networkPosition.OnValueChanged += OnNetworkPositionChanged;
            droppingCreaturId.OnValueChanged += OnDroppingCreaturesIDChanged;

            if (pickupType == ItemPickUpType.CharacterDrop)
                audioSource.PlayOneShot(itemDropSFX);

            if (!IsOwner)
            {
                OnItemIDChanged(0, itemID.Value);
                OnNetworkPositionChanged(Vector3.zero, networkPosition.Value);
                OnDroppingCreaturesIDChanged(0, droppingCreaturId.Value);
            }
        }

        public override void OnNetworkDespawn()
        {
            base.OnNetworkDespawn();
            itemID.OnValueChanged -= OnItemIDChanged;
            networkPosition.OnValueChanged -= OnNetworkPositionChanged;
            droppingCreaturId.OnValueChanged -= OnDroppingCreaturesIDChanged;
        }

        private void CheckIfWorldItemWasAlreadyLooted()
        {
            if (!NetworkManager.Singleton.IsHost)
            {
                gameObject.SetActive(false);
                return;
            }

            if (!WorldSaveGameManager.instance.currentCharacterData.worldItemsLooted.ContainsKey(worldSpawnIteractableID))
            {
                WorldSaveGameManager.instance.currentCharacterData.worldItemsLooted.Add(worldSpawnIteractableID, false);
            }

            hasBeenLooted = WorldSaveGameManager.instance.currentCharacterData.worldItemsLooted[worldSpawnIteractableID];

            if (hasBeenLooted)
                gameObject.SetActive(false);

            //Como por defecto inicia desactivado podemos hacer algo acá para activarlo nuevamente luego
            
        }

        public override void Interact(PlayerManager player)
        {
            if (player.isPerformingAction)
                return;
            base.Interact(player);

            //Reproducir el sonido de item recogido
            player.characterSoundFXManager.PlaySoundFX(WorldSoundFXManager.instance.pickUpItemSFX);

            //Reproducir Animacion
            player.playerAnimatorManager.PlayerTargetActionAnimation("Pick_Up_Item_01", true);

            //Agregar e Item a nuestro inventario
            player.playerInventoryManager.AddItemsToInventory(item, ammount);

            //Mostrar la ventana del Item en la pantalla
            PlayerUIManager.instance.playerUIPopUpManager.SendItemPopUp(item, ammount); 


            //Guardar el estado del Item en los objetos del mundo
            if (pickupType == ItemPickUpType.WorldSpawn)
            {
                if (WorldSaveGameManager.instance.currentCharacterData.worldItemsLooted.ContainsKey((int)worldSpawnIteractableID))
                {
                    WorldSaveGameManager.instance.currentCharacterData.worldItemsLooted.Remove(worldSpawnIteractableID);
                }
                WorldSaveGameManager.instance.currentCharacterData.worldItemsLooted.Add(worldSpawnIteractableID, true);
            }
            DestroyThisNetworkObjectServerRpc();
        }

        protected void OnItemIDChanged(int oldValue, int newValue)
        {
            if (pickupType != ItemPickUpType.CharacterDrop)
                return;

           item = WorldItemDatabase.Instance.GetItemById(itemID.Value);
        }

        protected void OnNetworkPositionChanged(Vector3 oldPosition, Vector3 newPosition)
        {

            if (pickupType != ItemPickUpType.CharacterDrop)
                return;

            transform.position = networkPosition.Value;
        }

        protected void OnDroppingCreaturesIDChanged(ulong oldValue, ulong newValue)
        {
            if (pickupType != ItemPickUpType.CharacterDrop)
                return;

            if (trackDroppingCreaturesPosition)
                StartCoroutine(TrackDroppingCreaturesPosition());
            
        }

        protected IEnumerator TrackDroppingCreaturesPosition()
        {
            AICharacterManager droppingCreature = NetworkManager.Singleton.SpawnManager.SpawnedObjects[droppingCreaturId.Value].gameObject.GetComponent<AICharacterManager>();
            bool trackCreature = false;


            if(droppingCreature != null)
                trackCreature = true;

            if (trackCreature)
            {
                while (gameObject.activeInHierarchy)
                {
                    transform.position = droppingCreature.characterCombatManager.lookOnTransform.position;
                    yield return null;
                }
            }
            yield return null;
        }

        [ServerRpc(RequireOwnership =false)]
        protected void DestroyThisNetworkObjectServerRpc()
        {
            if (IsServer)
            {
                GetComponent<NetworkObject>().Despawn();
            }
        }
    }
}
