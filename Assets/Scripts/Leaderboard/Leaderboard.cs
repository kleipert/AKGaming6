using System.Collections;
using UnityEngine;
using LootLocker.Requests;

public class Leaderboard : MonoBehaviour
{
    private string leaderboardKey = "global_highscore";

    public IEnumerator SubmitScoreRoutine(int scoreToUpload)
    {
        bool done = false;
        LootLockerSDKManager.SubmitScore("", scoreToUpload, leaderboardKey, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Successfully uploaded score: " + scoreToUpload);
            }
            else
            {
                Debug.Log("Failed to submit score: " + response.errorData?.ToString());
            }
            done = true;
        });
        yield return new WaitWhile(() => !done);
    }
}