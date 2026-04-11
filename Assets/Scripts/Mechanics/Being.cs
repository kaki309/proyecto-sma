using UnityEngine;

public abstract class Being : MonoBehaviour
{
    protected int currentHealth;
    public abstract int MaxHealth {get;}
    public bool IsAlive {get; protected set;}

    protected virtual void Awake()
    {
        currentHealth = MaxHealth;
    }

    public virtual void TakeDamage(int damage)
    {
        if (!IsAlive) return;

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            IsAlive = false;
            Die();
        }
    }

    protected abstract void Die();
}
