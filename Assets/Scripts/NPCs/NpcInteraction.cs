using UnityEngine;

public class NpcInteraction : InteractablesForPlayer
{
    [SerializeField] private DialogAndWhoSay[] dialogues;

    public override void Interact()
    {
        DialogueManager.Instance.StartDialogue(dialogues);
    }
}
