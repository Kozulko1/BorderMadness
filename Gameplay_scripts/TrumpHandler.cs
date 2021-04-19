using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.GameScripts;

public class TrumpHandler : MonoBehaviour
{
    public List<AudioSource> SoundList = new List<AudioSource>();
    private bool isReady;
    private System.Random generator = new System.Random();

    private void Update()
    {
        if((int)GameTimer.elapsedTime % 13 == 0 && isReady && GameTimer.elapsedTime > 5F)
        {
            if (MusicScript.SoundEffToggle)
            {
                int x = generator.Next(0, SoundList.Count);
                SoundList[x].Play();
                print("played music");
                isReady = false;
            }
        }
        if((int)GameTimer.elapsedTime % 17 == 0)
        {
            isReady = true;
        }
    }
}
