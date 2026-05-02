using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Score : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] AudioSource audioSource;
    
    public Leaderboard leaderboard;
    private int score;
    private IEnumerator _enumerator;
    
    void Start()
    {
        _enumerator = StopGame();
    }

    void Update()
    {
        score = Mathf.FloorToInt(player.transform.position.x - transform.position.x);
    }
    
    void LateUpdate()
    {
        if (audioSource.isPlaying)
            return;
        StartCoroutine(_enumerator);

    }

    IEnumerator StopGame()
    {
        yield return leaderboard.SubmitScoreRoutine(score);
    }
}
