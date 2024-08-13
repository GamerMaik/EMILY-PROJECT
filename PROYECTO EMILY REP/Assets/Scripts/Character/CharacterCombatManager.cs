using UnityEngine;

namespace KC
{
    public class CharacterCombatManager : MonoBehaviour
    {
        [Header("Attack Target")] 
        public CharacterManager currentTarget;

        [Header("Attack Type")]
        public AttackType currentAttackType;

        [Header("Look On Transform")]
        public Transform lookOnTransform;
        

        protected virtual void Awake()
        {
            
        }
    }
}
