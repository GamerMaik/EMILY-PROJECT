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
        public event Action<bool> OnQuestionAnswered;
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
        // Método para cargar una pregunta aleatoria
        public void LoadRandomQuestion()
        {
            PlayerManager player = NetworkManager.Singleton.LocalClient.PlayerObject.GetComponent<PlayerManager>();
            if (player == null)
                return;

            if (player.isDead.Value)
                return;

            if (questions.Count == 0)
            {
                Debug.LogWarning("No hay preguntas disponibles en la lista");
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
            float timeLimit = currentQuestion.timeLimit;

            PlayerUIManager.instance.playerUIQuestionPanelManager.OpenQuestionPanel(questionText, answerText01, answerText02, answerText03, answerText04, timeLimit);
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

            Debug.Log($"{questions.Count} preguntas cargadas en WorldQuestionManager.");
        }
        // Método para verificar si la respuesta seleccionada es correcta
        public bool CheckAnswer(int answerIndex)
        {
            bool isCorrect = currentQuestion.answerOptions[answerIndex].isCorrect;
            OnQuestionAnswered?.Invoke(isCorrect);
            return isCorrect;
        }
    }
}
