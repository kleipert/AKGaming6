using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

namespace UI
{
    public class SettingsMenu : MonoBehaviour
    {
        [SerializeField] private AudioMixer _masterMixer;

        private MenuManager _menuManager;
        private UIDocument _doc;
        private VisualElement _mainContainer;

        private Slider _musicSlider;
        private Slider _vfxSlider;
        private Button _backButton;

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
            _musicSlider.RegisterValueChangedCallback(OnMusicSliderValueChanged);
            _vfxSlider.RegisterValueChangedCallback(OnVfxSliderValueChanged);
            _backButton.RegisterCallback<ClickEvent>(OnBackButtonClicked);
        }

        private void OnDisable()
        {
            _musicSlider.UnregisterValueChangedCallback(OnMusicSliderValueChanged);
            _vfxSlider.UnregisterValueChangedCallback(OnVfxSliderValueChanged);
            _backButton.UnregisterCallback<ClickEvent>(OnBackButtonClicked);
        }
        
        private void OnVfxSliderValueChanged(ChangeEvent<float> evt)
        {
            _masterMixer.SetFloat("volVfx", evt.newValue);

        }

        private void OnMusicSliderValueChanged(ChangeEvent<float> evt)
        {
            _masterMixer.SetFloat("volMusic", evt.newValue);
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
