using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class EndScreen : MonoBehaviour
    {
        private MenuManager _menuManager;
        private UIDocument _doc;

        private Label _playerScore;
        private TextField _playerName;
        private Button _submitButton;
        private Button _restartButton;

        private Label _leaderboardNames;
        private Label _leaderboardScores;

        private void Awake()
        {
            _doc = GetComponent<UIDocument>();
            _menuManager = GetComponentInParent<MenuManager>();
            
            _playerScore = _doc.rootVisualElement.Q("LBL_playerScore") as Label;
            _playerName = _doc.rootVisualElement.Q("TF_playerName") as TextField;
            _submitButton = _doc.rootVisualElement.Q("SubmitButton") as Button;
            _restartButton = _doc.rootVisualElement.Q("RestartButton") as Button;
            
            _leaderboardNames = _doc.rootVisualElement.Q("LBL_Names") as Label;
            _leaderboardScores = _doc.rootVisualElement.Q("LBL_Scores") as Label;
        }

        private void OnEnable()
        {
            _playerName.RegisterValueChangedCallback(OnPlayerNameChanged);
            _submitButton.RegisterCallback<ClickEvent>(OnSubmitClicked);
            _restartButton.RegisterCallback<ClickEvent>(OnRestartClicked);

            LoadScoreboard();
        }

        private void LoadScoreboard()
        {
            LoadTopPlayerNames();
            LoadTopPlayerScores();
        }

        private void LoadTopPlayerScores()
        {
            
        }

        private void LoadTopPlayerNames()
        {
            
        }

        private void OnRestartClicked(ClickEvent evt)
        {
            
        }

        private void OnSubmitClicked(ClickEvent evt)
        {
            var playerName = _playerName.value;
            int playerScore;
            var success = int.TryParse(_playerScore.text.Substring(12), out playerScore);
            
        }

        private void OnDisable()
        {
            _playerName.UnregisterValueChangedCallback(OnPlayerNameChanged);
            _submitButton.UnregisterCallback<ClickEvent>(OnSubmitClicked);
            _restartButton.UnregisterCallback<ClickEvent>(OnRestartClicked);
        }

        private void OnPlayerNameChanged(ChangeEvent<string> evt)
        {
            _playerName.value = evt.newValue.ToUpper();
        }
        
        
    }
}
