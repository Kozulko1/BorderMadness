using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Nickname : MonoBehaviour
{
    public GameObject NickInput;
	private void Awake()
    {
        if (PlayerPrefs.HasKey("Nickname"))
        {
            if(PlayerPrefs.GetString("Nickname") != string.Empty)
            {
                NickInput.GetComponent<InputField>().text = PlayerPrefs.GetString("Nickname");
            }
        }
    }

    public void OptionsClick()
    {
        if (PlayerPrefs.HasKey("Nickname"))
        {
            if (PlayerPrefs.GetString("Nickname") != string.Empty)
            {
                NickInput.GetComponent<InputField>().text = PlayerPrefs.GetString("Nickname");
            }
        }
    }
}