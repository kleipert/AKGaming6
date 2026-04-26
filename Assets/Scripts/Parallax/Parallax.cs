using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] float parallax;
    [SerializeField] SpriteRenderer spriteRenderer;

    private float length, start;
    
    void Start()
    {
        start = cam.transform.position.x;
        length = spriteRenderer.bounds.size.x;
    }
    
    void Update()
    {
        float distance = cam.transform.position.x * parallax;
        float temp = cam.transform.position.x * (1 - parallax);
        
        transform.position = new Vector3(start + distance, transform.position.y, transform.position.z);
        
        if(temp > start +  length) start += length;
        if(temp < start - length) start -= length;
    }
}