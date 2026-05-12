using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private GameObject dialogueCanvas;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private Image dialogBoxImage;

    private string[] currentDialogues;
    private int currentDialogueIndex;

    private void Start()
    {
        dialogueCanvas.SetActive(false);
    }

    public void StartDialogue(string[] dialogues, Sprite dialogBoxSprite)
    {
        currentDialogues = dialogues;
        currentDialogueIndex = 0;
        dialogBoxImage.sprite = dialogBoxSprite;

        dialogueCanvas.SetActive(true);
        dialogueText.text = currentDialogues[currentDialogueIndex];
    }

    public void NextDialogue()
    {
        currentDialogueIndex++;

        if (currentDialogueIndex >= currentDialogues.Length)
        {
            HideDialogue();
            return;
        }

        dialogueText.text = currentDialogues[currentDialogueIndex];
    }

    public void HideDialogue()
    {
        dialogueCanvas.SetActive(false);
    }
}