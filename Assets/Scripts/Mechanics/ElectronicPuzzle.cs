using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ElectronicPuzzle : MonoBehaviour
{
    [SerializeField] Image lightBulb;
    [SerializeField] Sprite[] lightBulbAnimationFrames;
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
        foreach (ElectronicBlock block in blocks)
        {
            block.HighlightBlock();
            yield return new WaitForSeconds(0.3f);
        }
        foreach (Sprite frame in lightBulbAnimationFrames)
        {
            lightBulb.sprite = frame;
            yield return new WaitForSeconds(0.3f);
        }
    }
}
