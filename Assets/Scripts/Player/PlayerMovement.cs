using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [Header("Movement")]
    [SerializeField] float moveSpeed = 30;
    
    [Header("Jump")]
    [SerializeField] float jumpForce = 4;
    [SerializeField] Transform groundCheckLeft;
    [SerializeField] Transform groundCheckRight;
    [SerializeField] float checkDistance;
    [SerializeField] LayerMask groundLayer;

    // Interal variables
    InputSystem_Actions actions;
    Rigidbody2D rb;
    Animator animator;
    float move;
    bool isGrounded;

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
        actions.Player.Jump.performed -= PerformJump;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }
    void Update()
    {
        // Change Direction of View
        if (move < 0){
            // Moving to the left
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }
        else if (move > 0){
            // Moving to the right
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }
        // Check for ground
        bool checkGroundOnLeft = Physics2D.Raycast(groundCheckLeft.position, Vector2.down, checkDistance, groundLayer);
        bool checkGroundOnRight = Physics2D.Raycast(groundCheckRight.position, Vector2.down, checkDistance, groundLayer);
        // If any of the checks gets true, then the player is touching the ground:
        isGrounded = checkGroundOnLeft || checkGroundOnRight;
        // Walk Animation
        if (move != 0)
        { animator.SetBool("isWalking", true); }
        else { animator.SetBool("isWalking", false); }
        // Jump Animation
        if (isGrounded)
        { animator.SetBool("isFalling", false); }
        else { animator.SetBool("isFalling", true); }

    }

    void LateUpdate()
    {
        rb.velocity = new Vector2(move * moveSpeed * Time.deltaTime, rb.velocity.y);
    }
    // -------------------------- MOVEMENT METHODS
    void PerformMovement(InputAction.CallbackContext ctx)
    {
        move = ctx.ReadValue<Vector2>().x * 25;
    }
    void PerformJump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                animator.SetTrigger("jump");
            }
        }
    }
    // -------------------------- EDITOR HELPERS
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(groundCheckLeft.position, new Vector2(groundCheckLeft.position.x, groundCheckLeft.position.y - checkDistance));
        Gizmos.DrawLine(groundCheckRight.position, new Vector2(groundCheckRight.position.x, groundCheckRight.position.y - checkDistance));
    }
}
