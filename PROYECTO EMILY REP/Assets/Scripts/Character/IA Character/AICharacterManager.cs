using UnityEngine;
using UnityEngine.AI;

namespace KC
{
    public class AICharacterManager : CharacterManager
    {
        [HideInInspector] public AICharacterNetworkManager aiCharacterNetworkManager;
        [HideInInspector] public AICharacterCombatManager aICharacterCombatManager;
        [HideInInspector] public AICharacterLocomotionManager aICharacterLocomotionManager;

        [Header("NavMesh Agent")]
        public NavMeshAgent navMeshAgent;

        [Header("Current State")]
        [SerializeField] AIState currentState;

        [Header("States")]
        public IdleState idle;
        public PursueTargetState pursueTarget;

        //Estado de combate
        //Estado de ataque

        protected override void FixedUpdate()
        {
            base.FixedUpdate();

            ProcessStateMachine();
        }

        protected override void Awake()
        {
            base.Awake();

            aICharacterLocomotionManager = GetComponent<AICharacterLocomotionManager>();
            aICharacterCombatManager = GetComponent<AICharacterCombatManager>();
            aiCharacterNetworkManager = GetComponent<AICharacterNetworkManager>();

            navMeshAgent = GetComponentInChildren<NavMeshAgent>();


            //Se usa una copia del scriptable object, para que el original no se modifique
            idle = Instantiate(idle);
            pursueTarget = Instantiate(pursueTarget);

            currentState = idle;
        }

        private void ProcessStateMachine()
        {
            AIState nextState = currentState?.Tick(this);

            if (nextState != null)
            {
                currentState = nextState;
            }

            navMeshAgent.transform.localPosition = Vector3.zero;
            navMeshAgent.transform.localRotation = Quaternion.identity;

            if (navMeshAgent.enabled)
            {
                Vector3 agentDestination = navMeshAgent.destination;
                float remainingDistance = Vector3.Distance(agentDestination, transform.position);

                if (remainingDistance > navMeshAgent.stoppingDistance)
                {
                    aiCharacterNetworkManager.isMoving.Value = true;
                }
                else
                {
                    aiCharacterNetworkManager.isMoving.Value = false;
                }
            }
            else
            {
                aiCharacterNetworkManager.isMoving.Value = false;
            }
        }
    }
}
