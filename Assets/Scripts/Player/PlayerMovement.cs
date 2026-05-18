using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{

    [Header("Movement")]
    [SerializeField] float moveSpeed = 30;
    [Tooltip("Multiplier for movement effects. Default = 8")]
    [SerializeField] float moveMultiplierOnGround = 8;
    [Tooltip("Multiplier for movement while falling. Default = 4")]
    [SerializeField] float moveMultiplierOnAir = 4;

    [Header("Jump")]
    [SerializeField] float jumpForce = 5;
    [SerializeField] float sustainedJumpForce = 4;
    [SerializeField] Transform groundCheckLeft;
    [SerializeField] Transform groundCheckRight;
    [SerializeField] float checkDistance = 0.12f;
    [SerializeField] LayerMask groundLayer;

    // Dust Effect
    [Header("Effects")]
    [SerializeField] private GameObject dustEffect;

    // Events
    public static Action<float, Vector2> OnLandingEvent;


    // Interal variables
    InputSystem_Actions actions;
    Rigidbody2D rb;
    Animator animator;
    float move;
    float _moveMultiplier;
    bool isGrounded;
    bool hasLandedAlready = true;
    bool isChangingMoveSpeed;
    bool isJumping;
    bool hasAppliedJumpImpulse;
    float jumpHoldTime;
    const float MAX_JUMP_HOLD_TIME = 1f;
    bool isPlayerDead;
    float defaultGravityScale;
    float timeScaleCompensation;

    // -------------------------- UNITY METHODS
    void Awake()
    {
        actions = new InputSystem_Actions();
    }
    void OnEnable()
    {
        actions.Player.Enable();
        // Move
        actions.Player.Move.performed += PerformMovement;
        actions.Player.Move.canceled += PerformMovement;
        // Jump
        actions.Player.Jump.performed += PerformJump;
        actions.Player.Jump.canceled += PerformJump;

        PlayerHealth.OnPlayerDeath += changePlayerLifeStateToDead;
    }
    void OnDisable()
    {
        unsubscribeFromEvents();
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(GetSpriteAnimator());
        _moveMultiplier = moveMultiplierOnGround;
        defaultGravityScale = rb.gravityScale;
        timeScaleCompensation = 1f / Time.timeScale;
    }
    void Update()
    {
        if (isPlayerDead) return;
        // Check for ground
        CheckGrounded();
        // Check when is landing
        CheckLandingAndInvokeInteractions();
        // Update jump state after max hold time has passed
        UpdateJumpState();
        // Set movement speed internal multiplier
        SetMovementSpeed();
        // Change Direction of View
        SetSpriteDirection();
        // Walk Animation
        SetWalkingAnimation();
        // Animation while being on air (Jump or fall)
        SetOnAirAnimations();
    }
    void FixedUpdate()
    {
        timeScaleCompensation = 1f / Time.timeScale;
        rb.velocity = new Vector2(move * moveSpeed * _moveMultiplier * Time.fixedUnscaledDeltaTime, rb.velocity.y);

        rb.gravityScale = defaultGravityScale * timeScaleCompensation;
        ApplyJumpForce();
    }
    // -------------------------- MOVEMENT METHODS
    void PerformMovement(InputAction.CallbackContext ctx)
    {
        move = ctx.ReadValue<Vector2>().x;
    }
    void PerformJump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (isGrounded && !isJumping)
            {
                isJumping = true;
                hasAppliedJumpImpulse = false;
                jumpHoldTime = 0f;
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }
        }
        else if (ctx.canceled)
        {
            isJumping = false;
        }
    }
    void CheckGrounded()
    {
        bool checkGroundOnLeft = Physics2D.Raycast(groundCheckLeft.position, Vector2.down, checkDistance, groundLayer);
        bool checkGroundOnRight = Physics2D.Raycast(groundCheckRight.position, Vector2.down, checkDistance, groundLayer);
        // If any of the checks gets true, then the player is touching the ground:
        isGrounded = checkGroundOnLeft || checkGroundOnRight;
    }
    void CheckLandingAndInvokeInteractions()
    {
        if (!isGrounded)
        {
            hasLandedAlready = false;
            return;
        }

        // If is grounded and hasnt landed already, it is landing
        if (isGrounded && !hasLandedAlready)
        {
            hasLandedAlready = true;
            OnLandingEvent?.Invoke(rb.velocity.y, transform.position);
        }
    }
    void UpdateJumpState()
    {
        // Auto-stop jump if max hold time reached
        if (isJumping && jumpHoldTime >= MAX_JUMP_HOLD_TIME)
        {
            isJumping = false;
        }
    }
    void SetMovementSpeed()
    {
        float desiredSpeed = isGrounded ? moveMultiplierOnGround : moveMultiplierOnAir;

        if (_moveMultiplier != desiredSpeed && !isChangingMoveSpeed)
            StartCoroutine(ChangeMovementMultiplierProgressively(desiredSpeed));
    }
    IEnumerator ChangeMovementMultiplierProgressively(float desiredSpeed)
    {
        isChangingMoveSpeed = true;
        float duration = 0.5f;
        float elapsed = 0f;
        float startSpeed = _moveMultiplier;

        while (elapsed < duration)
        {
            _moveMultiplier = Mathf.Lerp(startSpeed, desiredSpeed, elapsed / duration);
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        _moveMultiplier = desiredSpeed;
        isChangingMoveSpeed = false;
    }
    void ApplyJumpForce()
    {
        // Apply jump force while holding button
        if (isJumping && jumpHoldTime < MAX_JUMP_HOLD_TIME)
        {
            // Apply initial impulse on first frame of jump
            if (!hasAppliedJumpImpulse)
            {
                float jumpCompensation = Mathf.Sqrt(timeScaleCompensation);
                rb.AddForce(new Vector2(0, jumpForce * jumpCompensation), ForceMode2D.Impulse);
                hasAppliedJumpImpulse = true;
            }

            // Apply sustain force to extend jump
            rb.AddForce(new Vector2(0, sustainedJumpForce * timeScaleCompensation), ForceMode2D.Force);
            jumpHoldTime += Time.fixedDeltaTime;
        }
    }
    // -------------------------- VISUAL CHANGES
    IEnumerator GetSpriteAnimator()
    {
        Animator _anim = null;
        while (_anim == null)
        {
            _anim = GetComponentInChildren<Animator>();
            yield return null;
        }
        animator = _anim;
        yield break;
    }
    void SetSpriteDirection()
    {
        if (move < 0)
        {
            // Moving to the left
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }
        else if (move > 0)
        {
            // Moving to the right
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }
    }
    void SetWalkingAnimation()
    {
        if (move != 0)
        { animator.SetBool("isWalking", true); }
        else { animator.SetBool("isWalking", false); }
    }
    void SetOnAirAnimations()
    {
        if (isGrounded)
        {
            animator.SetBool("isFalling", false);
            animator.SetBool("isJumping", false);
        }
        else if (isJumping) { animator.SetBool("isJumping", true); }
        // Negative velocity means Falling
        else if (rb.velocity.y < 0)
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", true);
        }
    }

    // -------------------------- OTHERS
    void changePlayerLifeStateToDead()
    {
        unsubscribeFromEvents();
        move = 0;
        isPlayerDead = true;
    }
    void unsubscribeFromEvents()
    {
        actions.Player.Disable();
        actions.Player.Move.performed -= PerformMovement;
        actions.Player.Move.canceled -= PerformMovement;
        actions.Player.Jump.performed -= PerformJump;
        actions.Player.Jump.canceled -= PerformJump;

        PlayerHealth.OnPlayerDeath -= changePlayerLifeStateToDead;
    }
    // -------------------------- EDITOR HELPERS
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(groundCheckLeft.position, new Vector2(groundCheckLeft.position.x, groundCheckLeft.position.y - checkDistance));
        Gizmos.DrawLine(groundCheckRight.position, new Vector2(groundCheckRight.position.x, groundCheckRight.position.y - checkDistance));
    }
}
