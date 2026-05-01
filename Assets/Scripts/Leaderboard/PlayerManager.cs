using System.Collections;
using UnityEngine;
using LootLocker.Requests;

public class PlayerManager : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(LoginRoutine());
    }
    
    IEnumerator LoginRoutine()
    {
        bool done = false;
        LootLockerSDKManager.StartGuestSession((response) =>
            {
                if (response.success)
                {
                    Debug.Log("Player is logged in");
                    PlayerPrefs.SetString("PlayerID", response.player_id.ToString());
                    done = true;
                }
                else
                {
                    Debug.Log("Cloud not start session");
                    done = true;
                }
            }    
            );
        yield return new WaitWhile(() => !done);
    }
}
