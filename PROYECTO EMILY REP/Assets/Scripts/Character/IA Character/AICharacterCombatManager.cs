using UnityEngine;

namespace KC
{
    public class AICharacterCombatManager : CharacterCombatManager
    {

        [Header("Detection")]
        [SerializeField] float detectionRadius = 15;
        [SerializeField] float minimumDetectionAngle = -35;
        [SerializeField] float maximumDetectionAngle = 35;

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
                    float viewableAngle = Vector3.Angle(targetsDirection, aiCharacter.transform.forward);

                    if (viewableAngle > minimumDetectionAngle && viewableAngle < maximumDetectionAngle)
                    {
                        //Verificar si hay algun elemento que obstruye la vision del enemigo
                        if (Physics.Linecast(
                            aiCharacter.characterCombatManager.lookOnTransform.position,
                            targetCharacter.characterCombatManager.lookOnTransform.position,
                            WorldUtilityManager.Instance.GetEnviroLayers()))
                        {
                            Debug.DrawLine(aiCharacter.characterCombatManager.lookOnTransform.position, targetCharacter.characterCombatManager.lookOnTransform.position);
                            Debug.Log("Bloqueado");
                        }
                        else
                        {
                            aiCharacter.characterCombatManager.SetTarget(targetCharacter);
                        }
                    }
                }
            }
        }
    }
}
