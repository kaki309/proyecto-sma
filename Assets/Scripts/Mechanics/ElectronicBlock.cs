using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class ElectronicBlock : MonoBehaviour
{
    public bool IsInRightPose { get; private set; }
    public int Order => _order;
    [SerializeField] int _order;
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
    void setRandomRotation()
    {
        int random = Random.Range(0, allSprites.Count);
        currentSpriteIndex = random;
        image.sprite = allSprites[currentSpriteIndex];
        checkIfRightPose();
    }
    public void RotateBlock()
    {
        currentSpriteIndex += 1;
        if (currentSpriteIndex == allSprites.Count) currentSpriteIndex = 0;
        image.sprite = allSprites[currentSpriteIndex];
        checkIfRightPose();
        puzzleController.checkPuzzleState();
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