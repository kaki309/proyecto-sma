using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FearManager : MonoBehaviour
{
    public static FearManager Instance { get; private set; }

    [Header("References")]
    [SerializeField] private TMP_Text counterText;
    [SerializeField] private Image fearFillImage;

    [Header("Settings")]
    [SerializeField] private float secondsPerPercent = 2f;
    [SerializeField] private float maxFear = 100f;

    private bool isTimerEnabled;
    private float currentFearPercent = 0f;
    private float timer = 0f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        isTimerEnabled = false;
    }
    void Update()
    {
        if (!isTimerEnabled) return;
        if (currentFearPercent < maxFear)
        {
            timer += Time.deltaTime;

            if (timer >= secondsPerPercent)
            {
                currentFearPercent += 1f;
                timer = 0f;
                currentFearPercent = Mathf.Clamp(currentFearPercent, 0, maxFear);
                UpdateInterface();
            }
        }
        else
        {
            isTimerEnabled = false;
            GameController.Instance.GameOver();
        }
    }
    public void StartTimer() => isTimerEnabled = true;

    void UpdateInterface()
    {
        if (counterText != null)
        {
            counterText.text = currentFearPercent.ToString("0") + "%";
        }

        if (fearFillImage != null)
        {
            fearFillImage.fillAmount = currentFearPercent / maxFear;
        }
    }

    public float GetCurrentFear()
    {
        return currentFearPercent;
    }

    public void ReduceFear(float amount = 20f)
    {
        currentFearPercent -= amount;
        currentFearPercent = Mathf.Clamp(currentFearPercent, 0, maxFear);
        UpdateInterface();
    }

}