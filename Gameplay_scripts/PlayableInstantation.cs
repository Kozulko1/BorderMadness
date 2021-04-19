using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.GameScripts;

public class PlayableInstantation : MonoBehaviour
{
    public GameObject gameMaster;
    private float speed;
    private float spawnTime;
    private float spawnDeltaTime;
    public List<GameObject> activeObjects = new List<GameObject>();
    public GameObject mexican;
    public GameObject fatGonzales;
    public GameObject chinaman;
    public GameObject facade;
    public GameObject supremeLeader;
    public PlayerInfluence playerInfluenceRef;
    private GameObject tempPlayable;
    private IClickableObject clickableObject;
    private ISwipeableObject swipeableObject;


    void Start ()
    {
        spawnDeltaTime = 0F;
	}
	
    public void DestroyAllActiveObjects()
    {
        foreach(GameObject go in activeObjects)
        {
            Destroy(go);
        }
    }

	void Update ()
    {
        speed = MathFormulas.GetSpeed(GameTimer.elapsedTime);
        spawnTime = MathFormulas.GetSpawnTime(GameTimer.elapsedTime);

        if (!PlayerInfluence.lostGame)
        {
            if (GameTimer.elapsedTime > spawnTime + spawnDeltaTime)
            {
                float randomValue = Random.Range(0F, 100F);
                if((activeObjects.Count % 10 == 0 && playerInfluenceRef.GetLifePoints() == 2) 
                    || (activeObjects.Count % 5 == 0 && playerInfluenceRef.GetLifePoints() == 1))
                {
                    tempPlayable = Instantiate
                        (facade, SpawnPosition.GetFacadeSpawn(), this.transform.rotation);
                    clickableObject = tempPlayable.GetComponent<Facade>();
                }

                else if (randomValue <= 25F && randomValue > 15F && this.activeObjects.Count > 7)
                {
                    tempPlayable = Instantiate
                        (fatGonzales, SpawnPosition.GetGonzalesSpawnPoint(), this.transform.rotation);
                    clickableObject = tempPlayable.GetComponent<FatGonzales>();
                }

                else if (randomValue <= 15F && randomValue > 5F && this.activeObjects.Count > 10)
                {
                    tempPlayable = Instantiate
                        (supremeLeader, SpawnPosition.GetBasicSpawnPoint(), this.transform.rotation);
                    swipeableObject = tempPlayable.GetComponent<SupremeLeader>();
                }

                else if(randomValue <= 5F && this.activeObjects.Count > 10)
                {
                    tempPlayable = Instantiate
                        (chinaman, SpawnPosition.GetBasicSpawnPoint(), this.transform.rotation);
                    clickableObject = tempPlayable.GetComponent<Chinaman>();
                }

                else
                {
                    tempPlayable = Instantiate
                        (mexican, SpawnPosition.GetBasicSpawnPoint(), this.transform.rotation);
                    clickableObject = tempPlayable.GetComponent<BasicMexican>();
                }

                spawnDeltaTime = GameTimer.elapsedTime;

                if(randomValue <= 15F  && randomValue > 5F && activeObjects.Count > 10)
                {
                    swipeableObject.SetSpeed(speed);
                }
                else
                {
                    clickableObject.SetSpeed(speed);
                    //print(GameTimer.elapsedTime + "   " + speed);
                }         
                activeObjects.Add(tempPlayable.gameObject);
            }
        }
	}
    private void LateUpdate()
    {
        if (PlayerInfluence.lostGame)
        {
            foreach (GameObject playableObject in activeObjects)
            {
                Destroy(playableObject);
            }
        }
    }
}