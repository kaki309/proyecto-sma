using UnityEngine;

public interface IDamageable
{
    float CurrentHealth { get; }
    float MaxHealth { get; }
    bool IsAlive { get; }
    
    void TakeDamage(float damage);
}
