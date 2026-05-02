using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class SettingsMenu : MonoBehaviour
    {
        public float MusicSliderValue => _sliderValue;

        private MenuManager _menuManager;
        private UIDocument _doc;
        private VisualElement _mainContainer;

        private Slider _musicSlider;
        private Button _backButton;

        private int _sliderValue = 100;

        private void Awake()
        {
            _doc = GetComponent<UIDocument>();
            
            _mainContainer = _doc.rootVisualElement.Q("MainContainer");
            _musicSlider = _doc.rootVisualElement.Q("SliderMusic") as Slider;
            _backButton = _doc.rootVisualElement.Q("ExitButton") as Button;

            _menuManager = GetComponentInParent<MenuManager>();
        }

        private void OnEnable()
        {
            _musicSlider.RegisterValueChangedCallback(OnSliderValueChanged);
            _backButton.RegisterCallback<ClickEvent>(OnBackButtonClicked);
        }

        private void OnDisable()
        {
            _musicSlider.UnregisterValueChangedCallback(OnSliderValueChanged);
            _backButton.UnregisterCallback<ClickEvent>(OnBackButtonClicked);
        }

        private void OnSliderValueChanged(ChangeEvent<float> evt)
        {
            _sliderValue = (int) evt.newValue;
        }

        private void OnBackButtonClicked(ClickEvent evt)
        {
            _menuManager.OpenStartMenu();
        }

        public void OpenSettingsMenu()
        {
            _mainContainer.RemoveFromClassList("hide");
            _backButton.AddToClassList("button");
        }
        
        public void HideSettingsMenu()
        {
            _mainContainer.AddToClassList("hide");
            _backButton.RemoveFromClassList("button");
        }
    }
}
