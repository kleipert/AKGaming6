using UnityEngine;

namespace UI
{
    public class MenuManager : MonoBehaviour
    {
        private MainMenu _mainMenu;
        private SettingsMenu _settingsMenu;

        private void Awake()
        {
            _mainMenu = GetComponentInChildren<MainMenu>();
            _settingsMenu = GetComponentInChildren<SettingsMenu>();
        }

        public void OpenStartMenu()
        {
            _settingsMenu.HideSettingsMenu();
            _mainMenu.OpenMainMenu();
        }

        public void OpenSettingsMenu()
        {
            _mainMenu.HideMainMenu();
            _settingsMenu.OpenSettingsMenu();
        }
    }
}
