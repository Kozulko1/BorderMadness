using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.GameScripts;
using UnityEngine.UI;
using Firebase;
using Firebase.Unity.Editor;
using Firebase.Database;

public class PlayerInfluence : MonoBehaviour
{
    private Collider2D hitPoint;
    private IClickableObject controlledObject;
    private ISwipeableObject swipeableObject;
    #region public Game Objects
    public AppOverlay appOverlay;
    public GameObject hatDanceLoop;
    public GameObject pauseButton;
    public GameObject oneMoreChancePanel;
    public GameObject loseSound;
    public GameObject hammeringSound;
    public GameObject rockCollisionSound;
    public BackgroundControl backgroundControl;
    public Text continuePanelCounter;
    #endregion
    private static bool lostGame = false;
    private DatabaseReference DBref;
    private bool firstlose = false;
    private int lifePoints = 3;
    private Vector2 startPosition = new Vector2();
    private Vector2 endPosition = new Vector2();
    private int bestScore = 0;

    public void LoseGame()
    {
        appOverlay.LoseTextHandler();
        Time.timeScale = 1;
        GameTimer.ContinueStop();
        pauseButton.SetActive(false);
        GameTimer.Stop();
        GameTimer.Reset();
        if (MusicScript.SoundEffToggle)
        {
            loseSound.GetComponent<AudioSource>().Play();
        }
        hatDanceLoop.GetComponent<AudioSource>().Stop();
        PlayerInfluence.lostGame = !PlayerInfluence.lostGame;
        appOverlay.LostGameScreen();
        lifePoints = 3;
        firstlose = false;
        AdHandler.GamesPlayed++;
        if(FBScript.user != null)
        {
            if (appOverlay.GetScore() > bestScore)
            {
                writeNewUser(FBScript.auth.CurrentUser.UserId,
                    FBScript.user.DisplayName, appOverlay.GetScore());
            }
        }
        AdHandler.ShowAd();
    }

    public void OneMoreChance()
    {
        firstlose = true;
        GameTimer.ContinueStart();
        oneMoreChancePanel.SetActive(true);
        GameTimer.Stop();
    }

    public void DecreaseLife()
    {
        if(lifePoints > 0)
        {
            lifePoints--;
            if (MusicScript.SoundEffToggle)
            {
                rockCollisionSound.GetComponent<AudioSource>().Play();
            }
        }      
    }

    public void IncreaseLifePoints()
    {
        if (MusicScript.SoundEffToggle)
        {
            hammeringSound.GetComponent<AudioSource>().Play();
        }
        if(lifePoints < 3)
        {
            lifePoints++;
        }
    }

    public int GetLifePoints()
    {
        return this.lifePoints;
    }

    public void ResetForNextGame()
    {
        lifePoints = 3;
        firstlose = false;
    }

    private void Update ()
    {
        if (!GameTimer.Pause)
        {
            if (Input.touches.Length > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    Vector3 position = Camera.main.ScreenToWorldPoint(touch.position);
                    hitPoint = Physics2D.OverlapPoint(position);
                    if (hitPoint != null)
                    {
                        controlledObject = hitPoint.transform.GetComponent<IClickableObject>();
                        controlledObject.Ability();
                    }
                }
            }
        }       
	}
    

    //Treba riješiti sranje s firstloseom da se ne zove stalno, vjv u lateupdateu
    private void LateUpdate()
    {
        if (!GameTimer.Pause)
        {
            if (Input.touches.Length > 0)
            {
                Touch touch = Input.touches[0];
                float deltaX = 0F;
                float deltaY = 0F;

                if (touch.phase == TouchPhase.Began)
                {
                    Vector3 position = Camera.main.ScreenToWorldPoint(touch.position);
                    hitPoint = Physics2D.OverlapPoint(position);
                    startPosition = touch.position;
                    if (hitPoint != null)
                    {
                        swipeableObject = hitPoint.transform.GetComponent<ISwipeableObject>();
                    }
                }
                if (touch.phase == TouchPhase.Moved)
                {
                    endPosition = touch.position;
                    deltaX = endPosition.x - startPosition.x;
                    deltaY = endPosition.y - startPosition.y;
                    swipeableObject.Swipe(deltaX, deltaY);
                }            
            }
        }

        //---------------------------------------------------

        if (!PlayerInfluence.lostGame)
        {
            if (lifePoints == 3)
            {
                backgroundControl.SetHP3();
            }
            else if (lifePoints == 2)
            {
                backgroundControl.SetHP2();
            }
            else if (lifePoints == 1)
            {
                backgroundControl.SetHP1();
            }
            else if (lifePoints == 0)
            {
                backgroundControl.SetHP0();
                continuePanelCounter.text = GameTimer.continueTime.ToString();
                if (!firstlose)
                {
                    Time.timeScale = 0;
                    GameTimer.Stop();
                    pauseButton.SetActive(false);
                    if(Application.internetReachability != NetworkReachability.NotReachable
                        && Advertisements.IsReady())
                    {
                        OneMoreChance();
                    }
                    else
                    {
                        LoseGame();
                    }
                }
                else if (GameTimer.continueTime < 1 && firstlose || AdHandler.ContinuePressed)
                {
                    oneMoreChancePanel.SetActive(false);
                    LoseGame();
                }
            }
        }
    }

    private void Awake()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("Database url goes here");
        DBref = FirebaseDatabase.DefaultInstance.RootReference;

        if (FBScript.user != null)
        {
            FirebaseDatabase.DefaultInstance
                .GetReference("users")
                .Child(FBScript.user.UserId)
                .GetValueAsync().ContinueWith(task =>
                {
                    if (task.IsFaulted)
                    {
                    }
                    else if (task.IsCompleted)
                    {
                        DataSnapshot snapshot = task.Result;

                    bestScore = System.Convert.ToInt32(snapshot.Child("score").Value);
                    }
                });
        }
    }

    private void writeNewUser(string userId, string name, int score)
    {
        string nickname;
        User user;
        if (PlayerPrefs.HasKey("Nickname"))
        {
            if(PlayerPrefs.GetString("Nickname").Length < 21)
            {
                nickname = PlayerPrefs.GetString("Nickname");
            }
            else
            {
                nickname = PlayerPrefs.GetString("Nickname").Substring(0, 20);
            }
            user = new User(nickname, score);
        }
        else
        {
            user = new User(name, score);
        }
        string json = JsonUtility.ToJson(user);

        DBref.Child("users").Child(userId).SetRawJsonValueAsync(json);
    }
}