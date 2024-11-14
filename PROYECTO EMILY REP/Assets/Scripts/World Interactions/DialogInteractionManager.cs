using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;
using System;
using System.Collections;

namespace KC
{
    public class DialogInteractionManager : Interactable
    {
        [Header("Panel")]
        [SerializeField] GameObject panelDialog;
        [SerializeField] TextMeshProUGUI dialogText;
        [SerializeField] string[] lines;
        public float textpeed = 0.1f;
        int index;

        public void OpenDialogPanel()
        {
            PlayerUIManager.instance.menuWindowIsOpen = true;
            panelDialog.SetActive(true);
            StartDialogue();
        }
        public void CloseDialogPanel()
        {
            PlayerUIManager.instance.menuWindowIsOpen = false;
            panelDialog.SetActive(false);
        }
        public void PassNextLine()
        {
            if (index < lines.Length - 1)
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
        protected override void Start()
        {
            base.Start();
        }

        public override void Interact(PlayerManager player)
        {
            base.Interact(player);
            OpenDialogPanel();
            PassNextLine();
        }
    }
}
