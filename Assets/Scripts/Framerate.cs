using UnityEngine;

public class Framerate : MonoBehaviour
{
    [SerializeField] private int frameCap = 30;

    void Start()
    {
        if(frameCap >= 30)
            Application.targetFrameRate = frameCap;
    }
}
