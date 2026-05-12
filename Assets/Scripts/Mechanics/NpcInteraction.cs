using UnityEngine;

public class NpcInteraction : InteractablesForPlayer
{

    [SerializeField] private DialogueManager dialogueManager;

    [TextArea]
    [SerializeField] private string[] dialogues;
    [SerializeField] private Sprite dialogBoxSprite;

    public override void Interact()
    {
        dialogueManager.StartDialogue(dialogues, dialogBoxSprite);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            dialogueManager.HideDialogue();
        }
    }
}
