using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public FBScript fbRef;
    public ScoreBoard sbRef;
    public GameObject NickInput;
    public AudioSource panelSound;
    public AudioSource menuMusic;
    public Text MusicText;
    public Text SoundText;

    private static bool clicked = false;
	public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
        clicked = false;
        if (MusicScript.SoundEffToggle)
        {
            panelSound.Play();
        }
    }

    public void OpenPanel(GameObject panel)
    {
        if (!clicked)
        {
            panel.SetActive(true);
            if (MusicScript.SoundEffToggle)
            {
                panelSound.Play();
            }
        }
        clicked = true;
    }
    public void fbLog()
    {
        fbRef.Login();
    }
    public void DataRetrieve()
    {
        sbRef.RetrieveData();
    }
    public void SetNickname()
    {
        PlayerPrefs.SetString("Nickname", NickInput.GetComponent<InputField>().text);
    }

    public void ToggleMusic()
    {
        if (MusicScript.MusicToggle)
        {
            menuMusic.Stop();
            MusicScript.MusicToggle = false;
            PlayerPrefs.SetInt("MusicToggle", 0);
            MusicText.text = "Music Off";
        }
        else
        {
            menuMusic.Play();
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