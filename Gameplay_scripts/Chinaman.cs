using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.GameScripts;

public class Chinaman : MonoBehaviour, IClickableObject
{
    private float speed;
    public PlayerInfluence playerInfluence;
    private Vector3 tempPosition;
    private float tempY;
    private float sinAngle;
    private bool sinDirection;
    public GameObject chinaSound;
    public AudioSource china15million;
    public AppOverlay appOverlay;

    void Start ()
    {
        tempPosition = this.transform.position;
        this.tempY = tempPosition.y;
        this.sinAngle = 0;
        if (Random.Range(0F, 1F) < 0.5F)
        {
            this.sinDirection = true;
        }
        else
        {
            this.sinDirection = false;
        }
    }

	void Update ()
    {
        tempPosition.x -= this.speed * Time.deltaTime;
        if (!GameTimer.Pause)
        {
            if (this.sinDirection)
            {
                this.sinAngle += 1F / 60F;
            }
            else
            {
                this.sinAngle -= 1F / 60F;
            }
            tempPosition.y = tempY + 300 * Mathf.Sin(4F * this.sinAngle);
        }
        this.transform.position = tempPosition;
        if (this.transform.position.x < -115F)
        {
            OnBecameInvisible();
        }
    }
    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public void Ability()
    {
        if (MusicScript.SoundEffToggle)
        {
            chinaSound.GetComponent<AudioSource>().Play();
        }
        playerInfluence.DecreaseLife();
        Destroy(gameObject);
    }

    public GameObject PlayableObject
    {
        get
        {
            return this.gameObject;
        }
    }

    private void OnBecameInvisible()
    {
        appOverlay.UpdateScore();
        if (MusicScript.SoundEffToggle)
        {
            china15million.Play();
        }
        Destroy(gameObject);      
    }
}
