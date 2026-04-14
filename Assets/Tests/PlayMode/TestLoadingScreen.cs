using UnityEngine;

public class TestLoadingScreen : MonoBehaviour
{
    public void ChangeScene(string name)
    {
        SceneController.Instance.LoadSceneWithLoadingScreen(name);
    }
}
