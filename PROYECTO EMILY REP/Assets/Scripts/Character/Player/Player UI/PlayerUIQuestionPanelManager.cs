using System.Collections;
using UnityEditor;
using UnityEngine;
using Unity.Netcode;
using TMPro;

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
        public void OpenQuestionPanel(string question, string answerR01, string answerR02, string answerR03, string answerR04)
        {
            PlayerUIManager.instance.menuWindowIsOpen = true;
            questionText.text = question;
            answer01.text = answerR01;
            answer02.text = answerR02;
            answer03.text = answerR03;
            answer04.text = answerR04;
            panelQuestions.SetActive(true);
        }

        public void CloseQuestionPanel()
        {
            PlayerUIManager.instance.menuWindowIsOpen = false;
            panelQuestions.SetActive(false);
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

            // Puedes agregar más acciones aquí, como cerrar el panel o mostrar feedback visual.


            CloseQuestionPanel();
        }
    }
}
