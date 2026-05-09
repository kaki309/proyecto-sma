using UnityEngine;

public class NpcInteraction : InteractablesForPlayer
{
    [SerializeField] private DialogueManager dialogueManager;
    public override void Interact()
    {
        // Debug.Log("Log desde NPC");
        dialogueManager.ShowDialogue("Hola, soy el Director. Bienvenido a la planta PTAR.");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            dialogueManager.HideDialogue();
        }
    }
}
