using UnityEngine;
using UnityEngine.InputSystem;

public class SliedeController : MonoBehaviour
{
    [SerializeField] private float diveForce = 20f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private InputActionAsset inputActions;
    
    private bool grounded;
    private InputAction m_diveAction;

    private void OnEnable()
    {
        inputActions.FindActionMap("Player").Enable();
    }

    private void OnDisable()
    {
        inputActions.FindActionMap("Player").Disable();
    }

    private void Awake()
    {
        m_diveAction = InputSystem.actions.FindAction("Dive");
    }

    private void FixedUpdate()
    {
        grounded = Physics2D.CircleCast(transform.position, 0.5f, Vector2.down, 0.1f, groundLayer);

        if (m_diveAction.IsPressed())
        {
            rb.AddForce(Vector2.down * diveForce, ForceMode2D.Force);
        }
    }
}
