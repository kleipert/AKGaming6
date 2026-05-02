using Level;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace UI
{
    public class PauseScreen : MonoBehaviour
    {
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
        }

        private void OnDisable()
        {
            _settingsButton.UnregisterCallback<ClickEvent>(OnSettingsButtonClicked);
            _restartButton.UnregisterCallback<ClickEvent>(OnRestartButtonClicked);
            _continueButton.UnregisterCallback<ClickEvent>(OnContinueButtonClicked);
        }
        
        private void OnContinueButtonClicked(ClickEvent evt)
        {
            _menuManager.OpenGameScreen();
            Time.timeScale = 1;
            SoundManager.Instance.SwitchClip();
        }

        private void OnRestartButtonClicked(ClickEvent evt)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Time.timeScale = 1;
        }

        private void OnSettingsButtonClicked(ClickEvent evt)
        {
            
        }
    }
}
