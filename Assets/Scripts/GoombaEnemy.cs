using UnityEngine;

public class GoombaEnemy : MonoBehaviour
{
    [Header("Variables Movimiento")]
    public float moveSpeed = 2f;
    public Transform wallCheck;
    public LayerMask wallLayer;
    public LayerMask Enemies;

    private Rigidbody2D rb;
    private int direction = -1;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);

        if (Physics2D.OverlapCircle(wallCheck.position, 0.1f, wallLayer)) Flip();
    }

    void Flip()
    {
        direction *= -1;
        wallCheck.localPosition = new Vector2(-wallCheck.localPosition.x, wallCheck.localPosition.y);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.gameObject.CompareTag("Player")) return;

        if (col.transform.position.y > transform.position.y + 0.5f)
        {
            Destroy(gameObject);
        }
        else
        {
            col.gameObject.GetComponent<PlayerMovement>().Die();
        }
    }
}