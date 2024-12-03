using UnityEngine;
using TMPro;
using System.Collections;
using System;
using UnityEngine.UI;

namespace KC
{
    public class PlayerUIPopUpManager : MonoBehaviour
    {
        [Header("Message Pop Up")]
        [SerializeField] TextMeshProUGUI popUpMessageText;
        [SerializeField] GameObject popUpMessageGameObject;

        [Header("Item Pop Up")]
        [SerializeField] GameObject itemPopUpGameObject;
        [SerializeField] Image itemIcon;
        [SerializeField] TextMeshProUGUI itemName;
        [SerializeField] TextMeshProUGUI itemAmount;


        [Header("You Died Pop UP")]
        [SerializeField] GameObject youDeadPopUpGameObject;
        [SerializeField] TextMeshProUGUI youDiedPopUpBackgroundText;
        [SerializeField] TextMeshProUGUI youDiedPopUpText;
        [SerializeField] CanvasGroup youDiedCanvasGroup; //Nos permite configurar la transparencia para que se desvanezca con el tiempo

        [Header("Boss Defeated Pop UP")]
        [SerializeField] GameObject bossDefeatedPopUpGameObject;
        [SerializeField] TextMeshProUGUI bossDefeatedPopUpBackgroundText;
        [SerializeField] TextMeshProUGUI bossDefeatedPopUpText;
        [SerializeField] CanvasGroup bossDefeatedCanvasGroup;

        [Header("Site Of Grace Pop UP")]
        [SerializeField] GameObject siteOfGracePopUpGameObject;
        [SerializeField] TextMeshProUGUI siteOfGracePopUpBackgroundText;
        [SerializeField] TextMeshProUGUI siteOfGracePopUpText;
        [SerializeField] CanvasGroup siteOfGraceCanvasGroup;

        [Header("Entering Pop Up")]
        [SerializeField] GameObject enteringPopUpGameObject;
        [SerializeField] TextMeshProUGUI enteringPopUpBackgroundText;
        [SerializeField] TextMeshProUGUI enteringPopUpText;
        [SerializeField] CanvasGroup enteringCanvasGroup;

        [Header("MessageText Pop Up")]
        [SerializeField] GameObject messageTextPopUpGameObject;
        [SerializeField] TextMeshProUGUI messageTextPopUpBackgroundText;
        [SerializeField] TextMeshProUGUI messageTextPopUpText;
        [SerializeField] CanvasGroup messageTextCanvasGroup;

        [Header("Dialog Pop Up")]
        [SerializeField] TextMeshProUGUI dialogTextPopUp;
        [SerializeField] GameObject dialogPopUp;

        public void closeAllPopUpWindows()
        {
            popUpMessageGameObject.SetActive(false);
            itemPopUpGameObject.SetActive(false);
            dialogPopUp.SetActive(false);

            PlayerUIManager.instance.popUpWindowIsOpen = false;
        }

        public void SendPlayerMessagePopUp(string messageText)
        {
            PlayerUIManager.instance.popUpWindowIsOpen = true;
            popUpMessageText.text = messageText;
            popUpMessageGameObject.SetActive(true);
        }

        public void SendDialogPopUp(string messageText)
        {
            PlayerUIManager.instance.popUpWindowIsOpen = true;
            dialogTextPopUp.text = messageText;
            dialogPopUp.SetActive(true);
        }

        public void SendItemPopUp(Item item, int amount)
        {
            itemAmount.enabled = false;
            itemIcon.sprite = item.itemIcon;
            itemName.text = item.itemName;

            if (amount > 0)
            {
                itemAmount.enabled = true;
                itemAmount.text = "x" + amount.ToString();
            }

            itemPopUpGameObject.SetActive(true);
            PlayerUIManager.instance.popUpWindowIsOpen = true;
        }

        public void SendYouDiedPopUp()
        {
            //Se puede activar un post procesado aca en un futuro

            youDeadPopUpGameObject.SetActive(true);
            youDiedPopUpBackgroundText.characterSpacing = 0;
            StartCoroutine(StrechPopUpTextOverTime(youDiedPopUpBackgroundText,8 , 16f));
            StartCoroutine(FadeInPopUpTextOverTime(youDiedCanvasGroup, 5));
            StartCoroutine(WaithThenFadeOutPopUpOverTime(youDiedCanvasGroup, 2 , 5));

        }

