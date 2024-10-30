using UnityEngine;
using System.Collections.Generic;

namespace KC
{
    public class WorldCharacterEffectsManager : MonoBehaviour
    {
        public static WorldCharacterEffectsManager instance;

        [Header("VFX")]
        public GameObject bloodSplatterVFX;

        [Header("Damage")]
        public TakeDamageEffect takeDamageEffect;
        public TakeBlockedDamageEffect takeBlockedDamageEffect;

        [Header("Two Hand")]
        public TwoHandingEffects twoHandingEffects;

        [Header("Instant Effect")]
        [SerializeField] List<InstantCharacterEffect> instantEffects;

        [Header("Static Effect")]
        [SerializeField] List<StaticCharacterEffects> staticEffects;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            GenerateEffectsIDs();
        }

        private void GenerateEffectsIDs()
        {
            for (int i = 0; i < instantEffects.Count; ++i)
            {
                instantEffects[i].instantEffectID = i;
            }

            for (int i = 0; i < staticEffects.Count; i++)
            {
                staticEffects[i].staticEffectID = i;
            }
        }
    }
}
