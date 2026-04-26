using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    [SerializeField] private Rigidbody2D playerRb;
    [SerializeField] private float fallOffset = 3f;
    [SerializeField] private float fallSpeedThreshold = 5f;
    [SerializeField] private float smoothing = 3f;

    private float currentY;

    void LateUpdate()
    {
        float fallAmount = Mathf.Clamp01(-playerRb.linearVelocity.y / fallSpeedThreshold);
        
        float targetY = -fallAmount * fallOffset;
        
        currentY = Mathf.Lerp(currentY, targetY, Time.deltaTime * smoothing);

        transform.localPosition = new Vector3(0f, currentY, 0f);
    }
}