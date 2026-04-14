using UnityEngine;

public abstract class Being : MonoBehaviour
{
    protected int currentHealth;
    public int CurrentHealth {get => currentHealth;}
    public abstract int MaxHealth { get; } // Abstract because every subclass must implement its own MaxHealth, that will initialize the "currentHealth" property in the Awake method. 
    public bool IsAlive { get; protected set; }

    protected virtual void Awake()
    {
        currentHealth = MaxHealth;
        IsAlive = true;
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
