using System;
using System.Collections;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Level
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance { get; private set; }
    
        [SerializeField] private AudioClip clipIntro;
        [SerializeField] private AudioClip clipGame;
    
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private bool playIntroOnStart = true;

        [SerializeField] private AudioSource audioCrowd;

        [SerializeField] private AudioSource audioObject;
        
        [SerializeField] private InputActionAsset inputActions;

        public event Action OnGameClipFinished;
        
        private MenuManager _menuManager;
    
        private float gameClipTime = 0f;  
        public bool isPlayingGameClip = false;
        private bool gameClipFinished = false;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            audioSource.loop = true;
        }

        private void Start()
        {
            _menuManager = GameObject.Find("MenuManager").GetComponent<MenuManager>();

            if (playIntroOnStart && clipIntro != null)
                PlayIntro();
            SwitchClip();
            SwitchClip();
            gameClipTime = 0f;
        }

        private void Update()
        {
            if (isPlayingGameClip && !gameClipFinished)
            {
                gameClipTime += Time.deltaTime;
                
                if (audioSource.time >= clipGame.length - 0.5f)
                {
                    inputActions.FindActionMap("Player").Disable();
                    inputActions.FindActionMap("UI").Enable();
                    StartCoroutine(ShowEndScreen());
                }

                if (gameClipTime >= clipGame.length)
                {
                    gameClipFinished = true;
                    isPlayingGameClip = false;
                    audioSource.Stop();
                    OnGameClipFinished?.Invoke();
                    audioCrowd.Play();
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
            if (!clipIntro) return;

            audioSource.clip = clipIntro;
            audioSource.time = 0f;
            audioSource.Play();
            isPlayingGameClip = false;
        }

        public void PlayGame()
        {
            if (!clipGame) return;

            audioSource.clip = clipGame;
            audioSource.time = Mathf.Clamp(gameClipTime, 0f, clipGame.length - 0.05f);
            audioSource.Play();
            isPlayingGameClip = true;
        }
    
        IEnumerator ShowEndScreen()
        {
            yield return new WaitForSeconds(5);
            _menuManager.OpenEndScreen();
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
}