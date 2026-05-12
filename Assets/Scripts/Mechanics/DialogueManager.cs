using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }
    [SerializeField] private GameObject dialogueCanvas;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private Image dialogBoxImage;
    [SerializeField] Sprite mateoDialogBox, saraDialogBox;

    Character playerChar;
    private DialogAndWhoSay[] currentDialogues;
    private int currentDialogueIndex;
    void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        dialogueCanvas.SetActive(false);
        playerChar = GameController.Instance.GetCurrentCharacter();
    }

    public void StartDialogue(DialogAndWhoSay[] dialogues)
    {
        currentDialogues = dialogues;
        currentDialogueIndex = 0;

        SetDialogBoxSprite();
        dialogueText.text = currentDialogues[currentDialogueIndex].text;
        dialogueCanvas.SetActive(true);
    }

    public void NextDialogue()
    {
        currentDialogueIndex++;

        if (currentDialogueIndex >= currentDialogues.Length)
        {
            HideDialogue();
            return;
        }

        SetDialogBoxSprite();
        dialogueText.text = currentDialogues[currentDialogueIndex].text;
    }

    public void HideDialogue()
    {
        dialogueCanvas.SetActive(false);
    }
    void SetDialogBoxSprite()
    {
        if (currentDialogues[currentDialogueIndex].who == Options.Player)
        {
            dialogBoxImage.sprite = setPlayerDialogBox();
        }
        else
        {
            dialogBoxImage.sprite = currentDialogues[currentDialogueIndex].sprite;
        }
    }
    Sprite setPlayerDialogBox()
    {
        if (playerChar == Character.Mateo)
        {
            return mateoDialogBox;
        }
        else
        {
            return saraDialogBox;
        }
    }
}
[System.Serializable]
public class DialogAndWhoSay
{
    [TextArea] public string text;
    public Options who;

    [Tooltip("Dont use this if who is speaking is the player")]
    public Sprite sprite = null;
}
public enum Options
{
    NPC,
    Player
}