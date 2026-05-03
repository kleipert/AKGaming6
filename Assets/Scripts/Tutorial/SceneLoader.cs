using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActions;
    [SerializeField] private float waitDuration;
    
    private InputAction m_switchAction;

    private void Awake()
    {
        m_switchAction = InputSystem.actions.FindAction("Dive");
    }
    
    void Start()
    {
        inputActions.FindActionMap("UI").Disable();
        inputActions.FindActionMap("Player").Enable();
        StartCoroutine(LoadScene());
    }
    
    void Update()
    {
        if (m_switchAction.IsPressed())
        {
            SceneManager.LoadScene("COP");
        }
    }

    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(waitDuration);
        SceneManager.LoadScene("COP");
    }
}
