using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Netcode;
using System.Collections;

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

            string randomAdvice = AdviceManager.instance.SelectRandomListAdvice();
            // Muestra la pantalla de carga con el consejo seleccionado.
            string worldScene = SceneUtility.GetScenePathByBuildIndex(2);
            NetworkManager.Singleton.SceneManager.LoadScene(worldScene, LoadSceneMode.Single);
            PlayerUIManager.instance.playerUIPopUpManager.ShowScreenLoad(randomAdvice);
            Vector3 newLocation = new Vector3(0f, 0f, 0f);
            playerTransform.position = newLocation;

        }
    }
}
