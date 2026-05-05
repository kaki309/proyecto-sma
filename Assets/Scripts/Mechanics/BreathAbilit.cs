using UnityEngine;

public class BreathAbility : MonoBehaviour
{
    
    public GameObject spriteEnabled;
    public GameObject spriteDisabled;

    void Start()
    {
     
        if (spriteEnabled != null) spriteEnabled.SetActive(true);
        if (spriteDisabled != null) spriteDisabled.SetActive(false);
    }

   
    public void UseAbility()
    {
       
        if (spriteEnabled != null) spriteEnabled.SetActive(false);
        if (spriteDisabled != null) spriteDisabled.SetActive(true);
    }
}