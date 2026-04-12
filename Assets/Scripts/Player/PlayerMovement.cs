using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [Header("Movement")]
    [SerializeField] float moveSpeed = 30;
    [Tooltip("Multiplier for movement effects. Default = 8")]
    [SerializeField] float moveMultiplierOnGround = 8;
    [Tooltip("Multiplier for movement while falling. Default = 4")]
    [SerializeField] float moveMultiplierOnAir = 4;

    [Header("Jump")]
    [SerializeField] float jumpForce = 20;
    [SerializeField] Transform groundCheckLeft;
    [SerializeField] Transform groundCheckRight;
    [SerializeField] float checkDistance = 0.12f;
    [SerializeField] LayerMask groundLayer;

    // Interal variables
    InputSystem_Actions actions;
    Rigidbody2D rb;
    Animator animator;
    float move;
    float _moveMultiplier;
    bool isGrounded;
    bool isChangingMoveSpeed;
    bool isJumping;
    float jumpHoldTime;
    const float MAX_JUMP_HOLD_TIME = 1f;

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
    }
    void OnDisable()
    {
        actions.Player.Disable();
        actions.Player.Move.performed -= PerformMovement;
        actions.Player.Move.canceled -= PerformMovement;
        actions.Player.Jump.performed -= PerformJump;
        actions.Player.Jump.canceled -= PerformJump;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        _moveMultiplier = moveMultiplierOnGround;
    }
    void Update()
    {
        // Check for ground
        CheckGrounded();
        // Set movement speed internal multiplier
        SetMovementSpeed();
        // Change Direction of View
        SetSpriteDirection();
        // Walk Animation
        SetWalkingAnimation();
        // Jump Animation
        SetJumpingAnimation();

    }
    void FixedUpdate()
    {
        rb.velocity = new Vector2(move * moveSpeed * _moveMultiplier * Time.fixedDeltaTime, rb.velocity.y);

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
                jumpHoldTime = 0f;
                rb.velocity = new Vector2(rb.velocity.x, 0);
                animator.SetTrigger("jump");
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
            elapsed += Time.deltaTime;
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
            // Scale force down as time increases for linear height growth
            float forceScale = 1f - (jumpHoldTime / MAX_JUMP_HOLD_TIME);
            rb.AddForce(new Vector2(0, jumpForce * forceScale), ForceMode2D.Force);
            jumpHoldTime += Time.fixedDeltaTime;
        }
    }
    // -------------------------- VISUAL CHANGES
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
    void SetJumpingAnimation()
    {
        if (isGrounded)
        { animator.SetBool("isFalling", false); }
        else { animator.SetBool("isFalling", true); }
    }

    // -------------------------- EDITOR HELPERS
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(groundCheckLeft.position, new Vector2(groundCheckLeft.position.x, groundCheckLeft.position.y - checkDistance));
        Gizmos.DrawLine(groundCheckRight.position, new Vector2(groundCheckRight.position.x, groundCheckRight.position.y - checkDistance));
    }
}
