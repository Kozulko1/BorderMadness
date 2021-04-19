using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.GameScripts;

public class ContinueTimer : MonoBehaviour
{
    public GameObject pauseButton;
    public PlayerInfluence playerInfluenceRef;
    public AudioSource congratulationsSound;
    public Text continueText;
    private int timeLeft = 3;

    private void Start()
    {
        playerInfluenceRef.IncreaseLifePoints();
        StartCoroutine("Continue");
    }

    private void Update()
    {
        continueText.text = timeLeft.ToString();
        if(timeLeft < 1)
        {
            if (MusicScript.SoundEffToggle)
            {
                congratulationsSound.Play();
            }
            GameTimer.Start();
            StopCoroutine("Continue");
            pauseButton.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }

    public IEnumerator Continue()
    {
        while (true)
        {
            yield return new WaitForSeconds(1F);
            timeLeft--;
        }
    }
}
