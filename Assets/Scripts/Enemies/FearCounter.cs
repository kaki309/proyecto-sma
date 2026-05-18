using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class FearManager : MonoBehaviour
{
    public static FearManager Instance { get; private set; }

    [Header("References")]
    [SerializeField] private TMP_Text counterText;
    [SerializeField] private Image fearFillImage;

    [Header("Settings")]
    [SerializeField] private float secondsPerPercent = 1.6f;
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private List<AudioClip> fearClips;

    private bool isScreaming = false;
    private float maxFear = 100f;
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

            CheckFearMilestone();
        }
        else
        {
            isTimerEnabled = false;
            GameController.Instance.GameOver();
        }
    }

    void CheckFearMilestone()
    {
        if (isScreaming) return;
        if (audioSource == null || fearClips == null || fearClips.Count == 0) return;

        bool atMilestone = currentFearPercent % 20 == 0;
        if (!atMilestone) return;

        AudioClip clip = fearClips[Random.Range(0, fearClips.Count)];
        StartCoroutine(PlayScream(clip));
    }

    IEnumerator PlayScream(AudioClip clip)
    {
        isScreaming = true;
        audioSource.PlayOneShot(clip);
        yield return new WaitForSecondsRealtime(clip.length);
        isScreaming = false;
    }
    void UpdateInterface()
    {
        if (counterText != null)
        { counterText.text = currentFearPercent.ToString("0") + "%"; }

        if (fearFillImage != null)
        { fearFillImage.fillAmount = currentFearPercent / maxFear; }

        CheckFearMilestone();
    }
    public void StartTimer() => isTimerEnabled = true;

    public float GetCurrentFear()
    {
        return currentFearPercent;
    }
    public void ReduceFear(float amount = 20f, bool instantly = false)
    {
        if (!isTimerEnabled) return;
        if (amount > currentFearPercent) amount = currentFearPercent;

        if (instantly)
        {
            currentFearPercent -= amount;
            UpdateInterface();
        }
        else
        {
            StartCoroutine(ReduceFearAnimation(amount));
        }
    }

    private IEnumerator ReduceFearAnimation(float amount)
    {
        float timer = 0f;
        float animationDuration = 3f;
        float startFear = currentFearPercent;
        float targetFear = startFear - amount;

        isTimerEnabled = false;
        while (timer < animationDuration)
        {
            timer += Time.deltaTime;
            float progress = timer / animationDuration;
            currentFearPercent = Mathf.Lerp(startFear, targetFear, progress);
            currentFearPercent = Mathf.Clamp(currentFearPercent, 0, maxFear);
            UpdateInterface();
            yield return null;
        }

        currentFearPercent = targetFear;
        currentFearPercent = Mathf.Clamp(currentFearPercent, 0, maxFear);
        UpdateInterface();
        isTimerEnabled = true;
    }
    public void ChangeTimerState(bool desiredState) => isTimerEnabled = desiredState;
}