        public void SendbBossDefeatedPopUp(string bossDefeatedMessage)
        {
            //Se puede activar un post procesado aca en un futuro
            bossDefeatedPopUpText.text = bossDefeatedMessage;
            bossDefeatedPopUpBackgroundText.text = bossDefeatedMessage;
            bossDefeatedPopUpGameObject.SetActive(true);
            bossDefeatedPopUpBackgroundText.characterSpacing = 0;
            StartCoroutine(StrechPopUpTextOverTime(bossDefeatedPopUpText, 8, 16f));
            StartCoroutine(FadeInPopUpTextOverTime(bossDefeatedCanvasGroup, 5));
            StartCoroutine(WaithThenFadeOutPopUpOverTime(bossDefeatedCanvasGroup, 2, 5));

        }

        public void SendbGraceRestoredPopUp(string graceRestoredMessage)
        {
            siteOfGracePopUpText.text = graceRestoredMessage;
            siteOfGracePopUpBackgroundText.text = graceRestoredMessage;
            siteOfGracePopUpGameObject.SetActive(true);
            siteOfGracePopUpBackgroundText.characterSpacing = 0;
            StartCoroutine(StrechPopUpTextOverTime(siteOfGracePopUpText, 8, 16f));
            StartCoroutine(FadeInPopUpTextOverTime(siteOfGraceCanvasGroup, 5));
            StartCoroutine(WaithThenFadeOutPopUpOverTime(siteOfGraceCanvasGroup, 2, 5));

        }

        public void EnteringPopUp(string enteringMessage)
        {
            enteringPopUpText.text = enteringMessage;
            enteringPopUpBackgroundText.text = enteringMessage;
            enteringPopUpGameObject.SetActive(true);
            enteringPopUpBackgroundText.characterSpacing = 0;
            StartCoroutine(StrechPopUpTextOverTime(enteringPopUpText, 5, 10f));
            StartCoroutine(FadeInPopUpTextOverTime(enteringCanvasGroup, 3));
            StartCoroutine(WaithThenFadeOutPopUpOverTime(enteringCanvasGroup, 2, 3));
        }

        public void MessagePopUp(string enteringMessage)
        {
            messageTextPopUpText.text = enteringMessage;
            messageTextPopUpBackgroundText.text = enteringMessage;
            messageTextPopUpGameObject.SetActive(true);
            messageTextPopUpBackgroundText.characterSpacing = 0;
            StartCoroutine(StrechPopUpTextOverTime(messageTextPopUpText, 8, 0f));
            StartCoroutine(FadeInPopUpTextOverTime(messageTextCanvasGroup, 5));
            StartCoroutine(WaithThenFadeOutPopUpOverTime(messageTextCanvasGroup, 2, 5));
        }

        private IEnumerator StrechPopUpTextOverTime(TextMeshProUGUI text, float duration, float strechAmount)
        {
            if (duration > 0f)
            {
                text.characterSpacing = 0;
                float timer = 0;
                yield return null;

                while (timer < duration)
                {
                    timer += Time.deltaTime;
                    text.characterSpacing = Mathf.Lerp(text.characterSpacing, strechAmount, duration * (Time.deltaTime / 20));
                    yield return null;
                }
            }
        }

        private IEnumerator FadeInPopUpTextOverTime(CanvasGroup canvas, float duration)
        {
            if (duration > 0)
            {
                canvas.alpha = 0;
                float timer = 0;

                yield return null;

                while(timer < duration)
                {
                    timer += Time.deltaTime;
                    canvas.alpha = Mathf.Lerp(canvas.alpha, 1, duration * (Time.deltaTime));
                    yield return null;
                }
            }
            canvas.alpha = 1;
            yield return null;
        }

        private IEnumerator WaithThenFadeOutPopUpOverTime(CanvasGroup canvas, float duration, float delay)
        {
            if (duration > 0)
            {
                while(delay > 0)
                {
                    delay -= Time.deltaTime;
                    yield return null;
                }

                canvas.alpha = 1;
                float timer = 0;

                yield return null;

                while (timer < duration)
                {
                    timer += Time.deltaTime;
                    canvas.alpha = Mathf.Lerp(canvas.alpha, 0, duration * (Time.deltaTime));
                    yield return null;
                }
            }
            canvas.alpha = 0;
            yield return null;
        }
    }
}
