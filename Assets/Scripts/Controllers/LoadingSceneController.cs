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

            float timeToKeepProgressFixed = 6f;
            float loadPercentageForFixedProgress = 40f;

            float fixedTimeProgressRatio = timer / (loadingScreenTime * (timeToKeepProgressFixed / 10)); // this goes from 0 to 1 over timeToKeepProgressFixed seconds

            float fixedProgress = Mathf.Clamp01(fixedTimeProgressRatio * (loadPercentageForFixedProgress / 100)); // Maps fixedTimeProgressRatio value (0:1) to 0-loadPercentageForFixedProgress (In decimal numbers)

            float realProgress = Mathf.Clamp01(asyncLoad.progress / 0.9f);

            // After timeToKeepProgressFixed of loading time, smoothly transition to real progress
            if (timer >= loadingScreenTime * (timeToKeepProgressFixed / 10))
            {
                float remainingTime = loadingScreenTime * (1- timeToKeepProgressFixed/10);
                float blendFactor = (timer - loadingScreenTime * (timeToKeepProgressFixed / 10)) / remainingTime;
                
                loadingSlider.value = Mathf.Lerp(fixedProgress, realProgress, blendFactor);
            }
            else
            {
                loadingSlider.value = fixedProgress;
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