using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [Header("Movement Variables")]
    [SerializeField] float moveSpeed = 30;
    [SerializeField] float jumpForce = 4;

    // Interal variables
    InputSystem_Actions actions;
    Rigidbody2D rb;
    float move;

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
        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
    }
}
