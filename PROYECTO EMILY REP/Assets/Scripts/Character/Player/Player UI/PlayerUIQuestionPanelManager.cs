using System.Collections;
using UnityEditor;
using UnityEngine;
using Unity.Netcode;
using TMPro;
using UnityEngine.UI;

namespace KC
{
    public class PlayerUIQuestionPanelManager : MonoBehaviour
    {
        [Header("Panel")]
        [SerializeField] GameObject panelQuestions;
        [SerializeField] TextMeshProUGUI questionText;
        [SerializeField] TextMeshProUGUI answer01;
        [SerializeField] TextMeshProUGUI answer02;
        [SerializeField] TextMeshProUGUI answer03;
        [SerializeField] TextMeshProUGUI answer04;

        [Header("Timer")]
        [SerializeField] private Slider timerSlider;

        private float questionTimeLimit;
        private Coroutine timerCoroutine;

        public void OpenQuestionPanel(string question, string answerR01, string answerR02, string answerR03, string answerR04, float timeLimit)
        {
            PlayerUIManager.instance.menuWindowIsOpen = true;
            questionText.text = question;
            answer01.text = answerR01;
            answer02.text = answerR02;
            answer03.text = answerR03;
            answer04.text = answerR04;
            panelQuestions.SetActive(true);

            // Configura el límite de tiempo y comienza la corrutina del temporizador
            questionTimeLimit = timeLimit;
            timerSlider.maxValue = questionTimeLimit;
            timerSlider.value = questionTimeLimit;

            if (timerCoroutine != null)
                StopCoroutine(timerCoroutine);

            timerCoroutine = StartCoroutine(TimerCountdown());
        }

        // Corrutina para controlar el temporizador
        private IEnumerator TimerCountdown()
        {
            float timeRemaining = questionTimeLimit;

            while (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                timerSlider.value = timeRemaining;
                yield return null;
            }

            // Si el tiempo se agota, la respuesta es incorrecta y se cierra el panel
            CheckAnswerQuestion(-1);  // -1 indica que no se seleccionó ninguna respuesta
            CloseQuestionPanel();
        }

        public void CloseQuestionPanel()
        {
            PlayerUIManager.instance.menuWindowIsOpen = false;
            panelQuestions.SetActive(false);

            if (timerCoroutine != null)
            {
                StopCoroutine(timerCoroutine);
                timerCoroutine = null;
            }
        }

        public void CloseQuestionsPanelAfterFixedFrame()
        {
            StartCoroutine(WaitThenCloseMenu());
        }

        private IEnumerator WaitThenCloseMenu()
        {
            yield return new WaitForFixedUpdate();

            PlayerUIManager.instance.menuWindowIsOpen = false;
            panelQuestions.SetActive(false);
        }

        public void CheckAnswerQuestion(int answerIndex)
        {
            bool isCorrect = ShowRandomQuestionsManager.instance.CheckAnswer(answerIndex);
            Debug.Log(isCorrect ? "Respuesta Correcta" : "Respuesta Incorrecta");

            // Cerrar el panel de preguntas
            CloseQuestionPanel();
        }
    }
}
