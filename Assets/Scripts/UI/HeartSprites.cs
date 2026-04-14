using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartSprites : MonoBehaviour
{

    [SerializeField] GameObject[] heartSprites;

    private void OnEnable()
    {
        PlayerHealth.OnDamageTaken += setSprites;

    }
    private void OnDisable()
    {
        PlayerHealth.OnDamageTaken -= setSprites;

    }
    private void setSprites (int amount)
    {
        for (int i = 0; i < heartSprites.Length; i++)
        {
            if (i < amount)
            {
                heartSprites[i].GetComponent<Animator>().SetBool("isFull",true);
            }
            else
            {
                heartSprites[i].GetComponent<Animator>().SetBool("isFull", false);
            }
        }
    }
}
