using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

namespace KC
{
    public class RoomTriggerManager : MonoBehaviour
    {
        [Header("Room NavMesh")]
        [SerializeField] public bool isActiveNavMesh;
        [SerializeField] NavMeshSurface navMesh;

        [Header("Room Other Entries")]
        [SerializeField] RoomTriggerManager[] isActiveEntries;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                // Alternar el estado de la variable isActiveNavMesh
                isActiveNavMesh = !isActiveNavMesh;

                // Actualizar las entradas relacionadas
                for (int i = 0; i < isActiveEntries.Length; i++)
                {
                    isActiveEntries[i].isActiveNavMesh = !isActiveEntries[i].isActiveNavMesh;
                    isActiveEntries[i].UpdateNavMeshState();
                }

                // Actualizar el estado del navMesh actual
                UpdateNavMeshState();
                //Debug.Log("NavMesh está: " + isActiveNavMesh);
            }
        }

        private void UpdateNavMeshState()
        {
            if (isActiveNavMesh)
            {
                // Activar el NavMesh y reconstruirlo
                navMesh.enabled = true;
                //navMesh.BuildNavMesh();
            }
            else
            {
                // Desactivar el NavMesh
                navMesh.enabled = false;
            }
        }
    }
}
