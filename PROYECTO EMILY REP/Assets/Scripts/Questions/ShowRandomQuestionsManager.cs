using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using Random = UnityEngine.Random;

namespace KC
{
    public class ShowRandomQuestionsManager : MonoBehaviour
    {
        public static ShowRandomQuestionsManager instance;

        [Header("Questions List")]
        [SerializeField] private List<Question> questions = new List<Question>();
        public event Action<bool> OnQuestionAnswered; // Evento para registrar callbacks de respuestas
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

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        // Mostrar una pregunta aleatoria
        public void LoadRandomQuestion(Action<bool> onAnswerCallback)
        {
            if (questions.Count == 0)
            {
                Debug.LogWarning("No hay preguntas disponibles en la lista.");
                onAnswerCallback?.Invoke(false); // Respuesta incorrecta por defecto
                return;
            }

            // Seleccionar una pregunta aleatoria
            int randomIndex = Random.Range(0, questions.Count);
            currentQuestion = questions[randomIndex];

            // Configurar el panel de la pregunta
            string questionText = currentQuestion.questionText;
            string answerText01 = currentQuestion.answerOptions[0].answerText;
            string answerText02 = currentQuestion.answerOptions[1].answerText;
            string answerText03 = currentQuestion.answerOptions[2].answerText;
            string answerText04 = currentQuestion.answerOptions[3].answerText;
            float timeLimit = currentQuestion.timeLimit;

            
            CursorManager.instance.ShowCursor();
            CameraSlowMotionManager.instance.ActivateSlowMotion(0);
            WorldLevelManager.instance.AddCountTotalQuestions();
            PlayerUIManager.instance.playerUIQuestionPanelManager.OpenQuestionPanel(questionText, answerText01, answerText02, answerText03, answerText04, timeLimit);
            // Suscribirse al evento de respuesta
            OnQuestionAnswered = onAnswerCallback;
            
        }

        // Comprobar respuesta
        public bool CheckAnswer(int answerIndex)
        {
            bool isCorrect = currentQuestion.answerOptions[answerIndex].isCorrect;

            // Llamar al callback con el resultado
            OnQuestionAnswered?.Invoke(isCorrect);

            // Reiniciar el evento para evitar llamadas no deseadas
            OnQuestionAnswered = null;

            return isCorrect;
        }
        public void LoadQuestionsFromPlayFabData(PlayFabQuestionData questionData)
        {
            questions.Clear();

            foreach (var playFabQuestion in questionData.questions)
            {
                Question newQuestion = ScriptableObject.CreateInstance<Question>();
                newQuestion.questionText = playFabQuestion.questionText;
                newQuestion.timeLimit = playFabQuestion.timeLimit;
                newQuestion.answerOptions = new List<AnswerOption>(playFabQuestion.answers);

                questions.Add(newQuestion);
            }

        }
    }
}
