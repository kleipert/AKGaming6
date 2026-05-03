using System;
using System.Collections;
using Level;
using UnityEngine;
using UnityEngine.UIElements;
using LootLocker.Requests;
using UnityEngine.SceneManagement;

namespace UI
{
    public class EndScreen : MonoBehaviour
    {
        [SerializeField] private AudioClip hoverClip;
        [SerializeField] private AudioClip clickClip;
        [SerializeField] private AudioClip errorClip;
        
        private UIDocument _doc;
        private VisualElement _mainContainer;

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
            _mainContainer = _doc.rootVisualElement.Q("MainContainer");
            
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
            
            _submitButton.RegisterCallback<MouseEnterEvent>(OnHover);
            _restartButton.RegisterCallback<MouseEnterEvent>(OnHover);
        }
        
        private void OnDisable()
        {
            _playerName.UnregisterValueChangedCallback(OnPlayerNameChanged);
            _submitButton.UnregisterCallback<ClickEvent>(OnSubmitClicked);
            _restartButton.UnregisterCallback<ClickEvent>(OnRestartClicked);
            
            _submitButton.UnregisterCallback<MouseEnterEvent>(OnHover);
            _restartButton.UnregisterCallback<MouseEnterEvent>(OnHover);
        }
        
        public void ShowEndScreen()
        {
            _mainContainer.RemoveFromClassList("hide");
            LoadScoreboard();
        }

        private void Update()
        {
            _playerScore.text = $"YOUR SCORE: {Score.Instance.GetScore().ToString()}";
        }

        public void HideEndScreen()
        {
            _mainContainer.AddToClassList("hide");
        }

        private void LoadScoreboard()
        {
            LoadTopPlayerScores();
        }

        private void LoadTopPlayerScores()
        {
            StartCoroutine(FetchHighscore());
        }

        private void OnRestartClicked(ClickEvent evt)
        {
            SoundManager.Instance.PlaySound(clickClip, transform, 1f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        private void OnSubmitClicked(ClickEvent evt)
        {
            var playerName = _playerName.value;
            int playerScore = Score.Instance.GetScore();
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
                        SoundManager.Instance.PlaySound(clickClip, transform, 1f);
                        StartCoroutine(FetchHighscore());
                    }
                    else
                    {
                        SoundManager.Instance.PlaySound(errorClip, transform, 1f);
                        Debug.Log("SubmitScore fehlgeschlagen: " + scoreResponse.errorData?.message);
                    }
                });
            });
            StartCoroutine(WaitSeconds());
        }

        private IEnumerator WaitSeconds()
        {
            yield return new WaitForSeconds(5);
            StartCoroutine(FetchHighscore());
        }

        private void OnPlayerNameChanged(ChangeEvent<string> evt)
        {
            _playerName.value = evt.newValue.ToUpper();
        }
        
        private IEnumerator FetchHighscore()
        {
            bool done = false;
            LootLockerSDKManager.GetScoreList(_leaderboardKey, 8, 0, (response) =>
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
        
        private void OnHover(MouseEnterEvent evt)
        {
            SoundManager.Instance.PlaySound(hoverClip, transform, 1f);
        }
    }
}
