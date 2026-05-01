using System;
using UnityEngine;

public class PlayerHealth : Being
{
    public override int MaxHealth => 5;
    public static event Action<int> OnDamageTaken;
    public static event Action OnPlayerDeath;
    [SerializeField] PlayerMovement playerMovement;

    void OnEnable()
    {
        playerMovement.OnFallEvent += HandleFallDamage;
    }

    void OnDisable()
    {
        playerMovement.OnFallEvent -= HandleFallDamage;
    }

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

    void HandleFallDamage(float fallSpeed)
    {
        int damage = 0;

        if (fallSpeed < 2f)
            damage = 0;
        else if (fallSpeed < 10f)
            damage = 1;
        else if (fallSpeed < 14f)
            damage = 2;
        else if (fallSpeed < 18f)
            damage = 4;
        else
            damage = MaxHealth;

        Debug.Log("Fall damage: " + fallSpeed + ", Health: " + currentHealth);
        TakeDamage(damage);
    }
}
