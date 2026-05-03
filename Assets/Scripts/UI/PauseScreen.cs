using Level;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace UI
{
    public class PauseScreen : MonoBehaviour
    {
        [SerializeField] private AudioClip hoverClip;
        [SerializeField] private AudioClip clickClip;
        
        private MenuManager _menuManager;
        private UIDocument _doc;
        private VisualElement _mainContainer;
        
        private Button _continueButton;
        private Button _restartButton;
        private Button _settingsButton;

        private void Awake()
        {
            _doc = GetComponent<UIDocument>();
            _mainContainer = _doc.rootVisualElement.Q("MainContainer");
            _menuManager = GetComponentInParent<MenuManager>();
            
            _settingsButton = _doc.rootVisualElement.Q("SettingsButton") as Button;
            _restartButton = _doc.rootVisualElement.Q("RestartButton") as Button;
            _continueButton = _doc.rootVisualElement.Q("ContinueButton") as Button;
        }

        public void ShowPauseScreen()
        {
            _continueButton.AddToClassList("button");
            _mainContainer.RemoveFromClassList("hide");
        }

        public void HidePauseScreen()
        {
            _mainContainer.AddToClassList("hide");
        }

        private void OnEnable()
        {
            _settingsButton.RegisterCallback<ClickEvent>(OnSettingsButtonClicked);
            _restartButton.RegisterCallback<ClickEvent>(OnRestartButtonClicked);
            _continueButton.RegisterCallback<ClickEvent>(OnContinueButtonClicked);
            
            _settingsButton.RegisterCallback<MouseEnterEvent>(OnHover);
            _restartButton.RegisterCallback<MouseEnterEvent>(OnHover);
            _continueButton.RegisterCallback<MouseEnterEvent>(OnHover);
        }

        private void OnDisable()
        {
            _settingsButton.UnregisterCallback<ClickEvent>(OnSettingsButtonClicked);
            _restartButton.UnregisterCallback<ClickEvent>(OnRestartButtonClicked);
            _continueButton.UnregisterCallback<ClickEvent>(OnContinueButtonClicked);
            
            _settingsButton.UnregisterCallback<MouseEnterEvent>(OnHover);
            _restartButton.UnregisterCallback<MouseEnterEvent>(OnHover);
            _continueButton.UnregisterCallback<MouseEnterEvent>(OnHover);
        }
        
        private void OnContinueButtonClicked(ClickEvent evt)
        {
            _continueButton.RemoveFromClassList("button");
            _menuManager.OpenGameScreen();
            Time.timeScale = 1;
            SoundManager.Instance.PlaySound(clickClip, transform, 1f);
            SoundManager.Instance.SwitchClip();
        }

        private void OnRestartButtonClicked(ClickEvent evt)
        {
            SoundManager.Instance.PlaySound(clickClip, transform, 1f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Time.timeScale = 1;
        }

        private void OnSettingsButtonClicked(ClickEvent evt)
        {
            SoundManager.Instance.PlaySound(clickClip, transform, 1f);
            _menuManager.OpenSettingsMenu();
        }
        
        private void OnHover(MouseEnterEvent evt)
        {
            SoundManager.Instance.PlaySound(hoverClip, transform, 1f);
        }
    }
}
