using UnityEngine;
using Unity.Netcode;
using System.Collections;

namespace KC
{
    public class FogWallInteractable : Interactable
    {
        [Header("Fog")]
        [SerializeField] GameObject[] fogGameObjects;
        //[SerializeField] GameObject trigguerObject;

        [Header("Collision")]
        [SerializeField] Collider fogWallCollider;

        [Header("ID")]
        public int fogWallId;

        [Header("Sound")]
        private AudioSource fogWallAudioSource;
        [SerializeField] AudioClip fogWallSFX;

        [Header("Active")]
        public NetworkVariable<bool> isActive = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

        protected override void Awake()
        {
            base.Awake();

            fogWallAudioSource = gameObject.GetComponent<AudioSource>();
        }

        public override void Interact(PlayerManager player)
        {
            base.Interact(player);

            Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward);
            player.transform.rotation = targetRotation;

            AllowPlayerThroughFogWallColliderServerRpc(player.NetworkObjectId);
            player.playerAnimatorManager.PlayerTargetActionAnimation("Pass_Through_Fog_01", true);
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            OnIsActiveChanged(false, isActive.Value);
            isActive.OnValueChanged += OnIsActiveChanged;
            WorldObjectManager.instance.AddFogWallToList(this);

        }

        public override void OnNetworkDespawn()
        {
            base.OnNetworkDespawn();
            isActive.OnValueChanged -= OnIsActiveChanged;
            WorldObjectManager.instance.RemoveFogWallFromList(this);
        }

        private void OnIsActiveChanged(bool oldStatus, bool newStatus)
        {
            if (isActive.Value)
            {
                //trigguerObject.SetActive(true);
                foreach (var fogObject in fogGameObjects)
                {
                    fogObject.SetActive(true);
                }
            }
            else
            {
                //trigguerObject.SetActive(false);
                foreach (var fogObject in fogGameObjects)
                {
                    fogObject.SetActive(false);
                }
            }
        }

        [ServerRpc(RequireOwnership = false)]
        private void AllowPlayerThroughFogWallColliderServerRpc(ulong playerObectID)
        {
            if (IsServer)
            {
                AllowPlayerThroughFogWallColliderClientRpc(playerObectID);
            }
        }
        [ClientRpc]
        private void AllowPlayerThroughFogWallColliderClientRpc(ulong playerObectID)
        {
            PlayerManager player = NetworkManager.Singleton.SpawnManager.SpawnedObjects[playerObectID].GetComponent<PlayerManager>();

            fogWallAudioSource.PlayOneShot(fogWallSFX);

            if (player != null)
                StartCoroutine(DisableCollisionForTime(player));
        }

        private IEnumerator DisableCollisionForTime(PlayerManager player)
        {
            Physics.IgnoreCollision(player.characterController, fogWallCollider, true);
            yield return new  WaitForSeconds(3);
            Physics.IgnoreCollision(player.characterController, fogWallCollider, false);
        }
    }
}
