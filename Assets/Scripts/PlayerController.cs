using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    

    private bool Jumped = false;
    private bool isGrounded = false;
    
    private Vector2 moveDirection;

    [Header("Visual Components")] 
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Header("Movement Variables")] 
    [SerializeField]private float maxSpeed = 5f;
    [SerializeField]private float groundAcceleration;
    [SerializeField]private float groundDeceleration;
    [SerializeField]private float airAcceleration;
    [SerializeField]private float airDeceleration;
    [SerializeField] private float stopThreshold = 0.05f;

    [Header("Jump Variables")]
    [SerializeField]private float jumpSpeed;
    [SerializeField]private float jumpCutFactor;
    [SerializeField]private float minimumJump;

    [Header("Tuning")] 
    [SerializeField] private float CoyoteTime;
    [SerializeField] private float jumpBufferTime;
    
    [Header("Gravity")]
    [SerializeField]private float fallGravityMultiplier;
    [SerializeField]private float lowJumpGravityMultiplier;
    [SerializeField]private float apexGravityMultiplier;
    [SerializeField]private float maxFallSpeed;
    
    [Header("GroundCheck")]
    [SerializeField]private Transform groundCheck;
    [SerializeField]private float groundCheckRadius;
    
    [Header("Properties")]
    private Rigidbody2D rb;
    private InputSystem_Actions actions;
    private InputAction moveAction;
    private InputAction jumpAction;
    
    private void Awake()
    {
        actions = new InputSystem_Actions();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        //Prendemos controles
        actions.Enable();
        //Suscribimos acciones de Salto
        actions.Player.Jump.started += OnJumpStarted; 
        actions.Player.Jump.started += OnJumpCanceled; 
    }

    private void OnDisable()
    {
        //Desuscribimos acciones de salto
        actions.Player.Jump.started -= OnJumpStarted; 
        actions.Player.Jump.started -= OnJumpCanceled; 
        //Apagamos controles
        actions.Disable();
    }

   
    void Start()
    {
        moveAction = actions.FindAction("Move");
    }

    // Update is called once per frame

    void FixedUpdate()
    {
        ManageMovement();
    }
    
    void Update()
    {
        ManageVisualAspect();
        Debug.Log("Is Grounded: "+isGrounded);
    }

    void ManageMovement()
    {
       
        
    }

    void OnJumpStarted(InputAction.CallbackContext context)
    {
       
    }

    void OnJumpCanceled(InputAction.CallbackContext context)
    {
        
    }
    void ManageVisualAspect()
    {
        if (moveDirection.x < 0)
        {
            spriteRenderer.flipX = true;
        }else if (moveDirection.x > 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        isGrounded = true;
    }
    
}
