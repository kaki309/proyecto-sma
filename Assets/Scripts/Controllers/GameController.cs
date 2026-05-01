using System.Collections;
using UnityEngine;
public class GameController : MonoBehaviour
{
    public static GameController Instace { get; private set; }

    [Header("UI")]
    [SerializeField] GameObject gameOverCanvas;

    [Header("Scenes")]
    [SerializeField] string mainMenuSceneName = "MainMenu";
    [SerializeField] string mainWorldSceneName = "MainWorld";

    // Internal Config
    Character currentCharacter;

    void Awake()
    {
        if (Instace != null && Instace != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instace = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start()
    {
        gameOverCanvas.SetActive(false);
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
        PlayerHealth.OnPlayerDeath -= GameOver;
        Time.timeScale = 0f;
        gameOverCanvas.SetActive(true);
    }
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneController.Instance.LoadSceneWithLoadingScreen(mainMenuSceneName);
    }
    // ----------------> Internal methods
    void ActivateCharacterSprite() => StartCoroutine(SearchAndConfigurePlayer());
    IEnumerator SearchAndConfigurePlayer()
    {
        // Find the player
        GameObject player = null;
        while (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            yield return null;
        }

        PlayerHealth.OnPlayerDeath += GameOver;

        // Get the right sprite GameObject
        var allChildren = player.GetComponentsInChildren<Transform>(true); // true => include inactive
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
}
