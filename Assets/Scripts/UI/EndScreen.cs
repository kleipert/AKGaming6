using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using LootLocker.Requests;

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
        
        private readonly string _leaderboardKey = "global_highscore";

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

        private void Update()
        {
            _playerScore.text = Score.Instance.GetScore().ToString();
        }

        private void LoadScoreboard()
        {
            LoadTopPlayerNames();
            LoadTopPlayerScores();
        }

        private void LoadTopPlayerScores()
        {
            StartCoroutine(FetchHighscroe());
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
            LootLockerSDKManager.SetPlayerName(playerName, (nameResponse) =>
            {
                if (!nameResponse.success)
                {
                    Debug.Log("SetPlayerName fehlgeschlagen: " + nameResponse.errorData?.message);
                    return;
                }

                Debug.Log("Name gesetzt: " + playerName);
                
                string memberID = ""; 
                LootLockerSDKManager.SubmitScore(memberID, playerScore, _leaderboardKey, (scoreResponse) =>
                {
                    if (scoreResponse.success)
                    {
                        Debug.Log("Score submitted: " + playerScore);
                        StartCoroutine(FetchHighscroe());
                    }
                    else
                    {
                        Debug.Log("SubmitScore fehlgeschlagen: " + scoreResponse.errorData?.message);
                    }
                });
            });
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
        
        private IEnumerator FetchHighscroe()
        {
            bool done = false;
            LootLockerSDKManager.GetScoreList(_leaderboardKey, 10, 0, (response) =>
            {
                if (response.success)
                {
                    string tempPlayerNames = null;
                    string tempPlayerScores = null;

                    LootLockerLeaderboardMember[] members = response.items;

                    for (int i = 0; i < members.Length; i++)
                    {
                        tempPlayerNames += members[i].rank + ". ";
                        if (members[i].player.name != "")
                        {
                            tempPlayerNames += members[i].player.name;
                        }
                        else
                        {
                            tempPlayerNames += members[i].player.id;
                        }

                        tempPlayerScores += members[i].score + "\n";
                        tempPlayerNames += "\n";
                    }

                    done = true;
                    _leaderboardNames.text = tempPlayerNames;
                    _leaderboardScores.text = tempPlayerScores;
                }
                else
                {
                    done = true;
                }
            });
            yield return new WaitWhile(() => !done);
        }
    }
}
