using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider2D))]
public abstract class InteractablesForPlayer : MonoBehaviour, IInteractable
{

    public abstract void Interact();
    [SerializeField] private GameObject interactionButton;

    void Start()
    {
        // Set collider as trigger
        GetComponent<BoxCollider2D>().isTrigger = true;
        // Disable Button Canvas
        interactionButton.SetActive(false);
        // Set click function on Button
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