using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    
    [SerializeField] private AudioClip clipIntro;
    [SerializeField] private AudioClip clipGame;
    
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private bool playIntroOnStart = true;

    [SerializeField] private AudioSource audioCrowd;

    [SerializeField] private AudioSource audioObject;

    public event Action OnGameClipFinished;
    
    private float gameClipTime = 0f;  
    private bool isPlayingGameClip = false;
    private bool gameClipFinished = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource.loop = true;
    }

    private void Start()
    {
        if (playIntroOnStart && clipIntro != null)
            PlayIntro();
    }

    private void Update()
    {
        if (isPlayingGameClip && !gameClipFinished)
        {
            gameClipTime += Time.deltaTime;

            if (gameClipTime >= clipGame.length)
            {
                gameClipFinished = true;
                isPlayingGameClip = false;
                audioSource.Stop();
                OnGameClipFinished?.Invoke();
            }
        }
    }

    public void SwitchClip()
    {
        if (isPlayingGameClip)
        {
            PlayIntro();
        }
        else
        {
            PlayGame();
        }
    }

    public void PlayIntro()
    {
        if (clipIntro == null) return;

        audioSource.clip = clipIntro;
        audioSource.time = 0f;
        audioSource.Play();
        isPlayingGameClip = false;
    }

    public void PlayGame()
    {
        if (clipGame == null) return;

        audioSource.clip = clipGame;
        audioSource.time = Mathf.Clamp(gameClipTime, 0f, clipGame.length - 0.05f);
        audioSource.Play();
        isPlayingGameClip = true;
    }

    public void PlayCrowd()
    {
        audioCrowd.Play();
    }
    
    public void ResetManager()
    {
        gameClipTime = 0f;
        gameClipFinished = false;
        isPlayingGameClip = false;
    }
    
    public void PlaySound(AudioClip clip, Transform spawnTransform, float volume, float clipLength = 1.0f)
    {
        AudioSource soundObject = Instantiate(audioObject, spawnTransform.position, Quaternion.identity);
        
        soundObject.clip = clip;
        soundObject.volume = volume;
        soundObject.Play();

        clipLength *= soundObject.clip.length;
        
        Destroy(soundObject.gameObject, clipLength);
    }
}