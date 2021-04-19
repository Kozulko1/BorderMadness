using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicScript : MonoBehaviour
{
    public AudioSource MenuMusic;
    public Text MusicText;
    public Text SoundText;

    public static bool MusicToggle;
    public static bool SoundEffToggle;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("MusicToggle"))
        {
            if (PlayerPrefs.GetInt("MusicToggle") == 1)
            {
                MusicText.text = "Music On";
                MusicToggle = true;
                MenuMusic.Play();
            }
            else
            {
                MusicText.text = "Music Off";
                MusicToggle = false;
            }
        }
        else
        {
            MusicText.text = "Music On";
            PlayerPrefs.SetInt("MusicToggle", 1);
            MusicToggle = true;
            MenuMusic.Play();
        }

        if (PlayerPrefs.HasKey("SoundToggle"))
        {
            if(PlayerPrefs.GetInt("SoundToggle") == 1)
            {
                SoundText.text = "Sound On";
                SoundEffToggle = true;
            }
            else
            {
                SoundText.text = "Sound Off";
                SoundEffToggle = false;
            }
        }
        else
        {
            PlayerPrefs.SetInt("SoundToggle", 1);
            SoundText.text = "Sound On";
            SoundEffToggle = true;
        }
    }
}