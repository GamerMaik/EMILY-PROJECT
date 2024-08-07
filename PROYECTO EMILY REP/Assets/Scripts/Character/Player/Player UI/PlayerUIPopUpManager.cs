using UnityEngine;
using TMPro;
using System.Collections;
using System;

namespace KC
{
    public class PlayerUIPopUpManager : MonoBehaviour
    {
        [Header("You Died PopUP")]
        [SerializeField] GameObject youDeadPopUpGameObject;
        [SerializeField] TextMeshProUGUI youDiedPopUpBackgroundText;
        [SerializeField] TextMeshProUGUI youDiedPopUpText;
        [SerializeField] CanvasGroup youDiedCanvasGroup; //Nos permite configurar la transparencia para que se desvanezca con el tiempo

        public void SendYouDiedPopUp()
        {
            //Se puede activar un post procesado aca en un futuro

            youDeadPopUpGameObject.SetActive(true);
            youDiedPopUpBackgroundText.characterSpacing = 0;
            StartCoroutine(StrechPopUpTextOverTime(youDiedPopUpBackgroundText,8 , 16f));
            StartCoroutine(FadeInPopUpTextOverTime(youDiedCanvasGroup, 5));
            StartCoroutine(WaithThenFadeOutPopUpOverTime(youDiedCanvasGroup, 2 , 5));

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
