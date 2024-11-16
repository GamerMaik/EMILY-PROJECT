using UnityEngine;

namespace KC
{
    public class SpawnAllEntities : Interactable
    {
        protected override void Start()
        {
            base.Start();
        }

        public override void Interact(PlayerManager player)
        {
            base.Interact(player);

            WorldDungeonManager.instance.IncializeDungeonNavMesh();
        }
    }
}
