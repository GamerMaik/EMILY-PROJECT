using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;

namespace KC
{
    public class DialogueManager : MonoBehaviour
    {
        public static DialogueManager instance;

        [Header("Dialog Config")]
        public TextMeshProUGUI dialogText;
        public string[] lines;
        public float textpeed = 0.1f;

        int index;

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

        public void StartDialogue()
        {
            index = 0;
            StartCoroutine(WriteLine());
        }

        public IEnumerator WriteLine()
        {
            foreach (char letter in lines[index].ToCharArray())
            {
                dialogText.text += letter;

                yield return new WaitForSeconds(textpeed);
            }
        } 

        public void NextLine()
        {
            if(index < lines.Length -1)
            {
                index++;
                dialogText.text = string.Empty;
                StartCoroutine(WriteLine());
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}
