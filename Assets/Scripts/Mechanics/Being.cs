using UnityEngine;

public abstract class Being : MonoBehaviour
{
    [SerializeField] protected float maxHealth = 100f;
    protected float currentHealth;
    protected bool isAlive = true;

    public float CurrentHealth 
    { 
        get => currentHealth;
        protected set => currentHealth = value;
    }

    public float MaxHealth => maxHealth;

    public bool IsAlive 
    { 
        get => isAlive;
        protected set => isAlive = value;
    }

    protected virtual void Awake()
    {
        currentHealth = maxHealth;
    }

    public virtual void TakeDamage(float damage)
    {
        if (!IsAlive) return;

        CurrentHealth -= damage;

        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            IsAlive = false;
            Die();
        }
    }

    protected abstract void Die();
}
