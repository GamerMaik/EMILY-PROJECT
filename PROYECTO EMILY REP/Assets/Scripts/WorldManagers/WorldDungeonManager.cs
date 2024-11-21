using System;
using Unity.AI.Navigation;
using UnityEngine;

namespace KC
{
    public class WorldDungeonManager : MonoBehaviour
    {
        [HideInInspector] public static WorldDungeonManager instance;

        [Header("NavMesh Generation")]
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
        private void Start()
        {
            navMeshSurface = GetComponent<NavMeshSurface>();
        }
        public void GenerateNavmesh()
        {
            navMeshSurface.BuildNavMesh();
            navMeshGenerated = true;
        }
    }
}
