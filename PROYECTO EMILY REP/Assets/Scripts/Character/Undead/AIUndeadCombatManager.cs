using UnityEngine;

namespace KC
{
    public class AIUndeadCombatManager : AICharacterCombatManager
    {

        [Header("Damage Colliders")]
        [SerializeField] UndeadHandDamageCollider rightHandDamageCollider;
        [SerializeField] UndeadHandDamageCollider leftHandDamageCollider;

        [Header("Damage")]
        [SerializeField] int baseDamage = 25;
        [SerializeField] int basePoiseDamage = 25;
        [SerializeField] float attack01DamageModifier = 1.0f;
        [SerializeField] float attack02DamageModifier = 2.0f;
        [SerializeField] float attack03DamageModifier = 1.5f;

        [Header("Question Configuration")]
        [Range(0, 100)]
        [SerializeField] private int questionChancePercentage = 20; // Probabilidad de mostrar pregunta (0 a 100)

        [Range(0,100)]
        [SerializeField] private int slowMotionChancePercentage = 20;
        public void SettAttack01Damage()
        {
            rightHandDamageCollider.physicalDamage = baseDamage * attack01DamageModifier;
            leftHandDamageCollider.physicalDamage = baseDamage * attack01DamageModifier;
            rightHandDamageCollider.poiseDamage = basePoiseDamage * attack01DamageModifier;
            leftHandDamageCollider.poiseDamage = basePoiseDamage * attack01DamageModifier;
        }

        public void SettAttack02Damage()
        {
            rightHandDamageCollider.physicalDamage = baseDamage * attack02DamageModifier;
            leftHandDamageCollider.physicalDamage = baseDamage * attack02DamageModifier;
            rightHandDamageCollider.poiseDamage = basePoiseDamage * attack02DamageModifier;
            leftHandDamageCollider.poiseDamage = basePoiseDamage * attack02DamageModifier;
        }

        public void SettAttack03Damage()
        {
            rightHandDamageCollider.physicalDamage = baseDamage * attack03DamageModifier;
            leftHandDamageCollider.physicalDamage = baseDamage * attack03DamageModifier;
            rightHandDamageCollider.poiseDamage = basePoiseDamage * attack03DamageModifier;
            leftHandDamageCollider.poiseDamage = basePoiseDamage * attack03DamageModifier;
        }

        public void OpenRightHandDamageCollider()
        {
            aiCharacter.characterSoundFXManager.PlayAttackGrunt();
            rightHandDamageCollider.EnableDamageCollider();
            TryShowQuestion(); // Intentar mostrar una pregunta
        }

        public void DisableRightHandDamageCollider()
        {
            rightHandDamageCollider.DisableDamageCollider();
        }

        public void OpenLeftHandDamageCollider()
        {
            aiCharacter.characterSoundFXManager.PlayAttackGrunt();
            leftHandDamageCollider.EnableDamageCollider();
            TryShowQuestion(); // Intentar mostrar una pregunta
        }

        public void DisableLeftHandDamageCollider()
        {
            leftHandDamageCollider.DisableDamageCollider();
        }

        private void TryShowQuestion()
        {
            int randomValue = Random.Range(0, 100); // Generar un n�mero aleatorio entre 0 y 99
            if (randomValue < questionChancePercentage)
            {
                ShowRandomQuestionsManager.instance.LoadRandomQuestion(OnAnswerReceived);
            }
        }
        private void OnAnswerReceived(bool isCorrect)
        {
            if (isCorrect)
            {
                Debug.Log("Respuesta correcta");
                WorldLevelManager.instance.AddCountQuestionsAnswers(true);
                CameraSlowMotionManager.instance.DeactivateSlowMotion();
                CursorManager.instance.HideCursor();
            }
            else
            {
                Debug.Log("Respuesta incorrecta, aplicando da�o al personaje.");
                WorldLevelManager.instance.AddCountQuestionsAnswers(false);
                int randomValue = Random.Range(0, 100);
                if (randomValue < slowMotionChancePercentage)
                {
                    CameraSlowMotionManager.instance.ActiveSlowMotionForTime(0.3f,3);
                    CursorManager.instance.HideCursor();
                }
                else
                {
                    CameraSlowMotionManager.instance.DeactivateSlowMotion();
                    CursorManager.instance.HideCursor();
                }
            }
        }
    }
}
