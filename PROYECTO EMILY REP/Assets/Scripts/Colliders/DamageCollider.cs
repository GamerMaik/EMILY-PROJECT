using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace KC
{
    public class DamageCollider : MonoBehaviour
    {

        [Header("Collider")]
        [SerializeField] protected Collider damageCollider;
        
        [Header("Damage")]
        public float physicalDamage = 0;
        public float magicDamage = 0;
        public float fireDamage = 0;
        public float lightningDamage = 0;
        public float holyDamage = 0;

        [Header("Contact Point")]
        public Vector3 contactPoint;

        [Header("Character Damaged")]
        protected List<CharacterManager> charactersDamaged = new List<CharacterManager>();

        [Header("Block")]
        protected Vector3 directionFromAttackToDamageTarget;
        protected float dotValueFromAttackToDamageTarget;
        protected virtual void Awake()
        {
            
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            CharacterManager damageTarget = other.GetComponentInParent<CharacterManager>();

            if (damageTarget != null )
            {
                contactPoint = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);

                //Se verificará si existe el daño entre amigos

                //Se verificará si el objetivo esta cubriendoses o bloqueando el daño y se procesará un efecto de bloqueo

                CheckForBlock(damageTarget);
                //Si se pasa todo lo anterios se realiza daño al objetivo
                DamageTarget(damageTarget); 
            }
        }

        protected virtual void CheckForBlock(CharacterManager damageTarget)
        {
            if (charactersDamaged.Contains(damageTarget))
                return;

            GetBlockingDotValues(damageTarget);

            if (damageTarget.characterNetworkManager.isBlocking.Value && dotValueFromAttackToDamageTarget > 0.3f)
            {
                charactersDamaged.Add(damageTarget);

                TakeBlockedDamageEffect damageEffect = Instantiate(WorldCharacterEffectsManager.instance.takeBlockedDamageEffect);

                damageEffect.physicalDamage = physicalDamage;
                damageEffect.magicDamage = magicDamage;
                damageEffect.fireDamage = fireDamage;
                damageEffect.holyDamage = holyDamage;
                damageEffect.contactPoint = contactPoint;

                damageTarget.characterEffectsManager.ProccessInstantEffect(damageEffect);
            }
        }

        protected virtual void GetBlockingDotValues(CharacterManager damageTarget)
        {
            directionFromAttackToDamageTarget = transform.position - damageTarget.transform.position;
            dotValueFromAttackToDamageTarget = Vector3.Dot(directionFromAttackToDamageTarget, damageTarget.transform.forward);
        }

        //virtual por que cuando tengamos más planeadores de daño como cuerpo a cuerpo o hechizos se anula esto para darle su propio comportamento
        protected virtual void DamageTarget(CharacterManager damageTarget)
        {
            if (charactersDamaged.Contains(damageTarget))
                return;

            charactersDamaged.Add(damageTarget);

            TakeDamageEffect damageEffect = Instantiate(WorldCharacterEffectsManager.instance.takeDamageEffect);
            damageEffect.physicalDamage = physicalDamage;
            damageEffect.magicDamage = magicDamage;
            damageEffect.fireDamage = fireDamage;
            damageEffect.holyDamage = holyDamage;
            damageEffect.contactPoint = contactPoint;
            //damageEffect.lightningDamage = lightningDamage;

            damageTarget.characterEffectsManager.ProccessInstantEffect(damageEffect);
        }
        public virtual void EnableDamageCollider()
        {
            damageCollider.enabled = true;
        }
        public virtual void DisableDamageCollider()
        {
            damageCollider.enabled = false;
            charactersDamaged.Clear();
        }
    }
}
