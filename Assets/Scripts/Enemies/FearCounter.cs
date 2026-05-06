using UnityEngine;
using TMPro;

public class FearManager : MonoBehaviour
{

    public static FearManager Instance { get; private set; }

    [Header("References")]
    [SerializeField] private TMP_Text counterText;

    [Header("Settings")]
    [SerializeField] private float fearMultiplier = 1f;

  
    private float currentFear = 0f;

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
        
        currentFear += Time.deltaTime * fearMultiplier;

       
        UpdateInterface();
    }

    
    public float GetCurrentFearCounter()
    {
        return currentFear / 60f;
    }

    void UpdateInterface()
    {
        if (counterText != null)
        {
            
            int minutes = Mathf.FloorToInt(currentFear / 60);

            
            float remainder = currentFear % 60;
            int seconds = Mathf.RoundToInt(remainder);

            
            if (seconds == 60)
            {
                minutes++;
                seconds = 0;
            }

            
            counterText.text = minutes.ToString("0") + ":" + seconds.ToString("00");
        }
    }
}