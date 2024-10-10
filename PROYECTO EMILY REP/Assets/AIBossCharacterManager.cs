using UnityEngine;
using Unity.Netcode;
using System.Collections;
using System.Collections.Generic;

namespace KC
{
    public class AIBossCharacterManager : AICharacterManager
    {
        public int bossID = 0;
        [SerializeField] bool hasBeenDefeated = false;
        [SerializeField] bool hasBeenAwakened = false;
        [SerializeField] List<FogWallInteractable> fogwalls;

        [Header("Debug")]
        [SerializeField] bool test = false;

        protected override void Update()
        {
            base.Update();

            if (test)
            {
                test = false;
                WakeBoss();
            }
        }
        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            if (IsServer)
            {
                //Si nuestros datos guardados no contienen informacion del jefe los agregaremos
                if (!WorldSaveGameManager.instance.currentCharacterData.bossesAwakened.ContainsKey(bossID))
                {
                    WorldSaveGameManager.instance.currentCharacterData.bossesAwakened.Add(bossID, false);
                    WorldSaveGameManager.instance.currentCharacterData.bossesDefeated.Add(bossID, false);
                }
                //Si los datos existen sobreescribimos dichos datos del jefe
                else
                {
                    hasBeenDefeated = WorldSaveGameManager.instance.currentCharacterData.bossesDefeated[bossID];
                    hasBeenAwakened = WorldSaveGameManager.instance.currentCharacterData.bossesAwakened[bossID];

                }

                StartCoroutine(GetFogWallsFromWorldObjectManager());

                //Si el jefe a despertado habilitar niebla
                if (hasBeenAwakened)
                {
                    for (int i = 0; i < fogwalls.Count; i++)
                    {
                        fogwalls[i].isActive.Value = true;
                    }
                }

                //si el jefe a muerto desactivar la niebla 
                if (hasBeenDefeated)
                {
                    for (int i = 0; i < fogwalls.Count; i++)
                    {
                        fogwalls[i].isActive.Value = false;
                    }
                    aiCharacterNetworkManager.isActive.Value = false;
                }
            }
        }

        private IEnumerator GetFogWallsFromWorldObjectManager()
        {
            while (WorldObjectManager.instance.fogWalls.Count == 0)
            {
                yield return new WaitForEndOfFrame();
            }
            fogwalls = new List<FogWallInteractable>();

            foreach (var fogWall in WorldObjectManager.instance.fogWalls)
            {
                if (fogWall.fogWallId == bossID)
                    fogwalls.Add(fogWall);
            }
        }

        public override IEnumerator ProcessDeathEvent(bool manuallySelectDeathAnimation = false)
        {
            if (IsOwner)
            {
                characterNetworkManager.currentHealth.Value = 0;
                isDead.Value = true;

                //Si no esta en el suelo podemos reproducir la animacion de muerte aerea

                if (!manuallySelectDeathAnimation)
                {
                    characterAnimatorManager.PlayerTargetActionAnimation("Dead_01", true);
                }

                hasBeenDefeated = true;

                //Si nuestros datos guardados no contienen informacion del jefe los agregaremos
                if (!WorldSaveGameManager.instance.currentCharacterData.bossesAwakened.ContainsKey(bossID))
                {
                    WorldSaveGameManager.instance.currentCharacterData.bossesAwakened.Add(bossID, true);
                    WorldSaveGameManager.instance.currentCharacterData.bossesDefeated.Add(bossID, true);
                }
                //Si los datos existen sobreescribimos dichos datos del jefe
                else
                {
                    WorldSaveGameManager.instance.currentCharacterData.bossesAwakened.Remove(bossID);
                    WorldSaveGameManager.instance.currentCharacterData.bossesDefeated.Remove(bossID);
                    WorldSaveGameManager.instance.currentCharacterData.bossesAwakened.Add(bossID, true);
                    WorldSaveGameManager.instance.currentCharacterData.bossesDefeated.Add(bossID, true);
                }

                WorldSaveGameManager.instance.SaveGame();
            }




            //Si se quiere se reproduce un EFECTO DE SONIDO

            yield return new WaitForSeconds(5);
        }

        public void WakeBoss()
        {
            hasBeenAwakened = true;

            if (!WorldSaveGameManager.instance.currentCharacterData.bossesAwakened.ContainsKey(bossID))
            {
                WorldSaveGameManager.instance.currentCharacterData.bossesAwakened.Add(bossID, true);
            }
            else
            {
                WorldSaveGameManager.instance.currentCharacterData.bossesAwakened.Remove(bossID);
                WorldSaveGameManager.instance.currentCharacterData.bossesAwakened.Add(bossID, true);
            }

            for (int i = 0; i < fogwalls.Count; i++)
            {
                fogwalls[i].isActive.Value = true;
            }
        }
    }
}