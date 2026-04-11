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

            // Progress bar update
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            loadingSlider.value = progress;

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