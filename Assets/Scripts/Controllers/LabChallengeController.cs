using System.Collections;
using UnityEngine;

public class LabChallengeController : MonoBehaviour
{
    public static LabChallengeController Instance;
    [SerializeField] GameObject winCanvas, orbsCounter;
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
        winCanvas.SetActive(false);
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
            StartCoroutine(winChallenge());
        }
    }
    IEnumerator winChallenge()
    {
        Time.timeScale = 0f;
        winCanvas.SetActive(true);
        orbsCounter.SetActive(true);
        yield return new WaitForSecondsRealtime(6.5f);
        winCanvas.SetActive(false);
        Time.timeScale = 1f;
    }
}
