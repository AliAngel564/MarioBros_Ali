using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private InputSystem_Actions actions;
    private InputAction moveAction;
    
    private Rigidbody2D rb;
    private Vector2 moveDirection;

    [Header("Visual Components")] 
    [SerializeField] private SpriteRenderer spriteRenderer;
    [Header("Movement Variables")]
    [SerializeField]private float moveSpeed;

    private void Awake()
    {
        actions = new InputSystem_Actions();
        rb = GetComponent<Rigidbody2D>();
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
    void Update()
    {
        ManageMovement();
        ManageVisualAspect();
    }

    void ManageMovement()
    {
        moveDirection = moveAction.ReadValue<Vector2>().normalized;
        rb.AddForce(moveDirection * moveSpeed,ForceMode2D.Force);
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
}
