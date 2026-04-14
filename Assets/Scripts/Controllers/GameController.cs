using UnityEngine;
public class GameController : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject gameOverCanvas;

    [Header("Scenes")]
    [SerializeField] private string initialSceneName;

    public static GameController Instace { get; private set; }

    // Internal Config
    private Character currentCharacter;

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

    }


    void Update()
    {

    }

    public void SetCurrentCharacter(Character character)
    {
        currentCharacter = character;
        Debug.Log($"[GameController] Character {character} set in GameController");
        //SceneController.Instance.ChangeScene(0);
        //Andres aqui le estableci que cargue la escena con el loading screen, solo tienes que cambiar el nombre de la escena a cargar
        SceneController.Instance.LoadSceneWithLoadingScreen("NombredeLaEscena");
    }


    public void GameOver()
    {
        Time.timeScale = 0f;
        gameOverCanvas.SetActive(true);
    }


    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneController.Instance.LoadSceneWithLoadingScreen(initialSceneName);
    }
}
