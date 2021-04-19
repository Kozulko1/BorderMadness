using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicButtonTextAwake : MonoBehaviour
{
    public Text MusicText;
    public Text SoundText;

    private void Awake()
    {
        if (MusicScript.MusicToggle)
        {
            MusicText.text = "Music On";
        }
        else
        {
            MusicText.text = "Music Off";
        }

        if (MusicScript.SoundEffToggle)
        {
            SoundText.text = "Sound On";
        }
        else
        {
            SoundText.text = "Sound Off";
        }
    }
}