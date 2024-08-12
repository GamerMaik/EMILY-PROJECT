using UnityEngine;

namespace KC
{
    public class CharacterCombatManager : MonoBehaviour
    {
        [Header("Attack Target")] 
        public CharacterManager currentTarget;

        [Header("Attack Type")]
        public AttackType currentAttackType;

        protected virtual void Awake()
        {
            
        }
    }
}
