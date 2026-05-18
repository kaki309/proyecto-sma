using System.Collections;
using UnityEngine;

public class BreathAbility : MonoBehaviour
{

    [SerializeField] GameObject buttonEnabled;
    [SerializeField] GameObject buttonDisabled;
    [SerializeField] GameObject vfx;
    [SerializeField] float transitionTime = 0.5f;
    [SerializeField] float abilityTime = 8f;
    [SerializeField] AudioSource audioSource;

    void Start()
    {
        if (buttonEnabled != null) buttonEnabled.SetActive(true);
        if (buttonDisabled != null) buttonDisabled.SetActive(false);
    }


    public void UseAbility()
    {
        buttonDisabled.SetActive(true);
        buttonEnabled.SetActive(false);
        StartCoroutine(slowTime());
    }
    public void ResetAbility()
    {
        buttonEnabled.SetActive(true);
        buttonDisabled.SetActive(false);
    }
    public void DisableAbility()
    {
        buttonEnabled.SetActive(false);
        buttonDisabled.SetActive(true);
    }
    IEnumerator slowTime()
    {
        FearManager.Instance.ReduceFear(60f);
        float timer = 0f;
        while (timer < transitionTime)
        {
            timer += Time.unscaledDeltaTime;
            float progress = timer / transitionTime;
            Time.timeScale = Mathf.Lerp(1f, 0.5f, progress);
            yield return null;
        }

        Time.timeScale = 0.5f;
        vfx.SetActive(true);
        audioSource.Play();

        yield return new WaitForSecondsRealtime(abilityTime);

        timer = 0f;
        while (timer < transitionTime)
        {
            timer += Time.unscaledDeltaTime;
            float progress = timer / transitionTime;
            Time.timeScale = Mathf.Lerp(0.5f, 1f, progress);
            yield return null;
        }

        Time.timeScale = 1f;
        vfx.SetActive(false);
        audioSource.Stop();
    }
}