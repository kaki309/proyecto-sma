using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider2D))]
public abstract class InteractablesForPlayer : MonoBehaviour, IInteractable
{
    public abstract void Interact();

    [Header("World UI")]
    [SerializeField] private GameObject interactionCanvas;

    private Button button;

    void Awake()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;

        button = interactionCanvas.GetComponentInChildren<Button>(true);

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(Interact);

        interactionCanvas.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactionCanvas.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactionCanvas.SetActive(false);
        }
    }
}