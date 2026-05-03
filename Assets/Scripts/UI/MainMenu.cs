using Level;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal;
using UnityEngine.UIElements;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        private static readonly int GameStart = Animator.StringToHash("GameStart");
        [SerializeField] private Animator animator;
        [SerializeField] private LightManager lightManager;
        [SerializeField] private Light2D light2D;
        
        [SerializeField] private AudioClip hoverClip;
        [SerializeField] private AudioClip clickClip;
        [SerializeField] private AudioClip errorClip;
        
        private MenuManager _menuManager;
        private UIDocument _doc;
        private VisualElement _mainContainer;

        private Button _startButton;
        private Button _settingsButton;
        private Button _exitButton;
        private Button _dayButton;
        private Button _nightButton;
        private Button _leaderboardButton;

        private bool _isDayActive = false;

        [SerializeField] private Sprite _daySprite;
        [SerializeField] private Sprite _daySpriteToggled;
        [SerializeField] private Sprite _nightSprite;
        [SerializeField] private Sprite _nightSpriteToggled;


        private void Awake()
        {
            _doc = GetComponent<UIDocument>();
            _menuManager = GetComponentInParent<MenuManager>();

            _mainContainer = _doc.rootVisualElement.Q("MainContainer");
            
            _startButton = _doc.rootVisualElement.Q("StartButton") as Button;
            _settingsButton = _doc.rootVisualElement.Q("SettingsButton") as Button;
            _exitButton = _doc.rootVisualElement.Q("ExitButton") as Button;
            _dayButton = _doc.rootVisualElement.Q("DayButton") as Button;
            _nightButton = _doc.rootVisualElement.Q("NightButton") as Button;
            _leaderboardButton = _doc.rootVisualElement.Q("LeaderboardButton") as Button;
        }

        private void OnEnable()
        {
            _startButton.RegisterCallback<ClickEvent>(OnStartButtonClicked);
            _settingsButton.RegisterCallback<ClickEvent>(OnSettingsButtonClicked);
            _exitButton.RegisterCallback<ClickEvent>(OnExitButtonClicked);
            _dayButton.RegisterCallback<ClickEvent>(OnDayButtonClicked);
            _nightButton.RegisterCallback<ClickEvent>(OnNightButtonClicked);
            _leaderboardButton.RegisterCallback<ClickEvent>(OnLeaderboardButtonClicked);
            
            _startButton.RegisterCallback<MouseEnterEvent>(OnHover);
            _settingsButton.RegisterCallback<MouseEnterEvent>(OnHover);
            _exitButton.RegisterCallback<MouseEnterEvent>(OnHover);
            _leaderboardButton.RegisterCallback<MouseEnterEvent>(OnHover);
        }



        private void OnDisable()
        {
            _startButton.UnregisterCallback<ClickEvent>(OnStartButtonClicked);
            _settingsButton.UnregisterCallback<ClickEvent>(OnSettingsButtonClicked);
            _exitButton.UnregisterCallback<ClickEvent>(OnExitButtonClicked);
            _dayButton.UnregisterCallback<ClickEvent>(OnDayButtonClicked);
            _nightButton.UnregisterCallback<ClickEvent>(OnNightButtonClicked);
            _leaderboardButton.UnregisterCallback<ClickEvent>(OnLeaderboardButtonClicked);
            
            _startButton.UnregisterCallback<MouseEnterEvent>(OnHover);
            _settingsButton.UnregisterCallback<MouseEnterEvent>(OnHover);
            _exitButton.UnregisterCallback<MouseEnterEvent>(OnHover);
            _leaderboardButton.UnregisterCallback<MouseEnterEvent>(OnHover);
        }
        
        private void OnLeaderboardButtonClicked(ClickEvent evt)
        {
            _menuManager.OpenLeaderboardScreen();
        }

        private void OnNightButtonClicked(ClickEvent evt)
        {
            if (!_isDayActive)
            {
                SoundManager.Instance.PlaySound(errorClip, transform, 1f);
                return;
            }
            
            SoundManager.Instance.PlaySound(clickClip, transform, 1f);
            
            _nightButton.style.backgroundImage = new StyleBackground(_nightSpriteToggled);
            _nightButton.style.scale = new StyleScale(new Vector2(1.2f, 1.2f));
            
            _dayButton.style.backgroundImage = new StyleBackground(_daySprite);
            _dayButton.style.scale = new StyleScale(new Vector2(1f, 1f));
            _isDayActive = false;
            
            lightManager.ActivateLight();
            light2D.intensity = 0.3f;
        }

        private void OnDayButtonClicked(ClickEvent evt)
        {
            if (_isDayActive)
            {
                SoundManager.Instance.PlaySound(errorClip, transform, 1f);
                return;
            }
            
            SoundManager.Instance.PlaySound(clickClip, transform, 1f);
            
            _dayButton.style.backgroundImage = new StyleBackground(_daySpriteToggled);
            _dayButton.style.scale = new StyleScale(new Vector2(1.2f, 1.2f));
            
            _nightButton.style.backgroundImage = new StyleBackground(_nightSprite);
            _nightButton.style.scale = new StyleScale(new Vector2(1f, 1f));
            _isDayActive = true;
            
            lightManager.DeactivateLight();
            light2D.intensity = 0.8f;
        }

        public void OpenMainMenu()
        {
            _mainContainer.RemoveFromClassList("hide");
            _startButton.AddToClassList("button");
        }
        
        public void HideMainMenu()
        {
            _mainContainer.AddToClassList("hide");
            _startButton.RemoveFromClassList("button");
        }

        private void OnExitButtonClicked(ClickEvent evt)
        {
            SoundManager.Instance.PlaySound(clickClip, transform, 1f);
            Application.Quit();
        }

        private void OnSettingsButtonClicked(ClickEvent evt)
        {
            SoundManager.Instance.PlaySound(clickClip, transform, 1f);
            _menuManager.OpenSettingsMenu();
        }

        private void OnStartButtonClicked(ClickEvent evt)
        {
            HideMainMenu();
            _menuManager.OpenGameScreen();
            animator.SetTrigger(GameStart);
            SoundManager.Instance.PlaySound(clickClip, transform, 1f);
            SoundManager.Instance.PlayCrowd();
        }

        private void OnHover(MouseEnterEvent evt)
        {
            SoundManager.Instance.PlaySound(hoverClip, transform, 1f);
        }
    }
}
