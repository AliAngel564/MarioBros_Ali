using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;



public class PlayerMovement : MonoBehaviour
{
    [Header("Variables Movimiento")]
    public float moveSpeed = 5f;
    public float jumpForce = 2f;
    public float bounceForce = 7f;
    public Transform groundCheck;
    public LayerMask groundLayer;

    [Header("Sprite")]
    [SerializeField] private SpriteRenderer spriteRenderer;

    private Rigidbody2D rb;
    private InputSystem_Actions controls;
    private float moveInput;
    private bool isGrounded;
    private Animator animator;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        controls = new InputSystem_Actions();
        animator = GetComponentInChildren<Animator>();
    }

    void OnEnable()
    {
        controls.Player.Enable();
        controls.Player.Jump.performed += OnJump;
    }

    void OnDisable()
    {
        controls.Player.Jump.performed -= OnJump;
        controls.Player.Disable();
    }

    void Update()
    {
        moveInput = controls.Player.Move.ReadValue<Vector2>().x;
        animator.SetBool("isMoving", moveInput != 0);
        if (moveInput < 0) spriteRenderer.flipX = true;
        else if(moveInput > 0) spriteRenderer.flipX = false;
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }

    void OnJump(InputAction.CallbackContext ctx)
    {
        if (isGrounded) 
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    public void Die()
    {
        FindAnyObjectByType<GameOverMenu>().ShowGameOver();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Enemy") && transform.position.y > col.transform.position.y + 0.3f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, bounceForce);
        }
    }
}