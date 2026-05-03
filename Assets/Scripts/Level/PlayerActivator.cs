using Player;
using Unity.Cinemachine;
using UnityEngine;

namespace Level
{
    public class PlayerActivator : MonoBehaviour
    {
        [SerializeField] GameObject player;
        [SerializeField] CinemachineCamera cinemachineCamera;
        [SerializeField] LightManager lightManager;
        [SerializeField] AudioListener audioListener;
        private SliderController _slider;
        private SpriteRenderer _playerSR;
        private Rigidbody2D _playerRB;
    
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _slider = player.GetComponent<SliderController>();
            _playerSR = GameObject.Find("PlayerSpriteRenderer").GetComponent<SpriteRenderer>();
            _playerRB = player.GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void ActivatePlayer()
        {
            audioListener.enabled = false;
            _playerRB.constraints = RigidbodyConstraints2D.None;
            _playerRB.freezeRotation = true;
            _playerSR.enabled = true;
            _slider.BoostThisMFer();
            //player.SetActive(true);
            cinemachineCamera.Priority = 0;
            lightManager.SetBpm(120f);
            lightManager.SetDuration(0.2f);
            SoundManager.Instance.SwitchClip();
        }
    }
}
