using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

namespace KC
{
    [CreateAssetMenu(menuName = "A.I/States/Combat Stance")]
    public class CombatStanceState : AIState
    {
        //Primero seleccionamos un ataque para el estado dependiendo de la posicion y angulo que esté el objetivo
        //Procesamos todos los ataques que sean posibles de la lista y elegimos uno porcesando en un futuro cualquier logica de combate como (bloquear, caminar esquivar)
        //Si el objetivo se mueve fuera del alcance de combate entonces regresamos p cambiamos nuestra funcion de perseguir
        //Si el objetivo ya no está presente se regresa al estado de Idle 

        [Header("Attacks")]
        public List<AICharacterAttackAction> aiCharacterAttacks; //lista de todos los posibles ataques
        [SerializeField] protected List<AICharacterAttackAction> potentialAttacks; //Lista de ataques seleccionados segun el momento
        [SerializeField] private AICharacterAttackAction choosenAttack;
        [SerializeField] private AICharacterAttackAction previusAttack;
        protected bool hasAttack = false;

        [Header("Combo")]
        [SerializeField] protected bool canPerformCombo = false;
        [SerializeField] protected int chanceToPerformCombo = 25;
        protected bool hasRolledForComboChance = false;

        [Header("Engagement Distance")]
        [SerializeField] public float maximumEngagementDistance = 5; //la cantidad de distancia que debemos alejarnos para que la IA vuelva al estado de persecucion 

        public override AIState Tick(AICharacterManager aiCharacter)
        {
            if (aiCharacter.isPerformingAction)
                return this;

            if (!aiCharacter.navMeshAgent.enabled)
                aiCharacter.navMeshAgent.enabled = true;

            if (!NavMesh.SamplePosition(aiCharacter.transform.position, out var agentHit, 2.0f, NavMesh.AllAreas))
            {
                //Debug.LogError("El NavMeshAgent no está sobre el NavMesh.");
                return SwitchState(aiCharacter, aiCharacter.idle);
            }

            if (!NavMesh.SamplePosition(aiCharacter.aICharacterCombatManager.currentTarget.transform.position, out var targetHit, 2.0f, NavMesh.AllAreas))
            {
                //Debug.LogWarning("El objetivo no está sobre el NavMesh.");
                return SwitchState(aiCharacter, aiCharacter.idle);
            }

            aiCharacter.navMeshAgent.Warp(agentHit.position);

            NavMeshPath path = new NavMeshPath();
            bool pathFound = aiCharacter.navMeshAgent.CalculatePath(targetHit.position, path);

            if (!pathFound || path.status != NavMeshPathStatus.PathComplete)
            {
                //Debug.LogWarning("No se pudo calcular un camino válido.");
                return SwitchState(aiCharacter, aiCharacter.pursueTarget); // Intentar perseguir
            }

            aiCharacter.navMeshAgent.SetPath(path);

            if (aiCharacter.aICharacterCombatManager.enablePivot)
            {
                if (!aiCharacter.aiCharacterNetworkManager.isMoving.Value)
                {
                    if (aiCharacter.aICharacterCombatManager.viewableAngle < -30 || aiCharacter.aICharacterCombatManager.viewableAngle > 30)
                        aiCharacter.aICharacterCombatManager.PivotTowardsTarget(aiCharacter);
                }
            }

            aiCharacter.aICharacterCombatManager.RotateTowardsAgent(aiCharacter);

            if (aiCharacter.aICharacterCombatManager.currentTarget == null)
                return SwitchState(aiCharacter, aiCharacter.idle);

            if (aiCharacter.aICharacterCombatManager.distanceFromTarget > maximumEngagementDistance)
                return SwitchState(aiCharacter, aiCharacter.pursueTarget);

            if (!hasAttack)
            {
                GetNewAttack(aiCharacter);
            }
            else
            {
                aiCharacter.attack.currentAttack = choosenAttack;
                return SwitchState(aiCharacter, aiCharacter.attack);
            }

            return this;
        }

        protected virtual void GetNewAttack(AICharacterManager aiCharacter)
        {
            potentialAttacks = new List<AICharacterAttackAction>();

            foreach (var potentialAttack in aiCharacterAttacks)
            {
                if (potentialAttack.minimumAttackDistance > aiCharacter.aICharacterCombatManager.distanceFromTarget)
                    continue;

                if (potentialAttack.maximumAttackDistance < aiCharacter.aICharacterCombatManager.distanceFromTarget)
                    continue;

                if (potentialAttack.minimumAttackAngle > aiCharacter.aICharacterCombatManager.viewableAngle)
                    continue;

                if (potentialAttack.maximumAttackAngle < aiCharacter.aICharacterCombatManager.viewableAngle)
                    continue;

               potentialAttacks.Add(potentialAttack);
            }

            if (potentialAttacks.Count <= 0)
                return;

            var totalWeight = 0;

            foreach (var attack in potentialAttacks)
            {
                totalWeight += attack.attackWeight;
            }

            var randomWeightValue = Random.Range(1, totalWeight + 1);
            var processWeight = 0;

            foreach (var attack in potentialAttacks)
            {
                processWeight += attack.attackWeight;

                if(randomWeightValue <= processWeight)
                {
                    choosenAttack = attack;
                    previusAttack = choosenAttack;
                    hasAttack = true;
                    return;
                }
            }


            //Clasificar todos los ataques posibles
            //Eliminar los ataques que no se puedan realizar en esa situacion

            //Se colocan los ataques restantes en otra lista

            //Seleccionamos randomicamente un ataque segun el orden y peso

            //pasamos el ataque a nuestro estado para que la IA lo realice
        }

        protected virtual bool RollForOutcomeChance(int outcomeChance)
        {
            bool outcomeWillBePerformed = false;
            int randomPorcentage = Random.Range(0, 100);

            if (randomPorcentage < outcomeChance)
                outcomeWillBePerformed = true;

            return outcomeWillBePerformed;
        }

        protected override void ResetStateFlags(AICharacterManager aiCharacter)
        {
            base.ResetStateFlags(aiCharacter);

            hasAttack = false;
            hasRolledForComboChance = false;
        }
    }
}
