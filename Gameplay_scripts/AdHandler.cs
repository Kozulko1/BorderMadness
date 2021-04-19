using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.GameScripts;
using UnityEngine.Advertisements;

public class AdHandler : MonoBehaviour
{
    public static int GamesPlayed = 0;
    public static bool ContinuePressed = false;
    public GameObject ContinueTimer;
    public GameObject ContinuePanel;
    public PlayableInstantation playableInstantationRef;
    public void ContinueClick()
    {
        ShowOptions options = new ShowOptions();
        options.resultCallback = HandleShowResult;
        GameTimer.ContinueStop();
        ContinuePanel.SetActive(false);
        Advertisement.Show("rewardedVideo", options);
    }

    private void HandleShowResult(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            Debug.Log("Video completed - Offer a reward to the player");
            ContinuePanel.SetActive(false);
            GameTimer.ContinueStop();
            ContinueTimer.SetActive(true);
            ContinuePressed = true;
            playableInstantationRef.DestroyAllActiveObjects();
            Time.timeScale = 1;
        }
        else if (result == ShowResult.Skipped)
        {
            Debug.LogWarning("Video was skipped - Do NOT reward the player");

        }
        else if (result == ShowResult.Failed)
        {
            Debug.LogError("Video failed to show");
        }
    }

    public static void ShowAd()
    {
        if (PlayerPrefs.HasKey("GamesPlayed"))
        {
            int gamesPlayed = PlayerPrefs.GetInt("GamesPlayed");
            if (gamesPlayed % 2 == 0)
            {
                Advertisement.Show();
            }
            PlayerPrefs.SetInt("GamesPlayed", gamesPlayed + 1);
        }
        else
        {
            PlayerPrefs.SetInt("GamesPlayed", 1);
        }
    }
}
