using UnityEngine;
using UnityEngine.UI; // Necesario para manejar el componente Image
using TMPro;

public class FearManager : MonoBehaviour
{
    public static FearManager Instance { get; private set; }

    [Header("References")]
    [SerializeField] private TMP_Text counterText;
    [SerializeField] private Image fearFillImage; // La imagen con color que se llenará

    [Header("Settings")]
    [SerializeField] private float secondsPerPercent = 2f; // Cada cuántos segundos sube 1%
    [SerializeField] private float maxFear = 100f; // El límite del miedo

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

    void Update()
    {
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
    }

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
}