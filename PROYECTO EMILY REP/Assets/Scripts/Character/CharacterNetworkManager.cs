using UnityEngine;
using Unity.Netcode;

namespace KC
{
    public class CharacterNetworkManager : NetworkBehaviour
    {
        CharacterManager character;
        //Si eres dueño del objeto puedes cambiar su posicion y si no solo puedes verlo
        [Header("Position")]
        public NetworkVariable<Vector3> networkPosition = new NetworkVariable<Vector3>(Vector3.zero, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<Quaternion> networkRotation = new NetworkVariable<Quaternion>(Quaternion.identity, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public Vector3 networkPositionVelocity;
        public float networkPositionSmoothTime = 0.1f;
        public float networkRotationSmoothTime = 0.1f;

        [Header("Animator")]
        public NetworkVariable<bool> isMoving = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<float> horizontalMovement = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<float> verticalMovement = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<float> moveAmount = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

        [Header("target")]
        public NetworkVariable<ulong> currentTargetNetworkObjetID = new NetworkVariable<ulong>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

        [Header("Flags")]
        public NetworkVariable<bool> isLokedOn = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<bool> isSprinting = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<bool> isJumping = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<bool> isChargingAttack = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

        [Header("Resources")]
       
        public NetworkVariable<int> currentHealth = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<int> maxHealth = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<float> currentStamina = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<int> maxStamina = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        
        [Header("Stats")]
        public NetworkVariable<int> vitality = new NetworkVariable<int>(1, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<int> endurance = new NetworkVariable<int>(1, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        //Una funcion RCP es una funcion que se llama desde cualquier cliente del servidor
        protected virtual void Awake()
        {
            character = GetComponent<CharacterManager>();
        }

        public void CheckHP(int oldValue, int newValue)
        {
            if (currentHealth.Value <= 0)
            {
                StartCoroutine(character.ProcessDeathEvent());
            }

            //prevenimos que se puedan curar con más puntos que su Vida Maxima
            if (character.IsOwner)
            {
                if (currentHealth.Value > maxHealth.Value)
                {
                    currentHealth.Value = maxHealth.Value;
                }
            }
        }

        public void OnLockOnTargetIdChange(ulong oldId, ulong newId)
        {
            if (!IsOwner)
            {
                character.characterCombatManager.currentTarget = NetworkManager.Singleton.SpawnManager.SpawnedObjects[newId].gameObject.GetComponent<CharacterManager>();
            }
        }

        public void OnIsLockedOnChange(bool oldValue, bool isLockOn)
        {
            //Si estamos bloqueados establecemos el valor en nulo para bloquear un nuevo objetivo
            if (!isLockOn)
            {
                character.characterCombatManager.currentTarget = null;
            }
        }

        public void IsChargingAttackChanged(bool oldStatus, bool newStatus)
        {
            character.animator.SetBool("IsChargingAttack", isChargingAttack.Value);
        }

        public void OnIsMovingChanged(bool oldStatus, bool newStatus)
        {
            character.animator.SetBool("IsMoving", isMoving.Value);
        }

        #region Action Animation
        [ServerRpc]
        public void NotifyTheServerOfActionAnimationServerRpc(ulong clientID, string animationID, bool applyRootMotion)
        {
            //Si es el servidor, decimos reproducir animacion de accion para todos los clientes
            if (IsServer)
            {
                PlayActionAnimationForAllClientsClientRpc(clientID, animationID, applyRootMotion);
            }
        }
        //el ClienteRcp tambien manda un Rcp DE CLIENTE a todos en el servidor 
        [ClientRpc]
        public void PlayActionAnimationForAllClientsClientRpc(ulong clientID, string animationID, bool apllyRootMotion)
        {
            if(clientID != NetworkManager.Singleton.LocalClientId) 
            {
                PerformActionAnimationFromServer(animationID, apllyRootMotion);
            }
        }
        private void PerformActionAnimationFromServer(string animationID, bool apllyRootMotion)
        {
            character.applyRootMotion = apllyRootMotion;
            character.animator.CrossFade(animationID, 0.2f);
        }
        #endregion

        #region Attack Animations
        //ATTACK
        [ServerRpc]
        public void NotifyTheServerOfAttackActionAnimationServerRpc(ulong clientID, string animationID, bool applyRootMotion)
        {
            //Si es el servidor, decimos reproducir animacion de accion para todos los clientes
            if (IsServer)
            {
                PlayAttackActionAnimationForAllClientsClientRpc(clientID, animationID, applyRootMotion);
            }
        }
        [ClientRpc]
        public void PlayAttackActionAnimationForAllClientsClientRpc(ulong clientID, string animationID, bool apllyRootMotion)
        {
            if (clientID != NetworkManager.Singleton.LocalClientId)
            {
                PerformAttackActionAnimationFromServer(animationID, apllyRootMotion);
            }
        }
        private void PerformAttackActionAnimationFromServer(string animationID, bool apllyRootMotion)
        {
            character.applyRootMotion = apllyRootMotion;
            character.animator.CrossFade(animationID, 0.2f);
        }

        #endregion

        #region Damage
        [ServerRpc(RequireOwnership = false)]
        public void NotifyTheServerOfCharacterDamageServerRpc(
            ulong damagedChracterID,
            ulong characterCausingDamage,
            float physicalDamage,
            float magicDamage,
            float fireDamage,
            float holyDamage,
            float poiseDamage,
            float angleHitFrom,
            float contactPointX,
            float contactPointY,
            float contactPointZ)
        {
            if (IsServer)
            {
                NotifyTheServerOfCharacterDamageCLientRpc(damagedChracterID, characterCausingDamage, physicalDamage, magicDamage, fireDamage, holyDamage, poiseDamage, angleHitFrom, contactPointX, contactPointY, contactPointZ);
            }
        }

        [ClientRpc]
        public void NotifyTheServerOfCharacterDamageCLientRpc(
            ulong damagedChracterID,
            ulong characterCausingDamage,
            float physicalDamage,
            float magicDamage,
            float fireDamage,
            float holyDamage,
            float poiseDamage,
            float angleHitFrom,
            float contactPointX,
            float contactPointY,
            float contactPointZ)
        {
            ProcessCharacterDamageFromServer(damagedChracterID, characterCausingDamage, physicalDamage, magicDamage, fireDamage, holyDamage, poiseDamage, angleHitFrom, contactPointX, contactPointY, contactPointZ);
        }

        public void ProcessCharacterDamageFromServer(
            ulong damagedChracterID,
            ulong characterCausingDamageID,
            float physicalDamage,
            float magicDamage,
            float fireDamage,
            float holyDamage,
            float poiseDamage,
            float angleHitFrom,
            float contactPointX,
            float contactPointY,
            float contactPointZ)
        {
            CharacterManager damageCharacter = NetworkManager.Singleton.SpawnManager.SpawnedObjects[damagedChracterID].gameObject.GetComponent<CharacterManager>();
            CharacterManager characterCausingDamage = NetworkManager.Singleton.SpawnManager.SpawnedObjects[characterCausingDamageID].gameObject.GetComponent<CharacterManager>();

            TakeDamageEffect damageEffect = Instantiate(WorldCharacterEffectsManager.instance.takeDamageEffect);

            damageEffect.physicalDamage = physicalDamage;
            damageEffect.magicDamage = magicDamage;
            damageEffect.fireDamage = fireDamage;
            damageEffect.holyDamage = holyDamage;
            damageEffect.poiseDamage = poiseDamage;
            damageEffect.angleHitFrom = angleHitFrom;
            damageEffect.contactPoint = new Vector3(contactPointX, contactPointY, contactPointZ);
            damageEffect.characterCausingDamage = characterCausingDamage;

            damageCharacter.characterEffectsManager.ProccessInstantEffect(damageEffect);
        }
        #endregion
    }
}
