using System.Collections;
using UnityEngine;

public class EnemyHealth : Being
{
    public override int MaxHealth => _maxHealth;
    [SerializeField] int _maxHealth = 1;
    [SerializeField] Collider2D[] collidersToDisable;
    [SerializeField] EnemyIA iaController;
    [SerializeField] float gravityScale = 5f;
    [SerializeField] LayerMask groundLayer;

    float groundRaycastDistance = 0.1f;
    Transform rayCastOrigin;
    Animator animator;
    SpriteRenderer sprite;
    Vector3 velocity;
    bool isDead = false;

    void Start()
    {
        sprite = transform.parent.GetComponentInChildren<SpriteRenderer>();
        animator = sprite.GetComponent<Animator>();
        velocity = Vector3.zero;
        rayCastOrigin = sprite.transform;
    }

    void Update()
    {
        if (isDead) ApplyGravity();
    }

    void ApplyGravity()
    {
        // Check if touching ground with raycast
        bool touchingGround = Physics2D.Raycast(rayCastOrigin.position, Vector2.down, groundRaycastDistance, groundLayer);

        if (!touchingGround)
        {
            // Apply gravity
            velocity.y -= gravityScale * Time.deltaTime;

            // Apply to parent transform (where NavMeshAgent is)
            transform.parent.position += new Vector3(0, velocity.y * Time.deltaTime, 0);
        }
        else
        {
            // Stop vertical movement when touching ground
            velocity.y = 0;
        }
    }

    public override void TakeDamage(int damage = 1)
    {
        if (currentHealth > 1) StartCoroutine(PlayDamageEffect());
        base.TakeDamage(damage);
    }
    protected override void Die()
    {
        isDead = true;
        iaController.IsDead = true;
        animator.SetBool("isDead", true);
        foreach (Collider2D collider in collidersToDisable)
        {
            collider.enabled = false;
        }
        Invoke(nameof(DestroyObject), 4f);
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
