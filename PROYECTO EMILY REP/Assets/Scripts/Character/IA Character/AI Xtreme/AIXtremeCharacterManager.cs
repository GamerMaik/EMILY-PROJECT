using UnityEngine;

namespace KC
{
    public class AIXtremeCharacterManager : AIBossCharacterManager
    {
        [HideInInspector] public AIXtremeSoundFXManager xtremeSoundFXManager;
        [HideInInspector] public AIXtremeCombatManager xtremeCombatManager;
        protected override void Awake()
        {
            base.Awake();
            xtremeSoundFXManager = GetComponent<AIXtremeSoundFXManager>();
            xtremeCombatManager = GetComponent<AIXtremeCombatManager>();
        }


    }
}
