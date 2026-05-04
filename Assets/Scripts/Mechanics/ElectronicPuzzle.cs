using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ElectronicPuzzle : MonoBehaviour
{
    [SerializeField] Image lightBulb;
    [SerializeField] Sprite[] lightBulbAnimationFrames;
    [SerializeField] int[] blocksOrderForAnimation;
    ElectronicBlock[] blocks;
    void Start()
    {
        blocks = GetComponentsInChildren<ElectronicBlock>();
        foreach (ElectronicBlock block in blocks)
        {
            block.SetMyparent(this);
        }
    }
    public void checkPuzzleState()
    {
        foreach (ElectronicBlock block in blocks)
        {
            if (!block.IsInRightPose) return;
        }
        StartCoroutine(HightLightBlocks());
    }
    IEnumerator HightLightBlocks()
    {
        foreach (int index in blocksOrderForAnimation)
        {
            blocks[index].HighlightBlock();
            yield return new WaitForSeconds(0.3f);
        }
        // Change bulb sprite like an animation
        foreach (Sprite frame in lightBulbAnimationFrames)
        {
            lightBulb.sprite = frame;
            yield return new WaitForSeconds(0.3f);
        }
    }
}
