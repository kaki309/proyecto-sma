using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider2D))]
public abstract class InteractablesForPlayer : MonoBehaviour, IInteractable
{
    public abstract void Interact();

    [Header("World UI")]
    [SerializeField] private GameObject interactionCanvas;
    [SerializeField] private float fadeDuration = 0.3f;

    [Header("Usage")]
    [Tooltip("-1 = infinito")]
    [SerializeField] private int maxUses = -1;

    private int currentUses = 0;

    private Button button;
    private CanvasGroup canvasGroup;
    private Coroutine currentFade;

    void Awake()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;

        button = interactionCanvas.GetComponentInChildren<Button>(true);
        canvasGroup = interactionCanvas.GetComponent<CanvasGroup>();

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(OnInteractPressed);

        interactionCanvas.GetComponent<Canvas>().worldCamera = Camera.main;

        canvasGroup.alpha = 0f;
        interactionCanvas.SetActive(false);
    }

    void OnInteractPressed()
    {
        if (!CanInteract()) return;

        Interact();

        currentUses++;

        // After using this time, check again if is still interactable
        if (!CanInteract())
        {
            DisableInteraction();
        }
    }

    bool CanInteract()
    {
        return maxUses < 0 || currentUses < maxUses;
    }

    void DisableInteraction()
    {
        button.interactable = false;

        // Opcional: ocultar completamente
        if (currentFade != null) StopCoroutine(currentFade);
        currentFade = StartCoroutine(FadeOut());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && CanInteract())
        {
            if (currentFade != null) StopCoroutine(currentFade);
            currentFade = StartCoroutine(FadeIn());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (currentFade != null) StopCoroutine(currentFade);
            currentFade = StartCoroutine(FadeOut());
        }
    }

    IEnumerator FadeIn()
    {
        interactionCanvas.SetActive(true);

        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float progress = t / fadeDuration;

            canvasGroup.alpha = progress;
            yield return null;
        }

        canvasGroup.alpha = 1f;
    }

    IEnumerator FadeOut()
    {
        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float progress = t / fadeDuration;

            canvasGroup.alpha = 1f - progress;
            yield return null;
        }

        canvasGroup.alpha = 0f;
        interactionCanvas.SetActive(false);
    }
}