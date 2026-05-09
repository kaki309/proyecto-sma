using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText; 

    // Start is called before the first frame update
    void Start()
    {
        dialoguePanel.SetActive(false);
    }

    public void ShowDialogue(string dialogue)
    {
        dialoguePanel.SetActive(true);
        dialogueText.text = dialogue;
    }

    public void HideDialogue()
    {
        dialoguePanel.SetActive(false);
    }

}
