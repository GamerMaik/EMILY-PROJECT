using Unity.Netcode;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace KC
{
    public class CharacterAnimatorManager : MonoBehaviour
    {
        CharacterManager characterManager;

        int vertical;
        int horizontal;

        [Header("Damage Animation")]
        public string hit_Forward_Medium_01 = "hit_Forward_Medium_01";
        public string hit_Backward_Medium_01 = "hit_Backward_Medium_01";
        public string hit_Left_Medium_01 = "hit_Left_Medium_01";
        public string hit_Right_Medium_01 = "hit_Right_Medium_01";

        protected virtual void Awake()
        {
            characterManager = GetComponent<CharacterManager>();

            vertical = Animator.StringToHash("Vertical");
            horizontal = Animator.StringToHash("Horizontal");
        }
        public void UpdateAnimatorMovementParameters(float horizontalMovement, float verticalMovement, bool isSprinting)
        {
            float horizontalAmount = horizontalMovement;
            float verticalAmount = verticalMovement;

            if (isSprinting)
            {
                verticalAmount = 2;
            }

            characterManager.animator.SetFloat(horizontal, horizontalAmount, 0.1f, Time.deltaTime);
            characterManager.animator.SetFloat(vertical, verticalAmount, 0.1f, Time.deltaTime);
        }

        public virtual void PlayerTargetActionAnimation(
            string targetAnimation,
            bool isPerformingAction,
            bool applyRootMotion = true,
            bool canRotate= false,
            bool canMove = false)
        {
            Debug.Log("Reproduciendo la animacion: " + targetAnimation);
            characterManager.applyRootMotion = applyRootMotion;
            characterManager.animator.CrossFade(targetAnimation, 0.2f);

            ///<sumary>
            ///Evita que el personaje no realice otra accion mientras se realiza otra
            ///Pero puede no incluir ciertas acciones por ejemplo cuando te realizan daño
            ///la bandera retornara True si estas aturdidoç
            ///de lo contrario si te dañan nuevamente y no estás bloqueado y estas realizando la accion la bandera está configurada en falso
            ///lo que permite salir de esa animacion y realizar otra accion
            ///En resumen es una barrera para permitir o detectar ciertas acciones
            ///</sumary>
            characterManager.isPerformingAction = isPerformingAction;
            characterManager.canRotate = canRotate;
            characterManager.canMove = canMove;

            //Aca le decimos al servidor que vamos a realizar una accion
            characterManager.characterNetworkManager.NotifyTheServerOfActionAnimationServerRpc(NetworkManager.Singleton.LocalClientId, targetAnimation, applyRootMotion);
        }

        public virtual void PlayerTargetAttackActionAnimation(AttackType attackType,
            string targetAnimation,
            bool isPerformingAction,
            bool applyRootMotion = true,
            bool canRotate = false,
            bool canMove = false)
        {

            characterManager.characterCombatManager.currentAttackType = attackType;
            characterManager.applyRootMotion = applyRootMotion;
            characterManager.animator.CrossFade(targetAnimation, 0.2f);
            characterManager.isPerformingAction = isPerformingAction;
            characterManager.canRotate = canRotate;
            characterManager.canMove = canMove;

            //Aca le decimos al servidor que vamos a realizar una accion
            characterManager.characterNetworkManager.NotifyTheServerOfAttackActionAnimationServerRpc(NetworkManager.Singleton.LocalClientId, targetAnimation, applyRootMotion);
        }


    }
}
