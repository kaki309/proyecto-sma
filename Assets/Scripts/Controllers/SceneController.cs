using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance;

    [SerializeField] private string loadingSceneName = "LoadingScene";

    private void Awake()
    {
        // Singleton Implementation
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ChangeScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }


    public void LoadSceneWithLoadingScreen(string sceneName)
    {
        StartCoroutine(LoadLoadingScene(sceneName));
    }

    // Corrutine
    IEnumerator LoadLoadingScene(string sceneName)
    {
        // Load the loading scene in English
        SceneManager.LoadScene(loadingSceneName);

        yield return null;

        // Find the loading controller
        LoadingSceneController loader = FindObjectOfType<LoadingSceneController>();

        if (loader != null)
        {
            loader.SetSceneToLoad(sceneName);
        }
        else
        {
            Debug.LogError("LoadingSceneController not found!");
        }
    }
}