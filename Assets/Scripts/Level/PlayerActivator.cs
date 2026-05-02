using Unity.Cinemachine;
using UnityEngine;

public class PlayerActivator : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] CinemachineCamera cinemachineCamera;
    [SerializeField] LightManager lightManager;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ActivatePlayer()
    {
        player.SetActive(true);
        cinemachineCamera.Priority = 0;
        lightManager.SetBpm(120f);
        lightManager.SetDuration(0.2f);
    }
}
