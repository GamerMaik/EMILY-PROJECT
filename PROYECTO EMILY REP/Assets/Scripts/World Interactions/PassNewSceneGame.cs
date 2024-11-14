using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Netcode;

namespace KC
{
    public class PassNewSceneGame : Interactable
    {

        protected override void Start()
        {
            base.Start();
        }

        public override void Interact(PlayerManager player)
        {
            Transform playerTransform = NetworkManager.Singleton.LocalClient.PlayerObject.GetComponent<Transform>();
            base.Interact(player);

            // Teletransporta al jugador a una nueva posición (ejemplo: X=10, Y=1, Z=5)
            Vector3 newLocation = new Vector3(5f, -40f, -5f);
            playerTransform.position = newLocation;

        }
    }
}
