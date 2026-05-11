using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LightSwitch : InteractablesForPlayer
{
    [SerializeField] private float fearReductionAmount = 20f;

    public override void Interact()
    {
        if (FearManager.Instance != null)
        {
            FearManager.Instance.ReduceFear(fearReductionAmount);
        }
    }

}
