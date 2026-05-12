using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance {get; private set;}
    [SerializeField] private GameObject dialogueCanvas;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private Image dialogBoxImage;

    private DialogTextAndBoxSprit[] currentDialogues;
    private int currentDialogueIndex;
    void Awake()
    {
        Instance = this;       
    }
    private void Start()
    {
        dialogueCanvas.SetActive(false);
    }

    public void StartDialogue(DialogTextAndBoxSprit[] dialogues)
    {
        currentDialogues = dialogues;
        currentDialogueIndex = 0;

        dialogBoxImage.sprite = currentDialogues[currentDialogueIndex].sprite;
        dialogueCanvas.SetActive(true);
        dialogueText.text = currentDialogues[currentDialogueIndex].text;
    }

    public void NextDialogue()
    {
        currentDialogueIndex++;

        if (currentDialogueIndex >= currentDialogues.Length)
        {
            HideDialogue();
            return;
        }

        dialogueText.text = currentDialogues[currentDialogueIndex].text;
        dialogBoxImage.sprite = currentDialogues[currentDialogueIndex].sprite;
    }

    public void HideDialogue()
    {
        dialogueCanvas.SetActive(false);
    }
}
[System.Serializable]
public class DialogTextAndBoxSprit
{
    [TextArea] public string text;
    public Sprite sprite;
}
