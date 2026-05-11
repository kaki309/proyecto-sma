using System.Collections;
using TMPro;
using UnityEngine;

public class NotificationController : MonoBehaviour
{
    [SerializeField] RectTransform notificationBar;
    
    [SerializeField] TMP_Text notificationText;
    [SerializeField] float transitionDuration = 0.5f;
    [SerializeField] float onScreenTime = 4f;
    [SerializeField] float offsetY = 400f;
    Vector2 targetPos;
    Vector2 startPos;

    void Start()
    {
        Setup();
        notificationText.text = "";
    }
    void Setup()
    {
        targetPos = notificationBar.anchoredPosition;
        startPos = targetPos + Vector2.up * offsetY;

        notificationBar.anchoredPosition = startPos;
    }
    IEnumerator AnimateIn()
    {
        float t = 0f;

        while (t < transitionDuration)
        {
            t += Time.deltaTime;
            float progress = t / transitionDuration;

            float ease = 1f - Mathf.Pow(1f - progress, 3);

            notificationBar.anchoredPosition = Vector2.Lerp(startPos, targetPos, ease);

            yield return null;
        }

        notificationBar.anchoredPosition = targetPos;
    }
    IEnumerator AnimateOut()
    {
        float t = 0f;

        while (t < transitionDuration)
        {
            t += Time.deltaTime;
            float progress = t / transitionDuration;

            float ease = Mathf.Pow(progress, 3);

            notificationBar.anchoredPosition = Vector2.Lerp(targetPos, startPos, ease);

            yield return null;
        }
    }

    public void ShowNotification(string text)
    {
        notificationText.text = text;
        StartCoroutine(showNotificationCoroutine());
    }

    IEnumerator showNotificationCoroutine()
    {
        yield return AnimateIn();
        yield return new WaitForSeconds(onScreenTime);
        yield return AnimateOut();
    }
}
