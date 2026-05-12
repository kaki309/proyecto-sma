using UnityEngine;

public class NpcInteraction : InteractablesForPlayer
{
    [SerializeField] private DialogTextAndBoxSprit[] dialogues;

    public override void Interact()
    {
        DialogueManager.Instance.StartDialogue(dialogues);
    }
}
