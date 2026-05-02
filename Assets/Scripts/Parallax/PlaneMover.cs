using UnityEngine;

public class PlaneMover : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float distance = 50.0f;
    private float initialX;

    void Start()
    {
        initialX = transform.position.x;
    }

    void Update()
    {
        if (!player) return;
        
        float targetX = Mathf.Lerp(transform.position.x, player.transform.position.x, 1) + distance;
        
        Vector3 newPos = transform.position;
        newPos.x = Mathf.MoveTowards(newPos.x, targetX, 100.0f * Time.deltaTime);
        transform.position = newPos;
    }
}