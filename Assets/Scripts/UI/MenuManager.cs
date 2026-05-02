using UnityEngine;
using UnityEngine.InputSystem;

namespace UI
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] private InputActionAsset _inputActions;
        private MainMenu _mainMenu;
        private SettingsMenu _settingsMenu;
        private EndScreen _endScreen;
        private GameScreen _gameScreen;
        private PauseScreen _pauseScreen;

        private void Awake()
        {
            _mainMenu = GetComponentInChildren<MainMenu>();
            _settingsMenu = GetComponentInChildren<SettingsMenu>();
            _endScreen = GetComponentInChildren<EndScreen>();
            _gameScreen = GetComponentInChildren<GameScreen>();
            _pauseScreen = GetComponentInChildren<PauseScreen>();
        }

        private void ActivateUIMap()
        {
            _inputActions.FindActionMap("Player").Disable();
            _inputActions.FindActionMap("UI").Enable();
        }

        private void ActivatePlayerMap()
        {
            _inputActions.FindActionMap("UI").Disable();
            _inputActions.FindActionMap("Player").Enable();
        }

        public void OpenStartMenu()
        {
            ActivateUIMap();
            _pauseScreen.HidePauseScreen();
            _gameScreen.HideGameScreen();
            _endScreen.HideEndScreen();
            _settingsMenu.HideSettingsMenu();
            _mainMenu.OpenMainMenu();
        }

        public void OpenSettingsMenu()
        {
            ActivateUIMap();
            _pauseScreen.HidePauseScreen();
            _endScreen.HideEndScreen();
            _gameScreen.HideGameScreen();
            _mainMenu.HideMainMenu();
            _settingsMenu.OpenSettingsMenu();
        }

        public void OpenGameScreen()
        {
            ActivatePlayerMap();
            _pauseScreen.HidePauseScreen();
            _settingsMenu.HideSettingsMenu();
            _mainMenu.HideMainMenu();
            _endScreen.HideEndScreen();
            _gameScreen.ShowGameScreen();
        }

        public void OpenEndScreen()
        {
            ActivateUIMap();
            _pauseScreen.HidePauseScreen();
            _settingsMenu.HideSettingsMenu();
            _mainMenu.HideMainMenu();
            _gameScreen.HideGameScreen();
            _endScreen.ShowEndScreen();
        }

        public void OpenPauseScreen()
        {
            ActivateUIMap();
            _endScreen.HideEndScreen();
            _settingsMenu.HideSettingsMenu();
            _mainMenu.HideMainMenu();
            _gameScreen.HideGameScreen();
            _pauseScreen.ShowPauseScreen();
        }
    }
}
