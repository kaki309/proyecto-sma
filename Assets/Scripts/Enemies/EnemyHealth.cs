using System.Collections;
using UnityEngine;

public class EnemyHealth : Being
{
    public override int MaxHealth => _maxHealth;
    [SerializeField] int _maxHealth = 1;
    Animator animator;
    Rigidbody2D rb;
    SpriteRenderer sprite;
    [SerializeField] Collider2D[] colliders;

    void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        animator = transform.parent.GetComponentInChildren<Animator>();
        sprite = transform.parent.GetComponentInChildren<SpriteRenderer>();
        rb.gravityScale = 0;
    }
    public override void TakeDamage(int damage = 1)
    {
        if (currentHealth > 1) StartCoroutine(PlayDamageEffect());
        base.TakeDamage(damage);
    }
    protected override void Die()
    {
        transform.parent.GetComponentInParent<EnemyIA>().enabled = false;
        animator.SetBool("isDead", true);
        rb.gravityScale = 1;
        foreach (Collider2D collider in colliders)
        {
            collider.enabled = false;
        }
        Invoke(nameof(DestroyObject), 5f);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            playerRb.velocity = new Vector2(0, 0);
            playerRb.AddForce(Vector2.up * 3, ForceMode2D.Impulse);
            TakeDamage();
        }
    }
    void DestroyObject()
    {
        Destroy(transform.parent.gameObject);
    }
    IEnumerator PlayDamageEffect()
    {
        sprite.enabled = false;
        yield return new WaitForSeconds(0.2f);
        sprite.enabled = true;
        yield return new WaitForSeconds(0.2f);
        sprite.enabled = false;
        yield return new WaitForSeconds(0.2f);
        sprite.enabled = true;
        yield return new WaitForSeconds(0.2f);
        sprite.enabled = false;
        yield return new WaitForSeconds(0.2f);
        sprite.enabled = true;
    }
}
