using System.Collections;
using LootLocker.Requests;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class LeaderboardScreen : MonoBehaviour
    {
        private UIDocument _doc;
        private VisualElement _mainContainer;
        private MenuManager _menuManager;

        private Button _backButton;
        
        private Label _leaderboardNames;
        private Label _leaderboardScores;
        
        
        private readonly string _leaderboardKey = "global_highscore";

        private void Awake()
        {
            _doc = GetComponent<UIDocument>();
            _menuManager = GetComponentInParent<MenuManager>();
            _mainContainer = _doc.rootVisualElement.Q("MainContainer");

            _backButton = _doc.rootVisualElement.Q("BackButton") as Button;
            _leaderboardNames = _doc.rootVisualElement.Q("LBL_Names") as Label;
            _leaderboardScores = _doc.rootVisualElement.Q("LBL_Scores") as Label;
        }

        private void OnEnable()
        {
            _backButton.RegisterCallback<ClickEvent>(OnBackButtonClicked);
            
        }

        private void OnDisable()
        {
            _backButton.UnregisterCallback<ClickEvent>(OnBackButtonClicked);
        }
        
        public void ShowLeaderboardScreen()
        {
            _mainContainer.RemoveFromClassList("hide");
            _backButton.AddToClassList("button");
            StartCoroutine(FetchHighscore());
        }
        

        public void HideLeaderboardScreen()
        {
            _backButton.RemoveFromClassList("button");
            _mainContainer.AddToClassList("hide");
        }
        
        private void OnBackButtonClicked(ClickEvent evt)
        {
            _menuManager.OpenStartMenu();
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
    }
}
