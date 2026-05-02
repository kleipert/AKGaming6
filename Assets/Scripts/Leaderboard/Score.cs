using System.Collections;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Score : MonoBehaviour
{
    public static Score Instance { get; private set; }
    
    [SerializeField] GameObject player;
    [SerializeField] AudioSource audioSource;
    [SerializeField] private InputActionAsset inputActions;
    
    public Leaderboard leaderboard;
    private int score;
    //private IEnumerator _enumerator;

    private MenuManager _menuManager;
    

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        //_enumerator = StopGame();
        _menuManager = GameObject.Find("MenuManager").GetComponent<MenuManager>();
    }

    void Update()
    {
        score = Mathf.FloorToInt(player.transform.position.x - transform.position.x);
    }
    
    void LateUpdate()
    {
        if (audioSource.isPlaying)
            return;
        //StartCoroutine(_enumerator);
        _menuManager.OpenEndScreen();
        inputActions.FindActionMap("Player").Disable();
        inputActions.FindActionMap("UI").Enable();
    }

    /*IEnumerator StopGame()
    {
        yield return leaderboard.SubmitScoreRoutine(score);
    }*/

    public int GetScore()
    {
        return score;
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }
}