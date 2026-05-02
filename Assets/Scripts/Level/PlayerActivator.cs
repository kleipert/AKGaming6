using UnityEngine;

public class PlayerActivator : MonoBehaviour
{
    [SerializeField] GameObject player;
    
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
    }
}
