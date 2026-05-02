using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace UI
{
    public class GameScreen : MonoBehaviour
    {
        [SerializeField] private InputActionAsset _inputActions;
        private MenuManager _menuManager;
        private UIDocument _doc;
        private VisualElement _mainContainer;
        private InputAction pauseAction;

        private Label _scoreValueLabel;

        private void Awake()
        {
            _doc = GetComponent<UIDocument>();
            _mainContainer = _doc.rootVisualElement.Q("MainContainer");
            _menuManager = GetComponentInParent<MenuManager>();

            _scoreValueLabel = _doc.rootVisualElement.Q("LBL_currentScoreValue") as Label;
            pauseAction = InputSystem.actions.FindAction("Pause");
        }

        public void HideGameScreen()
        {
            _mainContainer.AddToClassList("hide");
        }

        public void ShowGameScreen()
        {
            _mainContainer.RemoveFromClassList("hide");
        }

        private void Update()
        {
            _scoreValueLabel.text = Score.Instance.GetScore().ToString();

            if (pauseAction.WasPressedThisFrame())
            {
                _menuManager.OpenPauseScreen();
            }
        }
    }
}
