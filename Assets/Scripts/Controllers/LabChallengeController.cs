using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class LabChallengeController : MonoBehaviour
{
    public static LabChallengeController Instance;
    [SerializeField] GameObject winCanvas, orbsCounter, npc;
    [SerializeField] BreathAbility breathAbility;
    [SerializeField] UnityEvent afterWinning;
    [Header("Reset Lab Camera")]
    [SerializeField] GameObject labCamera;
    int completedPuzzles;
    int puzzlesCount;
    Vector3 originalCameraPosition;
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
        originalCameraPosition = labCamera.transform.position;
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
        afterWinning?.Invoke();
        labCamera.transform.position = originalCameraPosition;
        npc.SetActive(false);
        FearManager.Instance.ReduceFear(100f, true);
        FearManager.Instance.ChangeTimerState(false);
        orbsCounter.SetActive(true);
        breathAbility.DisableAbility();
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(6.5f);
        Time.timeScale = 1f;
        PlayerSpawner.Instance.SpawnPlayerOnCurrentSpawnPoint();
        winCanvas.SetActive(false);
    }
}
