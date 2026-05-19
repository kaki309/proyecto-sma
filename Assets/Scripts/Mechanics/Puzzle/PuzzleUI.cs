using System.Collections;
using UnityEngine;

public class PuzzleUI : MonoBehaviour
{
    [SerializeField] RectTransform puzzleContainer;
    [SerializeField] CanvasGroup blackPanel;
    [SerializeField] float panelOpacity = 0.8f;
    [SerializeField] float duration = 0.5f;
    [SerializeField] float offsetY = 1500f;

    Vector2 targetPos;
    Vector2 startPos;
    Vector2 originalPos;
    bool isInitialized = false;

    void OnEnable()
    {
        if (!isInitialized)
        {
            originalPos = puzzleContainer.anchoredPosition;
            isInitialized = true;
        }
        Setup();
        StartCoroutine(AnimateIn());
    }

    void Setup()
    {
        targetPos = originalPos;
        startPos = targetPos + Vector2.down * offsetY;

        puzzleContainer.anchoredPosition = startPos;
        blackPanel.alpha = 0f;
    }

    IEnumerator AnimateIn()
    {
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            float progress = t / duration;

            float ease = 1f - Mathf.Pow(1f - progress, 3);

            puzzleContainer.anchoredPosition = Vector2.Lerp(startPos, targetPos, ease);
            blackPanel.alpha = Mathf.Lerp(0f, panelOpacity, progress);

            yield return null;
        }

        puzzleContainer.anchoredPosition = targetPos;
        blackPanel.alpha = panelOpacity;
    }
    public void closePuzzle() => StartCoroutine(Hide());
    public IEnumerator Hide()
    {
        yield return StartCoroutine(AnimateOut());
    }

    IEnumerator AnimateOut()
    {
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            float progress = t / duration;

            float ease = Mathf.Pow(progress, 3);

            puzzleContainer.anchoredPosition = Vector2.Lerp(targetPos, startPos, ease);
            blackPanel.alpha = Mathf.Lerp(panelOpacity, 0f, progress);

            yield return null;
        }

        // Restaurar la posición original antes de desactivar
        puzzleContainer.anchoredPosition = originalPos;
        gameObject.SetActive(false);
    }
}