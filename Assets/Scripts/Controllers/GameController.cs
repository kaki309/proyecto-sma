using System.Collections;
using UnityEngine;
public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    [Header("UI")]
    [SerializeField] GameObject gameOverCanvas;
    [SerializeField] GameObject pauseCanvas;

    [Header("Scenes")]
    [SerializeField] string mainMenuSceneName = "MainMenu";
    [SerializeField] string mainWorldSceneName = "MainWorld";

    // Internal Config
    Character currentCharacter;
    GameObject playerObject = null;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start()
    {
        gameOverCanvas.SetActive(false);
    }
    public Character GetCurrentCharacter()
    {
        return currentCharacter;
    }
    public void SetCurrentCharacter(Character character)
    {
        currentCharacter = character;
        Debug.Log($"[GameController] Character {character} set in GameController");
        SceneController.Instance.LoadSceneWithLoadingScreen(mainWorldSceneName);
        // Wait 4 seconds to give time to the loadScreen
        // And then activate the character's sprite
        Invoke(nameof(ActivateCharacterSprite), 4);
    }
    public void GameOver()
    {
        Time.timeScale = 1f;
        StartCoroutine(ChangeGameOverCanvasStateDelayed(true, 3f));
    }
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneController.Instance.LoadSceneWithLoadingScreen(mainMenuSceneName);
        AudioController.Instance.StopSfx();
        StartCoroutine(ChangeGameOverCanvasStateDelayed(false, 1.5f));
        StartCoroutine(ChangePauseCanvasStateDelayed(false, 1.5f));
    }
    public void RetryGame()
    {
        Time.timeScale = 1f;
        SceneController.Instance.LoadSceneWithLoadingScreen(mainWorldSceneName);
        AudioController.Instance.StopSfx();
        StartCoroutine(ChangeGameOverCanvasStateDelayed(false, 0.25f));
        Invoke(nameof(ActivateCharacterSprite), 3);
    }
    public void PauseGame()
    {
        Time.timeScale = 0f;
        AudioController.Instance.PauseAllAudio();
        pauseCanvas.SetActive(true);
    }
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        AudioController.Instance.ResumeAllAudio();
        pauseCanvas.SetActive(false);
    }
    public void QuitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }
    // ----------------> Internal methods
    void ActivateCharacterSprite() => StartCoroutine(SearchAndConfigurePlayer());
    IEnumerator SearchAndConfigurePlayer()
    {
        // Find the player

        while (playerObject == null)
        {
            playerObject = GameObject.FindGameObjectWithTag("Player");
            yield return null;
        }

        PlayerHealth.OnPlayerDeath += GameOver;

        // Get the right sprite GameObject
        var allChildren = playerObject.GetComponentsInChildren<Transform>(true); // true => include inactive
        GameObject sprite = null;

        switch (currentCharacter)
        {
            case Character.Mateo:
                foreach (var obj in allChildren)
                {
                    if (obj.CompareTag("MateoSprite"))
                    {
                        sprite = obj.gameObject;
                        break;
                    }
                }
                break;

            case Character.Sara:
                foreach (var obj in allChildren)
                {
                    if (obj.CompareTag("SaraSprite"))
                    {
                        sprite = obj.gameObject;
                        break;
                    }
                }
                break;
        }
        // Enable the sprite
        sprite.SetActive(true);
        // Finish the coroutine
        yield break;
    }
    IEnumerator ChangeGameOverCanvasStateDelayed(bool desiredState, float time)
    {
        yield return new WaitForSecondsRealtime(time);
        gameOverCanvas.SetActive(desiredState);
    }
    IEnumerator ChangePauseCanvasStateDelayed(bool desiredState, float time)
    {
        yield return new WaitForSecondsRealtime(time);
        pauseCanvas.SetActive(desiredState);
    }
}
