using System;
using System.Collections;
using UnityEngine;

public class PlayerHealth : Being
{
    public override int MaxHealth => _maxHealth;
    [SerializeField] int _maxHealth = 5;
    public static event Action<int> OnDamageTaken;
    public static event Action OnPlayerDeath;
    Animator animator;

    void Start()
    {
        StartCoroutine(GetAnimatorAsync());
    }

    void OnEnable()
    {
        PlayerMovement.OnLandingEvent += HandleFallDamage;
    }

    void OnDisable()
    {
        PlayerMovement.OnLandingEvent -= HandleFallDamage;
    }

    public override void TakeDamage(int damage = 1)
    {
        base.TakeDamage(damage);
        animator.SetTrigger("takeDamage");
        OnDamageTaken?.Invoke(currentHealth);
    }

    protected override void Die()
    {
        animator.SetTrigger("die");
        OnPlayerDeath?.Invoke();
    }

    void HandleFallDamage(float fallSpeed)
    {
        float firstLimit = -9.0f;
        float secondLimit = -11.0f;
        float thirdLimit = -13.0f;
        float fourthLimit = -16.0f;

        // Not enough height to take damage
        if (fallSpeed > firstLimit) return;

        int damage = 0;

        if (fallSpeed <= firstLimit && fallSpeed > secondLimit) damage = 1;
        if (fallSpeed <= secondLimit && fallSpeed > thirdLimit) damage = 2;
        if (fallSpeed <= secondLimit && fallSpeed > thirdLimit) damage = 4;
        if (fallSpeed <= fourthLimit) damage = _maxHealth;

        TakeDamage(damage);
        Debug.Log("Daño tomado");
    }
    IEnumerator GetAnimatorAsync()
    {
        Animator _anim = null;
        while (_anim == null)
        {
            _anim = GetComponentInChildren<Animator>();
            yield return null;
        }
        animator = _anim;
    }
}
