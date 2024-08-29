using UnityEngine;

namespace KC
{
    public class WorldUtilityManager : MonoBehaviour
    {
        public static WorldUtilityManager Instance;

        [Header("Layers")]
        [SerializeField] LayerMask characterLayers;
        [SerializeField] LayerMask enviroLayers;

        private void Awake()
        {
            if (Instance == null) 
            {
                Instance = this;
            }
            else 
            {
                Destroy(gameObject);
            }
        }

        public LayerMask GetCharacterLayers() 
        {
            return characterLayers;
        }

        public LayerMask GetEnviroLayers() 
        {
            return enviroLayers;
        }

        public bool CanIDamageThisTarget(CharacterGroup attakingCharacter, CharacterGroup targetCharacter)
        {
            if (attakingCharacter == CharacterGroup.Team01)
            {
                switch (targetCharacter)
                {
                    case CharacterGroup.Team01: return false;
                    case CharacterGroup.Team02: return true;
                    default:
                        break;
                }
            }
            else if (attakingCharacter == CharacterGroup.Team02)
            {
                switch (targetCharacter)
                {
                    case CharacterGroup.Team01: return true;
                    case CharacterGroup.Team02: return false;
                    default:
                        break;
                }
            }
            return false;
        }
    }
}
