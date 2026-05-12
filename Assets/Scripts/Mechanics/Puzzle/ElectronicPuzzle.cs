using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ElectronicPuzzle : MonoBehaviour
{
    [SerializeField] Image lightBulb;
    [SerializeField] Sprite[] lightBulbAnimationFrames;
    [SerializeField] GameObject blockInteractionsPanel;
    [SerializeField] Animator electricBoxAnimator;
    ElectronicBlock[] blocks;
    PuzzleUI puzzleCanvas;
    void Start()
    {
        blockInteractionsPanel.SetActive(false);
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
        blockInteractionsPanel.SetActive(true);
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
        yield return new WaitForSeconds(1.2f);

        yield return puzzleCanvas.Hide();

        electricBoxAnimator.SetTrigger("fixBox");

        LabChallengeController.Instance.completePuzzle();
    }
}
