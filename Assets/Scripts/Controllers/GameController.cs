using UnityEngine;
public class GameController : MonoBehaviour
{

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
        SceneController.Instance.ChangeScene(0);
    }
}
