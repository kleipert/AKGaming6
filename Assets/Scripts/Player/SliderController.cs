using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class SliderController : MonoBehaviour
    {
        [SerializeField] private float diveForce = 20f;
        [SerializeField] private float startForce = 40f;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private InputActionAsset inputActions;
        
        [SerializeField] private bool useLatencyCompensation = true;

        private bool diveHeld;
        private double inputTimestamp;
        private InputAction m_diveAction;

        private void Awake()
        {
            Time.fixedDeltaTime = 1f / 100f;

            inputActions.FindActionMap("Player").Disable();
            inputActions.FindActionMap("UI").Enable();
            m_diveAction = InputSystem.actions.FindAction("Dive");
        }

        public void BoostThisMFer()
        {
            inputActions.FindActionMap("Player").Enable();
            rb.AddForce(Vector2.down * startForce, ForceMode2D.Impulse);
        }

        private void OnDisable()
        {
            inputActions.FindActionMap("Player").Disable();
        }

        private void Update()
        {
            diveHeld = m_diveAction.IsPressed();
            
            if (m_diveAction.WasPerformedThisFrame())
            {
                inputTimestamp = m_diveAction.activeControl?.device.lastUpdateTime ?? Time.unscaledTimeAsDouble;
            }
        }

        private void FixedUpdate()
        {
            if (!diveHeld) return;

            if (useLatencyCompensation)
            {
                float timeSinceInput = (float)(Time.fixedUnscaledTimeAsDouble - inputTimestamp);

                if (timeSinceInput < Time.fixedDeltaTime * 2f)
                {
                    float compensationFactor = 1f + (timeSinceInput / Time.fixedDeltaTime);
                    rb.AddForce(Vector2.down * (diveForce * compensationFactor), ForceMode2D.Force);
                }
                else
                {
                    rb.AddForce(Vector2.down * diveForce, ForceMode2D.Force);
                }
            }
            else
            {
                rb.AddForce(Vector2.down * diveForce, ForceMode2D.Force);
            }
        }
    }
}