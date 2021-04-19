using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.GameScripts;

public class StartTimer : MonoBehaviour
{
    private bool firstStart = true;
    private int countdown = 3;
    private bool loopStart = true;
    public Text countdownText;
    public GameObject hatDanceStart;
    public GameObject hatDanceLoop;
    public AudioSource makeAmericaGreat;
    private void Awake()
    {
        if (MusicScript.SoundEffToggle)
        {
            makeAmericaGreat.Play();
        }
    }
    void Start ()
    {
        StartCoroutine("StartTime");
	}
	
	void Update ()
    {
        if (this.firstStart)
        {
            this.countdownText.text = countdown.ToString();
        }
        if(this.countdown == 2)
        {
            if (MusicScript.MusicToggle)
            {
                this.hatDanceStart.GetComponent<AudioSource>().Play();
            }
        }
        if(countdown < 1 && this.firstStart)
        {
            this.countdownText.text = "Protect The Border!";
            StopCoroutine("StartTime");
            GameTimer.Start();
            this.firstStart = false;
        }
	}
    private void LateUpdate()
    {
        if(GameTimer.elapsedTime > 1.8F && this.loopStart)
        {
            Debug.Log("loop start");
            if (MusicScript.MusicToggle)
            {
                this.hatDanceLoop.GetComponent<AudioSource>().Play();
            }
            this.loopStart = false;
        }
        if (GameTimer.elapsedTime > 1.2F)
        {
            this.countdownText.text = string.Empty;
        }
        if(GameTimer.elapsedTime > 1.9F)
        {
            this.gameObject.SetActive(false);
        }
    }
    IEnumerator StartTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            countdown--;
        }
    }
}
