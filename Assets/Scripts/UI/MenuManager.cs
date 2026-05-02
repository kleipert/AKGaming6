using UnityEngine;

namespace UI
{
    public class MenuManager : MonoBehaviour
    {
        private MainMenu _mainMenu;
        private SettingsMenu _settingsMenu;
        private EndScreen _endScreen;
        private GameScreen _gameScreen;

        private void Awake()
        {
            _mainMenu = GetComponentInChildren<MainMenu>();
            _settingsMenu = GetComponentInChildren<SettingsMenu>();
            _endScreen = GetComponentInChildren<EndScreen>();
            _gameScreen = GetComponentInChildren<GameScreen>();
        }

        public void OpenStartMenu()
        {
            _gameScreen.HideGameScreen();
            _endScreen.HideEndScreen();
            _settingsMenu.HideSettingsMenu();
            _mainMenu.OpenMainMenu();
        }

        public void OpenSettingsMenu()
        {
            _endScreen.HideEndScreen();
            _gameScreen.HideGameScreen();
            _mainMenu.HideMainMenu();
            _settingsMenu.OpenSettingsMenu();
        }

        public void OpenGameScreen()
        {
            _settingsMenu.HideSettingsMenu();
            _mainMenu.HideMainMenu();
            _endScreen.HideEndScreen();
            _gameScreen.ShowGameScreen();
        }

        public void OpenEndScreen()
        {
            _settingsMenu.HideSettingsMenu();
            _mainMenu.HideMainMenu();
            _gameScreen.HideGameScreen();
            _endScreen.ShowEndScreen();
        }
    }
}
