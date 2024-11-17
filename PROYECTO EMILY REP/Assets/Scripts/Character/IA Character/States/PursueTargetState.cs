using UnityEngine;
using UnityEngine.AI;

namespace KC
{
    [CreateAssetMenu(menuName = "A.I/States/Pursue Target")]
    public class PursueTargetState : AIState
    {
        public override AIState Tick(AICharacterManager aiCharacter)
        {
            //Primero verificar si la  IA está realizando un tipo de accion
            
            if (aiCharacter.isPerformingAction)
                return this;

            //Si no se encuentra ningun objetivo regrear al estado de Idle
            if (aiCharacter.aICharacterCombatManager.currentTarget == null)
                return SwitchState(aiCharacter, aiCharacter.idle);

            if (aiCharacter.aICharacterCombatManager.distanceFromTarget > 10) {
                aiCharacter.aICharacterCombatManager.currentTarget = null;
                return SwitchState(aiCharacter, aiCharacter.idle); 
            }

            //Asegurarnos que la malla de navegacion este activa
            if (!aiCharacter.navMeshAgent.enabled)
                aiCharacter.navMeshAgent.enabled = true;

            //Si el objetivo está lejos del objetivo gira hacia el para atacarlo
            if (aiCharacter.aICharacterCombatManager.enablePivot)
            {
                if (aiCharacter.aICharacterCombatManager.viewableAngle < aiCharacter.aICharacterCombatManager.minimumFOV
                    || aiCharacter.aICharacterCombatManager.viewableAngle > aiCharacter.aICharacterCombatManager.maximumFOV)
                    aiCharacter.aICharacterCombatManager.PivotTowardsTarget(aiCharacter);
            }


            aiCharacter.aICharacterLocomotionManager.RotateTowarsAgent(aiCharacter);

            //Si esta dentro del area de combate de un objetivo cambiamos al estilo de postura de combate
            if (aiCharacter.aICharacterCombatManager.distanceFromTarget <= aiCharacter.navMeshAgent.stoppingDistance)
                return SwitchState(aiCharacter, aiCharacter.combatStance);
            //Si el objetivo no esta dentro del area lo devolvemos a casa


            //Perseguir al objetivo
            NavMeshPath path = new NavMeshPath();
            aiCharacter.navMeshAgent.CalculatePath(aiCharacter.aICharacterCombatManager.currentTarget.transform.position, path);
            aiCharacter.navMeshAgent.SetPath(path);

            return this;
        }
    }
}
