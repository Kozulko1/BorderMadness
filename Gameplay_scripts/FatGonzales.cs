using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.GameScripts;

public class FatGonzales : MonoBehaviour, IClickableObject
{
    private float speed;
    public PlayerInfluence playerInfluence;
    private Vector3 tempPosition;
    private int healthPoints = 3;
    public GameObject hitSound;
    public GameObject destructionSound;
    public AppOverlay appOverlay;

    void Start ()
    {
        tempPosition = this.transform.position;
    }

	void Update ()
    {
        tempPosition.x -= this.speed * Time.deltaTime;
        this.transform.position = this.tempPosition;
        this.transform.Rotate(0F, 0F, speed * Time.deltaTime / 2.7F);
        if (healthPoints == 2)
        {
            Color woundedRed = new Color(255F, 163F, 163F, 255F);
            this.gameObject.GetComponentInChildren<SpriteRenderer>().color = woundedRed;
        }
        if (healthPoints == 1)
        {
            Color ultraRed = new Color(255F, 109F, 109F, 255F);
            this.gameObject.GetComponentInChildren<SpriteRenderer>().color = ultraRed;
        }
        if (this.transform.position.x < -99F)
        {
            OnBecameInvisible();
            Destroy(gameObject);
        }
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed * 0.8F;
    }

    public void Ability()
    {
        this.healthPoints--;
        if (healthPoints == 0)
        {
            appOverlay.UpdateScore(3);
            if (MusicScript.SoundEffToggle)
            {
                this.destructionSound.GetComponent<AudioSource>().Play();
            }
            Destroy(gameObject);
        }
        else
        {
            if (MusicScript.SoundEffToggle)
            {
                this.hitSound.GetComponent<AudioSource>().Play();
            }
        }
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
        playerInfluence.DecreaseLife();
    }
}
