using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class SliderController : MonoBehaviour
    {
        [SerializeField] private float diveForce = 20f;
        [SerializeField] private float startForce = 40f;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private InputActionAsset inputActions;
    
        private bool grounded;
        private InputAction m_diveAction;

        private void OnEnable()
        {
            /*
            inputActions.FindActionMap("Player").Enable();
            rb.AddForce(Vector2.down * startForce, ForceMode2D.Force);*/    
        }

        public void BoostThisMFer()
        {
            inputActions.FindActionMap("Player").Enable();
            rb.AddForce(Vector2.down * startForce, ForceMode2D.Force);
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
}
