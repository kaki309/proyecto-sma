
using UnityEngine;

public class BeerEasterEgg : InteractablesForPlayer
{
    [SerializeField] private DialogAndWhoSay[] dialogues;
    PlayerHealth playerHealth;
    void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
    }
    public override void Interact()
    {
        playerHealth.TakeDamage();
        
        // Seleccionar un diálogo aleatorio
        DialogAndWhoSay randomDialog = dialogues[Random.Range(0, dialogues.Length)];
        
        // Pasar el diálogo seleccionado en un array
        DialogueManager.Instance.StartDialogue(new DialogAndWhoSay[] { randomDialog });
    }
}
