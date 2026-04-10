using System;
using UnityEngine;

public class Player : Being
{
    public static event Action<float> OnDamageTaken;
    public static event Action OnPlayerDeath;

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        OnDamageTaken?.Invoke(CurrentHealth);
    }

    protected override void Die()
    {
        OnPlayerDeath?.Invoke();
        // TODO: Handle death (game over screen, respawn, etc)
    }
}
