using UnityEngine;

public class LabChallengeController : MonoBehaviour
{
    public static LabChallengeController Instance;
    int completedPuzzles;
    int puzzlesCount;
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        puzzlesCount = GameObject.FindObjectsOfType<ElectronicPuzzle>(true).Length;
    }
    public void completePuzzle()
    {
        completedPuzzles += 1;
        checkIfAllPuzzlesCompleted();
    }
    void checkIfAllPuzzlesCompleted()
    {
        if (completedPuzzles == puzzlesCount)
        {
            NotificationController.Instance.ShowNotification("Reto completado");
        }
    }
}
