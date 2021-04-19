using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.GameScripts;
using UnityEngine.UI;

public class PauseActivity : MonoBehaviour
{
    public GameObject pausePanel;
    public AudioSource gameMusic;
    public Text MusicText;
    public Text SoundText;


    public void Pause(GameObject pauseButton)
    {
        if (GameTimer.elapsedTime > 2F)
        {
            pauseButton.SetActive(false);
            this.pausePanel.SetActive(true);
            Time.timeScale = 0;
            GameTimer.Stop();
        }
    }
    public void Resume(GameObject pauseButton)
    {
        pauseButton.SetActive(true);
        this.pausePanel.SetActive(false);
        Time.timeScale = 1;
        GameTimer.Start();
    }

    public void ToggleMusic()
    {
        if (MusicScript.MusicToggle)
        {
            gameMusic.Stop();
            MusicScript.MusicToggle = false;
            PlayerPrefs.SetInt("MusicToggle", 0);
            MusicText.text = "Music Off";
        }
        else
        {
            gameMusic.Play();
            MusicScript.MusicToggle = true;
            PlayerPrefs.SetInt("MusicToggle", 1);
            MusicText.text = "Music On";
        }
    }

    public void ToggleSound()
    {
        if (MusicScript.SoundEffToggle)
        {
            MusicScript.SoundEffToggle = false;
            PlayerPrefs.SetInt("SoundToggle", 0);
            SoundText.text = "Sound Off";
        }
        else
        {
            MusicScript.SoundEffToggle = true;
            PlayerPrefs.SetInt("SoundToggle", 1);
            SoundText.text = "Sound On";
        }
    }
}