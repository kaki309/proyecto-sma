using System.Collections;
using UnityEngine;
public class LightSwitch : InteractablesForPlayer
{
    [SerializeField] private float fearReductionAmount = 20f;
    [SerializeField] Sprite[] animationFrames;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public override void Interact()
    {
        if (FearManager.Instance != null)
        {
            StartCoroutine(ChangeSprite());
            FearManager.Instance.ReduceFear(fearReductionAmount);
        }
    }
    IEnumerator ChangeSprite()
    {
        foreach (Sprite sp in animationFrames)
        {
            spriteRenderer.sprite = sp;
            yield return new WaitForSeconds(0.25f);
        }
    }
}
