using UnityEngine;

public class BreathAbility : MonoBehaviour
{

    [SerializeField] GameObject buttonEnabled;
    [SerializeField] GameObject buttonDisabled;

    void Start()
    {
        if (buttonEnabled != null) buttonEnabled.SetActive(true);
        if (buttonDisabled != null) buttonDisabled.SetActive(false);
    }


    public void UseAbility()
    {
        buttonDisabled.SetActive(true);
        buttonEnabled.SetActive(false);
    }
    public void ResetAbility()
    {
        buttonEnabled.SetActive(true);
        buttonDisabled.SetActive(false);
    }
}