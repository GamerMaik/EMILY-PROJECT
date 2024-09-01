using UnityEngine;

namespace KC
{
    public class AICharacterCombatManager : CharacterCombatManager
    {
        [Header("Action Recovery")]
        public float actionRecoveryTimer = 0;

        [Header("Target Information")]
        public float distanceFromTarget;
        public float viewableAngle;
        public Vector3 targetsDirection;


        [Header("Detection")]
        [SerializeField] float detectionRadius = 15;
        public float minimumFOV = -35;
        public float maximumFOV = 35;

        [Header("Attack Rotation Speed")]
        public float attackRotationSpeed = 25;

        public void FindTargetViaLineOffSight(AICharacterManager aiCharacter)
        {
            if (currentTarget != null)
                return;

            Collider[] colliders = Physics.OverlapSphere(aiCharacter.transform.position, detectionRadius, WorldUtilityManager.Instance.GetCharacterLayers());

            for (int i = 0; i < colliders.Length; i++)
            {
                CharacterManager targetCharacter = colliders[i].transform.GetComponent<CharacterManager>();

                if (targetCharacter == null)
                    continue;

                if (targetCharacter == aiCharacter)
                    continue;

                if (targetCharacter.isDead.Value)
                    continue;

                if (WorldUtilityManager.Instance.CanIDamageThisTarget(aiCharacter.characterGroup, targetCharacter.characterGroup))
                {
                    //Si el objetivo encontrado esta al frente de nosotros

                    Vector3 targetsDirection = targetCharacter.transform.position - aiCharacter.transform.position;
                    float angleOfPotentialTarget = Vector3.Angle(targetsDirection, aiCharacter.transform.forward);

                    if (angleOfPotentialTarget > minimumFOV && angleOfPotentialTarget < maximumFOV)
                    {
                        //Verificar si hay algun elemento que obstruye la vision del enemigo
                        if (Physics.Linecast(
                            aiCharacter.characterCombatManager.lookOnTransform.position,
                            targetCharacter.characterCombatManager.lookOnTransform.position,
                            WorldUtilityManager.Instance.GetEnviroLayers()))
                        {
                            Debug.DrawLine(aiCharacter.characterCombatManager.lookOnTransform.position, targetCharacter.characterCombatManager.lookOnTransform.position);

                        }
                        else
                        {
                            targetsDirection = targetCharacter.transform.position - transform.position;
                            viewableAngle = WorldUtilityManager.Instance.GetAngleOfTarget(transform, targetsDirection);
                            aiCharacter.characterCombatManager.SetTarget(targetCharacter);
                            PivotTowardsTarget(aiCharacter);
                        }
                    }
                }
            }
        }

        public void PivotTowardsTarget(AICharacterManager aiCharacter)
        {
            //Aca reproducimos la animacion de pivote dependiendo a la direccion del objetivo
            if (aiCharacter.isPerformingAction)
                return;

            if (viewableAngle >= 20 && viewableAngle <= 60)
            {
                aiCharacter.characterAnimatorManager.PlayerTargetActionAnimation("Turn_R 45", true);
            }
            else if (viewableAngle <= -20 && viewableAngle >= -60)
            {
                aiCharacter.characterAnimatorManager.PlayerTargetActionAnimation("Turn_L 45", true);
            }
            else if (viewableAngle >= 61 && viewableAngle <= 110)
            {
                aiCharacter.characterAnimatorManager.PlayerTargetActionAnimation("Turn_R 90", true);
            }
            else if (viewableAngle <= -61 && viewableAngle >= -110)
            {
                aiCharacter.characterAnimatorManager.PlayerTargetActionAnimation("Turn_L 90", true);
            }
            else if (viewableAngle >= 110 && viewableAngle <= 145)
            {
                aiCharacter.characterAnimatorManager.PlayerTargetActionAnimation("Turn_R 135", true);
            }
            else if (viewableAngle <= -110 && viewableAngle >= -145)
            {
                aiCharacter.characterAnimatorManager.PlayerTargetActionAnimation("Turn_L 135", true);
            }
            else if (viewableAngle >= 146 && viewableAngle <= 180)
            {
                aiCharacter.characterAnimatorManager.PlayerTargetActionAnimation("Turn_R 180", true);
            }
            else if (viewableAngle <= -146 && viewableAngle >= -180)
            {
                aiCharacter.characterAnimatorManager.PlayerTargetActionAnimation("Turn_L 180", true);
            }

        }
        public void RotateTowadsAgent(AICharacterManager aiCharacter)
        {
            if (aiCharacter.aiCharacterNetworkManager.isMoving.Value)
            {
                aiCharacter.transform.rotation = aiCharacter.navMeshAgent.transform.rotation;
            }
        }

        public void RotateTowarsTargetWhillstAttacking(AICharacterManager aICharacter)
        {
            if (currentTarget == null)
                return;

            if (!aICharacter.canRotate)
                return;

            if (!aICharacter.isPerformingAction)
                return;

            Vector3 targetDirection = currentTarget.transform.position - aICharacter.transform.position;
            targetDirection.y = 0;
            targetDirection.Normalize();

            if(targetDirection == Vector3.zero)
                targetDirection = aICharacter.transform.forward;

            Quaternion targetotation = Quaternion.LookRotation(targetDirection);

            aICharacter.transform.rotation = Quaternion.Slerp(aICharacter.transform.rotation, targetotation, attackRotationSpeed * Time.deltaTime);
        }

        public void HandleActionRecovery(AICharacterManager aiCharacter)
        {
            if(actionRecoveryTimer > 0)
            {
                if (!aiCharacter.isPerformingAction)
                {
                    actionRecoveryTimer -= Time.deltaTime;
                }
            }
        }
    }
}
