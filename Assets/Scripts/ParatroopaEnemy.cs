using UnityEngine;

public class ParatroopaEnemy : MonoBehaviour
{
    [Header("Variables Movimiento")]
    public float moveSpeed = 2f;
    public float jumpForce = 5f;
    public Transform wallCheck;
    public Transform groundCheck;
    public LayerMask wallLayer;
    public LayerMask groundLayer;

    [Header("Sprite")]
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Header("Prefab")]
    [SerializeField] private GameObject turtlePrefab;

    private Rigidbody2D rb;
    private int direction = -1;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);

        bool hitWall = Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer) || Physics2D.OverlapCircle(wallCheck.position, 0.2f, groundLayer);

        if (hitWall) Flip();

        bool grounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, wallLayer) || Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        if (grounded) rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    void Flip()
    {
        direction *= -1;
        if (direction > 0) spriteRenderer.flipX = true;
        else spriteRenderer.flipX = false;
        wallCheck.localPosition = new Vector2(-wallCheck.localPosition.x, wallCheck.localPosition.y);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.gameObject.CompareTag("Player")) return;

        if (col.transform.position.y > transform.position.y + 0.3f)
        {
            Instantiate(turtlePrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else
        {
            col.gameObject.GetComponent<PlayerMovement>().Die();
        }
    }
}