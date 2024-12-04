using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace KC
{
    public class AICharacterNetworkManager : CharacterNetworkManager
    {
        AICharacterManager aiCharacter;
        protected override void Awake()
        {
            base.Awake();

            aiCharacter =  GetComponent<AICharacterManager>();

            //aiCharacter.characterNetworkManager.currentHealth.Value = 200;
            //aiCharacter.characterNetworkManager.maxHealth.Value = 200;
        }
        public override void OnIsDeadChange(bool oldStatus, bool newStatus)
        {
            base.OnIsDeadChange(oldStatus, newStatus);

            aiCharacter.aICharacterInventoryManager.DropItem();
            WorldLevelManager.instance.AddNumberOfDead();
            WorldLevelManager.instance.SubstractEnemiesInRoom();
            StartCoroutine(ActivateSpawners());
        }

        public IEnumerator ActivateSpawners()
        {
            yield return new WaitForSeconds(5);
            Destroy(this.gameObject);
        }
    }
}
