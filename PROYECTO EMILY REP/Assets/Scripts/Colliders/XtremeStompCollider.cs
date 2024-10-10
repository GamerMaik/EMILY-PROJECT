using System.Collections.Generic;
using UnityEngine;

namespace KC
{
    public class XtremeStompCollider : DamageCollider
    {
        [SerializeField] AIXtremeCharacterManager xtremeCharacterManager;

        protected override void Awake()
        {
            base.Awake();
            xtremeCharacterManager = GetComponentInParent<AIXtremeCharacterManager>();
        }
        public void StompAttack()
        {
            GameObject stompFX = Instantiate(xtremeCharacterManager.xtremeCombatManager.xtremeImpactVFX, transform);
            Collider[] colliders = Physics.OverlapSphere(transform.position, xtremeCharacterManager.xtremeCombatManager.stompAttackRadius, WorldUtilityManager.Instance.GetCharacterLayers());
            List<CharacterManager> charactersDamaged = new List<CharacterManager>();
            foreach (var collider in colliders)
            {
                CharacterManager character = collider.GetComponentInParent<CharacterManager>();

                if (character != null)
                {
                    if (charactersDamaged.Contains(character))
                        continue;

                    if (character == xtremeCharacterManager)
                        continue;

                    charactersDamaged.Add(character);

                    if (character.IsOwner)
                    {
                        TakeDamageEffect damageEffect = Instantiate(WorldCharacterEffectsManager.instance.takeDamageEffect);
                        damageEffect.physicalDamage = xtremeCharacterManager.xtremeCombatManager.stompAttack;
                        damageEffect.poiseDamage = xtremeCharacterManager.xtremeCombatManager.stompAttack;

                        character.characterEffectsManager.ProccessInstantEffect(damageEffect);
                    }
                }
            }
        }
    }
}
