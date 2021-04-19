using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.GameScripts;

public class SupremeLeader : MonoBehaviour, ISwipeableObject
{
    private float speed;
    public PlayerInfluence playerInfluence;
    private Vector3 tempPosition;
    private float tempY;
    private float sinAngle;
    private bool sinDirection;
    private Vector3 vector;
    public AppOverlay appOverlay;
    public AudioSource fakeNews;
    public AudioSource swipeSound;
    private bool swiped = false;
    public bool Touched { get; set; }

    private void Start()
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
    void Update()
    {
        if (!Touched)
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
                this.transform.position = tempPosition;
            }
        }
        else
        {
            this.transform.Translate(vector * Time.deltaTime * 5000F);
        }     
        if (this.transform.position.x < -99F)
        {
            OnBecameInvisible();
            Destroy(gameObject);
        }
        if(this.transform.position.y > 1585F || this.transform.position.y < -116F || this.transform.position.x > 3100F)
        {
            OffTheScreen();
        }
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed * 0.95F;
    }

    public void Ability()
    {
        appOverlay.UpdateScore(3);
        Destroy(gameObject);
    }

    public GameObject SwipeableObject
    {
        get
        {
            return this.gameObject;
        }
    }

    private void OnBecameInvisible()
    {
        playerInfluence.DecreaseLife();
    }

    private void OffTheScreen()
    {
        if (MusicScript.SoundEffToggle)
        {
            fakeNews.Play();
        }
        Ability();
    }

           
    public void Swipe(float x, float y)
    {
        float normalizer = Mathf.Sqrt(Mathf.Pow(x, 2F) + Mathf.Pow(y, 2F));
        if (normalizer > 50F)
        {
            if (!swiped)
            {
                if (MusicScript.SoundEffToggle)
                {
                    swipeSound.Play();
                }
                this.vector = new Vector3(x / normalizer, y / normalizer);
                this.Touched = true;
                this.swiped = true;
            }
        }
    }
}