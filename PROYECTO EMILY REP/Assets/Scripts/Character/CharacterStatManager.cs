using System.Globalization;
using UnityEngine;

namespace KC
{
    public class CharacterStatManager : MonoBehaviour
    {
        CharacterManager character;
        [Header("Stamina Regeneration")]
        private float staminaRegenerationTimer = 0;
        private float staminaTickTimer = 0;
        [SerializeField] float staminaRegenerationDelay = 2;
        [SerializeField] float staminaRegenerationAmount = 0.5f;
        protected virtual void Awake()
        {
            character = GetComponent<CharacterManager>();
        }

        protected virtual void Start()
        {
            
        }

        public int CalculateHealthBasedOnVitalityLevel(int vitality)
        {
            float helath = 0;
            helath = vitality * 15;
            return Mathf.RoundToInt(helath);
        }

        public int CalculateStaminaBasedOnEnduranceLevel(int endurance)
        {
            float stamina = 0;
            stamina = endurance * 10;
            return Mathf.RoundToInt(stamina);
        }

        public virtual void RegenerateStamina()
        {
            //Solo necesitamos ejecutar esta funcion si somo el propietario 
            if (!character.IsOwner)
                return;

            //No regenramos estamina mientras estamos corriendo o usandolo
            if (character.characterNetworkManager.isSprinting.Value)
                return;


            if (character.isPerformingAction)
                return;

            staminaRegenerationTimer += Time.deltaTime;

            if (staminaRegenerationTimer >= staminaRegenerationDelay)
            {
                if (character.characterNetworkManager.currentStamina.Value < character.characterNetworkManager.maxStamina.Value)
                {
                    staminaTickTimer += staminaTickTimer + Time.deltaTime;

                    if (staminaTickTimer >= 0.1f)
                    {
                        staminaTickTimer = 0;
                        character.characterNetworkManager.currentStamina.Value += staminaRegenerationAmount;
                    }
                }
            }
        }

        public virtual void ResetStaminaRegenrationTimer(float previusStaminaAmount, float currentStaminaAmount)
        {
            //Si nuestra estamina nueva es menor que la antigua estamina sabemos que algo la consumi�
            //y luego esperamos 2 segundos antes de volver a regenerarla
            if(currentStaminaAmount < previusStaminaAmount)
            {
                staminaRegenerationTimer = 0;
            }
        }
    }
}
