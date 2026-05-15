using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private InputSystem_Actions actions;
    private InputAction moveAction;
    private InputAction jumpAction;

    private bool isGrounded = false;
    
    private Rigidbody2D rb;
    private Vector2 moveDirection;

    [Header("Visual Components")] 
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Header("Movement Variables")] 
    [SerializeField] private float startingSpeed;
    [SerializeField]private float maxSpeed = 10f;
    [SerializeField]private float moveSpeed;
    [SerializeField] private float jumpStrength;
    private float pastSpeed;
    
    [Header("Debug I Guess")]
    [SerializeField]private ContactPoint2D[] contactPoints;

    private void Awake()
    {
        actions = new InputSystem_Actions();
        rb = GetComponent<Rigidbody2D>();
        actions.Player.Jump.started += OnJump; 
    }

    private void OnEnable()
    {
        actions.Enable();
    }

    private void OnDisable()
    {
        actions.Disable();
    }

   
    void Start()
    {
        moveAction = actions.FindAction("Move");
    }

    // Update is called once per frame

    void FixedUpdate()
    {
        pastSpeed = rb.linearVelocityX;
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

    void OnJump(InputAction.CallbackContext context)
    {
        if (isGrounded)
        {
            rb.AddForce(Vector2.up*jumpStrength, ForceMode2D.Impulse);
            isGrounded = false;
        }
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
        contactPoints = new ContactPoint2D[other.contactCount];
        other.GetContacts(contactPoints);
    }
    
}
