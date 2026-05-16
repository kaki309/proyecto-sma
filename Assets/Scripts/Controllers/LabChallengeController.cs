using System.Collections;
using UnityEngine;

public class LabChallengeController : MonoBehaviour
{
    public static LabChallengeController Instance;
    [SerializeField] GameObject winCanvas, orbsCounter, npc;
    [SerializeField] BreathAbility breathAbility;
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
        else
        {
            FearManager.Instance.ReduceFear(10f);
            NotificationController.Instance.ShowNotification("Caja eléctrica arreglada");
        }
    }
    IEnumerator winChallenge()
    {
        winCanvas.SetActive(true);
        npc.SetActive(false);
        FearManager.Instance.ReduceFear(100f, true);
        FearManager.Instance.ChangeTimerState(false);
        orbsCounter.SetActive(true);
        breathAbility.DisableAbility();
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(6.5f);
        Time.timeScale = 1f;
        PlayerSpawner.Instance.SpawnPlayerOnWorldSpawnpoint(WorldSpawnPoints.outsideLab);
        winCanvas.SetActive(false);
    }
}
