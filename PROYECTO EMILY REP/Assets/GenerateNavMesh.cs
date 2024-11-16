using Unity.AI.Navigation;
using UnityEngine;

namespace KC
{
    public class GenerateNavMesh : MonoBehaviour
    {
        public static GenerateNavMesh instance;
        [Header("Rooms Configuration")]
        [SerializeField] private NavMeshSurface navMeshSurface;

        public bool navMeshGenerated = false;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void GenerateNavmesh()
        {
            navMeshSurface.BuildNavMesh();
            navMeshGenerated = true;
        }

        public bool IsNavMeshGenerate()
        {
            return navMeshGenerated;
        }
    }
}
