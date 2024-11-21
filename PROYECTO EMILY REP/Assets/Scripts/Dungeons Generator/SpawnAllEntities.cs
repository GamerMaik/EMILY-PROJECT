using UnityEngine;

namespace KC
{
    public class SpawnAllEntities : Interactable
    {
        [Header("Colliders Doors Initial")]
        [SerializeField] Collider Door1;
        [SerializeField] Collider Door2;
        [SerializeField] Collider Door3;
        protected override void Start()
        {
            base.Start();
        }

        public override void Interact(PlayerManager player)
        {
            base.Interact(player);

            WorldDungeonManager.instance.GenerateNavmesh();
            Door1.enabled = true;
            Door2.enabled = true;
            Door3.enabled = true;

        }
    }
}
