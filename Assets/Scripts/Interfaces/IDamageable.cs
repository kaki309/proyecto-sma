using UnityEngine;

public interface IDamageable
{
    float CurrentHealth { get; protected set; }
    float MaxHealth { get; }
    bool IsAlive {get; protected set;}
    
    void TakeDamage(float damage)
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

    void Die();
}
