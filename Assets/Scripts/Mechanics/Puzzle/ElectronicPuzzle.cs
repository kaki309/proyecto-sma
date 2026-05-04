using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ElectronicPuzzle : MonoBehaviour
{
    [SerializeField] Image lightBulb;
    [SerializeField] Sprite[] lightBulbAnimationFrames;
    ElectronicBlock[] blocks;
    PuzzleUI puzzleCanvas;
    void Start()
    {
        puzzleCanvas = GetComponentInParent<PuzzleUI>();
        blocks = GetComponentsInChildren<ElectronicBlock>().OrderBy(block => block.Order).ToArray();
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
        StartCoroutine(FinishPuzzle());
    }

    // --------------------------------- ANIMATIONS
    IEnumerator FinishPuzzle()
    {
        // HightLight Blocks In Order
        foreach (ElectronicBlock block in blocks)
        {
            block.HighlightBlock();
            yield return new WaitForSeconds(0.1f);
        }
        // Change bulb sprite like an animation
        foreach (Sprite frame in lightBulbAnimationFrames)
        {
            lightBulb.sprite = frame;
            yield return new WaitForSeconds(0.3f);
        }
        yield return new WaitForSeconds(3f);
        puzzleCanvas.Hide();
    }
}
