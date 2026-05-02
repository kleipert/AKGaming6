using UnityEditor;
using UnityEngine;
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
        
        private MenuManager _menuManager;
        private UIDocument _doc;
        private VisualElement _mainContainer;

        private Button _startButton;
        private Button _settingsButton;
        private Button _exitButton;
        private Button _dayButton;
        private Button _nightButton;

        private bool _isDayActive = false;

        private Sprite _daySprite;
        private Sprite _daySpriteToggled;
        private Sprite _nightSprite;
        private Sprite _nightSpriteToggled;


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

            _daySprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Art/UI/MainMenu/Button_Day.png");
            _daySpriteToggled = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Art/UI/MainMenu/Button_Day_Toggled.png");
            _nightSprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Art/UI/MainMenu/Button_Night.png");
            _nightSpriteToggled = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Art/UI/MainMenu/Button_Night_Toggled.png");
        }

        private void OnEnable()
        {
            _startButton.RegisterCallback<ClickEvent>(OnStartButtonClicked);
            _settingsButton.RegisterCallback<ClickEvent>(OnSettingsButtonClicked);
            _exitButton.RegisterCallback<ClickEvent>(OnExitButtonClicked);
            _dayButton.RegisterCallback<ClickEvent>(OnDayButtonClicked);
            _nightButton.RegisterCallback<ClickEvent>(OnNightButtonClicked);
        }

        private void OnDisable()
        {
            _startButton.UnregisterCallback<ClickEvent>(OnStartButtonClicked);
            _settingsButton.UnregisterCallback<ClickEvent>(OnSettingsButtonClicked);
            _exitButton.UnregisterCallback<ClickEvent>(OnExitButtonClicked);
            _dayButton.UnregisterCallback<ClickEvent>(OnDayButtonClicked);
            _nightButton.UnregisterCallback<ClickEvent>(OnNightButtonClicked);
        }

        private void OnNightButtonClicked(ClickEvent evt)
        {
            if (!_isDayActive) return;
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
            if (_isDayActive) return;
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
            Application.Quit();
        }

        private void OnSettingsButtonClicked(ClickEvent evt)
        {
            _menuManager.OpenSettingsMenu();
        }

        private void OnStartButtonClicked(ClickEvent evt)
        {
            HideMainMenu();
            animator.SetTrigger(GameStart);
            SoundManager.Instance.PlayCrowd();
        }
    }
}
