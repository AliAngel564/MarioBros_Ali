using UnityEngine;

public class TurtleEnemy : MonoBehaviour
{
    [Header("Variables Movimiento")]
    public float moveSpeed = 2f;
    public float shellSpeed = 8f;
    public Transform wallCheck;
    public LayerMask wallLayer;
    public LayerMask Enemies;

    [Header("Sprites")]
    [SerializeField] private Sprite shellSprite;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private Rigidbody2D rb;
    private int direction = -1;
    private bool isShell = false;
    private float currentSpeed;
    private float wallCheckX;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        currentSpeed = moveSpeed;
        wallCheckX = Mathf.Abs(wallCheck.localPosition.x);
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(direction * currentSpeed, rb.linearVelocity.y);

        if (currentSpeed > 0 && Physics2D.OverlapCircle(wallCheck.position, 0.1f, wallLayer)) SetDirection(-direction);
    }

    void SetDirection(int dir)
    {
        direction = dir;
        wallCheck.localPosition = new Vector2(wallCheckX * direction, wallCheck.localPosition.y);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            bool fromAbove = col.transform.position.y > transform.position.y + 0.3f;

            if (!isShell)
            {
                if (fromAbove)
                {
                    isShell = true;
                    currentSpeed = 0;
                    spriteRenderer.sprite = shellSprite;
                }
                else
                {
                    col.gameObject.GetComponent<PlayerMovement>().Die();
                }
            }
            else
            {
                if (currentSpeed == 0)
                {
                    if (col.transform.position.x < transform.position.x)
                        SetDirection(1);
                    else
                        SetDirection(-1);
                    currentSpeed = shellSpeed;
                }
                else if (fromAbove)
                {
                    currentSpeed = 0;
                }
                else
                {
                    col.gameObject.GetComponent<PlayerMovement>().Die();
                }
            }
        }
        else if (col.gameObject.CompareTag("Enemy") && isShell && currentSpeed > 0)
        {
            Destroy(col.gameObject);
        }
    }
}