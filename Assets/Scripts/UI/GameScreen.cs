using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class GameScreen : MonoBehaviour
    {
        private MenuManager _menuManager;
        private UIDocument _doc;
        private VisualElement _mainContainer;

        private Label _scoreValueLabel;

        private void Awake()
        {
            _doc = GetComponent<UIDocument>();
            _mainContainer = _doc.rootVisualElement.Q("MainContainer");
            _menuManager = GetComponentInParent<MenuManager>();

            _scoreValueLabel = _doc.rootVisualElement.Q("LBL_currentScoreValue") as Label;
        }

        public void HideGameScreen()
        {
            _mainContainer.AddToClassList("hide");
        }

        public void ShowGameScreen()
        {
            _mainContainer.RemoveFromClassList("hide");
        }
    }
}
