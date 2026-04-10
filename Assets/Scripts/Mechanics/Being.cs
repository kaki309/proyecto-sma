using UnityEngine;

public class Being : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth;

    protected bool isDead = false;

    protected virtual void Awake()
    {
            currentHealth = maxHealth;
    }

    public virtual void TakeDamage(float damage)
    {
        if (isDead) return;

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            isDead = true;
            Die();
        }
    }

    protected virtual void Die()
    {
    }

    public float CurrentHealth => currentHealth;
    public float MaxHealth => maxHealth;
    public bool IsAlive => !isDead;
}
