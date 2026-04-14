using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LoadingSceneController : MonoBehaviour
{
    [SerializeField] private Slider loadingSlider;
    [SerializeField] private TMP_Text adviceText;

    [SerializeField] private List<string> advices;
    [SerializeField] private float adviceChangeTime = 4f;

    private float loadingScreenTime = 6f;
    private string sceneToLoad;

    public void SetSceneAndStartLoad(string sceneName)
    {
        sceneToLoad = sceneName;
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad);
        asyncLoad.allowSceneActivation = false;

        float timer = 0f;
        float adviceTimer = 0f;

        ShowRandomAdvice();

        while (timer < loadingScreenTime || asyncLoad.progress < 0.9f)
        {
            timer += Time.deltaTime;
            adviceTimer += Time.deltaTime;

            // Progress bar update with slow progression
            float timeProgressRatio = timer / (loadingScreenTime * 0.8f); // 0 to 1 over 80% of loading time
            float slowProgress = Mathf.Clamp01(timeProgressRatio * 0.6f); // Maps 0-1 to 0-0.6
            float realProgress = Mathf.Clamp01(asyncLoad.progress / 0.9f);

            // After 80% of loading time, smoothly transition to real progress
            if (timer >= loadingScreenTime * 0.8f)
            {
                float remainingTime = loadingScreenTime * 0.2f;
                float blendFactor = (timer - loadingScreenTime * 0.8f) / remainingTime;
                loadingSlider.value = Mathf.Lerp(slowProgress, realProgress, blendFactor);
            }
            else
            {
                loadingSlider.value = slowProgress;
            }

            // change advice
            if (adviceTimer >= adviceChangeTime)
            {
                ShowRandomAdvice();
                adviceTimer = 0f;
            }

            yield return null;
        }

        asyncLoad.allowSceneActivation = true;
    }

    void ShowRandomAdvice()
    {
        if (advices.Count == 0) return;

        int index = Random.Range(0, advices.Count);
        adviceText.text = advices[index];
    }
}