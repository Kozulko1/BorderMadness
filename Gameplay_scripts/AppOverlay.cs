using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.GameScripts;
using UnityEngine.UI;

public class AppOverlay : MonoBehaviour
{
    public GameObject lostGamePanel;
    public Text loseText;
    private int score = 0;
    public Text scoreText;

    public void UpdateScore()
    {
        score++;
        this.scoreText.text = "Score: " + score.ToString();
    }
    public void UpdateScore(int x)
    {
        score += x;
        this.scoreText.text = "Score: " + score.ToString();
    }

    public void LostGameScreen()
    {
        GameTimer.Stop();
        lostGamePanel.SetActive(true);
    }
    public void RestartLevel()
    {
        AdHandler.ContinuePressed = false;
        GameTimer.Stop();
        GameTimer.Reset();
        lostGamePanel.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        PlayerInfluence.lostGame = false;
    }
    public void ReturnToMenu()
    {
        AdHandler.ContinuePressed = false;
        GameTimer.Stop();
        GameTimer.Reset();
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
        PlayerInfluence.lostGame = false;
    }

    public void LoseTextHandler()
    {
        if(score < 50)
        {
            loseText.text = score.ToString() + "? DEPORTED!";
        }
        else if(score >= 50 && score < 100)
        {
            loseText.text = score.ToString() + "? Not Bad!";
        }
        else if(score >= 100 && score < 200)
        {
            loseText.text = score.ToString() + "? Very Nice!";
        }
        else
        {
            loseText.text = score.ToString() + "? WOW, LIT AF!!";
        }
    }

    public int GetScore()
    {
        return this.score;
    }
}