using System;
using UnityEngine;

public class PlayerHealth : Being
{
    public override int MaxHealth => 5;
    public static event Action<int> OnDamageTaken;
    public static event Action OnPlayerDeath;

    public override void TakeDamage(int damage = 1)
    {
        base.TakeDamage(damage);
        OnDamageTaken?.Invoke(currentHealth);
    }

    protected override void Die()
    {
        OnPlayerDeath?.Invoke();
        // TODO: Handle death (game over screen, respawn, etc)
    }
}
