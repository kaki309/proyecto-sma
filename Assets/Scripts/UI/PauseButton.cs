using UnityEngine;

public class PauseButton : MonoBehaviour
{
    public void Pause()
    {
        GameController.Instance.PauseGame();
    }
}
