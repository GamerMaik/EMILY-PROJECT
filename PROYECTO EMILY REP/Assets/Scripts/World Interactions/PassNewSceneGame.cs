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

            //GenerateNavMesh.instance.GenerateNavmesh();
            // Llamar al Spawn después de un breve retraso
            //StartCoroutine(SpawnObjectsWithDelay());

            //Teletransporta al jugador a una nueva posición(ejemplo: X = 10, Y = 1, Z = 5)
            string worldScene = SceneUtility.GetScenePathByBuildIndex(2);
            NetworkManager.Singleton.SceneManager.LoadScene(worldScene, LoadSceneMode.Single);
            Vector3 newLocation = new Vector3(0f, 0f, 0f);
            playerTransform.position = newLocation;

        }

        private IEnumerator SpawnObjectsWithDelay()
        {
            yield return new WaitForSeconds(3f); // Asegúrate de que el NavMesh esté listo
            DungeonsGeneratorTemplates.instance.SpawnObjects();

        }
    }
}
