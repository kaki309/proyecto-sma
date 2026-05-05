using UnityEngine;
using UnityEngine.UI;

public abstract class InteractablesForPlayer : MonoBehaviour, IInteractable
{

    public abstract void Interact();
    [SerializeField] private GameObject interactionButton;

    void Start()
    {
        // Desabilitar el canvas
        interactionButton.SetActive(false);
        // Establecer la función del click para interactuar
        interactionButton.GetComponentInChildren<Button>(true).onClick.AddListener(Interact);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactionButton.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactionButton.SetActive(false);
        }
    }
}