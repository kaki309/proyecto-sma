using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [Header("Movement")]
    [SerializeField] float moveSpeed = 30;
    [Header("Jump")]
    [SerializeField] float jumpForce = 4;
    [SerializeField] Transform groundCheck;
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
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        // Change Direction of View
        if (move < 0)
        { transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z); }
        else if (move > 0)
        { transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z); }
        // Check for ground
        isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, checkDistance, groundLayer);
        // Walk Animation
        if (move != 0)
        { animator.SetBool("isWalking", true); }
        else { animator.SetBool("isWalking", false); }
        // Jump Animation
        if (isGrounded)
        { animator.SetBool("isJumping", false); }
        else { animator.SetBool("isJumping", true); }

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
            }
        }
    }
    // -------------------------- EDITOR HELPERS
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - checkDistance));
    }
}
