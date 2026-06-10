using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float lookAhead = 2f;
    public float smooth = 5f;

    private Rigidbody2D targetRb;

    void Start()
    {
        targetRb = target.GetComponent<Rigidbody2D>();
    }

    void LateUpdate()
    {
        float offsetX = 0f;
        if (targetRb.linearVelocity.x > 0.1f) offsetX = lookAhead;
        else if (targetRb.linearVelocity.x < -0.1f) offsetX = -lookAhead;

        Vector3 targetPos = new Vector3(target.position.x + offsetX, target.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPos, smooth * Time.deltaTime);
    }
}