using UnityEngine;

namespace KC
{
    public class AICharacterManager : CharacterManager
    {
        public AICharacterCombatManager aICharacterCombatManager;

        [Header("Current State")]
        [SerializeField] AIState currentState;

        protected override void FixedUpdate()
        {
            base.FixedUpdate();

            ProcessStateMachine();
        }

        protected override void Awake()
        {
            base.Awake();

            aICharacterCombatManager = GetComponent<AICharacterCombatManager>();
        }

        private void ProcessStateMachine()
        {
            AIState nextState = currentState?.Tick(this);

            if (nextState != null)
            {
                currentState = nextState;
            }
        }
    }
}
