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

        public bool IsNavMeshGenerate()
        {
            return navMeshGenerated;
        }

        public void IncializeDungeonNavMesh()
        {
            if (navMeshGenerated)
            {
                Debug.Log("se generó Nav mesh"+ navMeshGenerated);
                DungeonsGeneratorTemplates.instance.SpawnObjects();
                Debug.Log("se generaron las entidades" + navMeshGenerated);
            }
        }
    }
}
