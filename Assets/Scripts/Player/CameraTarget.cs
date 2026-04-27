using Unity.Cinemachine;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    [Header("Fall")]
    [SerializeField] private Rigidbody2D playerRb;
    [SerializeField] private float fallOffset = 3f;
    [SerializeField] private float fallSpeedThreshold = 5f;
    [SerializeField] private float smoothing = 3f;

    [Header("Zoom")]
    [SerializeField] private CinemachineCamera cam;
    [SerializeField] private float minOrthographicSize = 10f;
    [SerializeField] private float maxOrthographicSize = 50f;
    [SerializeField] private float zoomStartHeight = 5f;   // ab dieser Höhe beginnt der Zoom
    [SerializeField] private float zoomMaxHeight = 30f;    // bei dieser Höhe ist max. Zoom erreicht
    [SerializeField] private float zoomSmoothing = 1f;     // klein = langsam

    private float currentY;
    private float currentSize;
    private float startY;

    void Start()
    {
        startY = playerRb.position.y;
        currentSize = minOrthographicSize;
        cam.Lens.OrthographicSize = currentSize;
    }

    void LateUpdate()
    {
        // Y-Offset beim Fallen (wie gehabt)
        float fallAmount = Mathf.Clamp01(-playerRb.linearVelocity.y / fallSpeedThreshold);
        float targetY = -fallAmount * fallOffset;
        currentY = Mathf.Lerp(currentY, targetY, Time.deltaTime * smoothing);
        transform.localPosition = new Vector3(0f, currentY, 0f);

        // Zoom basierend auf Höhe
        float height = playerRb.position.y - startY;
        float t = Mathf.InverseLerp(zoomStartHeight, zoomMaxHeight, height);
        float targetSize = Mathf.Lerp(minOrthographicSize, maxOrthographicSize, t);

        currentSize = Mathf.Lerp(currentSize, targetSize, Time.deltaTime * zoomSmoothing);
        cam.Lens.OrthographicSize = currentSize;
    }
}