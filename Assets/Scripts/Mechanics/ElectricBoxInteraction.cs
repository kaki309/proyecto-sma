using UnityEngine;

public class ElectricBoxInteraction : InteractablesForPlayer
{
    [Header("Interaction")]
    [SerializeField] GameObject puzzleCanvas;
    public override void Interact()
    {
        puzzleCanvas.SetActive(true);
    }
}
