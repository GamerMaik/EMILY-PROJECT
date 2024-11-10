using System.Collections.Generic;
using UnityEngine;

namespace KC
{
    public class ShowRandomQuestionsManager : MonoBehaviour
    {
        public static ShowRandomQuestionsManager instance;

        [Header("Questions List")]
        [SerializeField] private List<Question> questions = new List<Question>();

        private Question currentQuestion;

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
        }

        // Método para cargar una pregunta aleatoria
        public void LoadRandomQuestion()
        {
            if (questions.Count == 0)
            {
                Debug.LogWarning("No hay preguntas disponibles en la lista.");
                return;
            }

            // Seleccionar una pregunta aleatoria de la lista
            int randomIndex = Random.Range(0, questions.Count);
            currentQuestion = questions[randomIndex];

            // Preparar los textos de las respuestas
            string questionText = currentQuestion.questionText;
            string answerText01 = currentQuestion.answerOptions[0].answerText;
            string answerText02 = currentQuestion.answerOptions[1].answerText;
            string answerText03 = currentQuestion.answerOptions[2].answerText;
            string answerText04 = currentQuestion.answerOptions[3].answerText;

            PlayerUIManager.instance.playerUIQuestionPanelManager.OpenQuestionPanel(questionText, answerText01, answerText02, answerText03, answerText04);
        }

        // Método para verificar si la respuesta seleccionada es correcta
        public bool CheckAnswer(int answerIndex)
        {
            if (currentQuestion == null || answerIndex < 0 || answerIndex >= currentQuestion.answerOptions.Count)
            {
                Debug.LogWarning("Respuesta seleccionada no válida.");
                return false;
            }

            return currentQuestion.answerOptions[answerIndex].isCorrect;
        }
    }
}
