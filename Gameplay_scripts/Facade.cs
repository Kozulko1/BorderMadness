using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.GameScripts;

public class Facade : MonoBehaviour, IClickableObject
{
    private float speed;
    public PlayerInfluence playerInfluence;
    private Vector3 tempPosition;
    private float tempY;
    public AppOverlay appOverlay;

    private void Start()
    {
        tempPosition = this.transform.position;
        this.tempY = tempPosition.y;
    }
    void Update()
    {
        tempPosition.x -= this.speed * Time.deltaTime * 1.25F;
        this.transform.position = tempPosition;
        if (this.transform.position.x < -99F)
        {
            Destroy(gameObject);
        }
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public void Ability()
    {
        playerInfluence.IncreaseLifePoints();
        Destroy(gameObject);
    }

    public GameObject PlayableObject
    {
        get
        {
            return this.gameObject;
        }
    }

}
