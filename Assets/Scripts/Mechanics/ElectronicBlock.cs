using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class ElectronicBlock : MonoBehaviour
{
    public bool IsInRightPose { get; private set; }
    [SerializeField] Sprite rightPosition;
    [SerializeField] Sprite blockHighlighted;
    [SerializeField] Sprite[] wrongPositions;
    Image image;
    List<Sprite> allSprites;
    int currentSpriteIndex;
    ElectronicPuzzle puzzleController;

    void Start()
    {
        allSprites = new List<Sprite>();
        image = GetComponent<Image>();
        allSprites.Add(rightPosition);
        allSprites.AddRange(wrongPositions);
        setRandomRotation();
    }
    public void RotateBlock()
    {
        image.sprite = allSprites[currentSpriteIndex + 1];
        checkIfRightPose();
        puzzleController.checkPuzzleState();
    }
    void setRandomRotation()
    {
        int random = Random.Range(0, allSprites.Count + 1);
        currentSpriteIndex = random;
        image.sprite = allSprites[random];
        checkIfRightPose();
    }
    void checkIfRightPose()
    {
        if (image.sprite == rightPosition)
        {
            IsInRightPose = true;
        }
        else
        {
            IsInRightPose = false;
        }
    }
    public void SetMyparent(ElectronicPuzzle controller)
    {
        puzzleController = controller;
    }
    public void HighlightBlock()
    {
        image.sprite = blockHighlighted;
    }
}