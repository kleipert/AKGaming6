using UnityEngine;

public class RotateSprite : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform visual;
    [SerializeField] private Rigidbody2D rb;

    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 2f, groundLayer);
        if (hit)
        {
            float angle = Mathf.Atan2(hit.normal.x, hit.normal.y) * -Mathf.Rad2Deg;
            visual.rotation = Quaternion.Slerp(visual.rotation,
                Quaternion.Euler(0, 0, angle),
                Time.deltaTime * 10f);
        }
        else
        {
            float angle = Mathf.Atan2(rb.linearVelocity.y, Mathf.Abs(rb.linearVelocity.x)) * Mathf.Rad2Deg;
            angle = Mathf.Clamp(angle, -60f, 60f);
            visual.rotation = Quaternion.Slerp(visual.rotation,
                Quaternion.Euler(0, 0, angle),
                Time.deltaTime * 5f);
        }
    }
}