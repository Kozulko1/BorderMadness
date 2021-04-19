using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using Firebase.Auth;
using UnityEngine.UI;

public class FBScript : MonoBehaviour
{
    private bool loggedIn = false;
    public static FirebaseUser user;
    public static FirebaseAuth auth;
    public GameObject fbButton;
    public Text loggedText;

    public void Login()
    {
        if (!loggedIn)
        {
            var perms = new List<string>() { "public_profile", "email", "user_friends" };
            FB.LogInWithReadPermissions(perms, AuthCallback);
        }
        else
        {
            this.SignOut();
        }
    }
    private void Awake()
    {
        if (!FB.IsInitialized)
        {
            auth = FirebaseAuth.DefaultInstance;
            // Initialize the Facebook SDK
            FB.Init(InitCallback, OnHideUnity);
        }
        else
        {
            // Already initialized, signal an app activation App Event
            FB.ActivateApp();
        }
        if(user != null)
        {
            loggedIn = true;
            fbButton.GetComponentInChildren<Text>().text = "Sign Out";
            loggedText.text = "You are logged in as:   " + user.DisplayName;
        }
        else
        {
            loggedIn = false;
            fbButton.GetComponentInChildren<Text>().text = "Login with Facebook";
            loggedText.text = string.Empty;
        }
    }

    private void InitCallback()
    {
        if (FB.IsInitialized)
        {
            // Signal an app activation App Event
            FB.ActivateApp();
            Debug.Log("FB Succes");
            // Continue with Facebook SDK
            // ...
            if (AdHandler.GamesPlayed == 0)
            {
                Login();
            }
        }
        else
        {
            Debug.Log("Failed to Initialize the Facebook SDK");
        }
    }

    private void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            // Pause the game - we will need to hide
            Time.timeScale = 0;
        }
        else
        {
            // Resume the game - we're getting focus again
            Time.timeScale = 1;
        }
    }

    private void AuthCallback(ILoginResult result)
    {
        if (FB.IsLoggedIn)
        {
            // AccessToken class will have session details
            var aToken = AccessToken.CurrentAccessToken;
            // Print current access token's User ID
            Debug.Log(aToken.UserId);
            // Print current access token's granted permissions
            foreach (string perm in aToken.Permissions)
            {
                Debug.Log(perm);
            }
            //auth = FirebaseAuth.DefaultInstance;

            Credential credential = FacebookAuthProvider.GetCredential(aToken.TokenString);
            auth.SignInWithCredentialAsync(credential).ContinueWith(task => {
                if (task.IsCanceled)
                {
                    Debug.LogError("SignInWithCredentialAsync was canceled.");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("SignInWithCredentialAsync encountered an error: " + task.Exception);
                    return;
                }

                user = task.Result;
                Debug.LogFormat("User signed in successfully: {0} ({1})",
                    user.DisplayName, user.UserId);

                loggedIn = true;
            });
        }
        else
        {
            Debug.Log("User cancelled login");
        }
    }

    private void Update()
    {
        if (loggedIn)
        {
            fbButton.GetComponentInChildren<Text>().text = "Sign Out";
            loggedText.text = "You are logged in as:   " + user.DisplayName;
        }
        else
        {
            loggedIn = false;
            fbButton.GetComponentInChildren<Text>().text = "Login with Facebook";
            loggedText.text = string.Empty;
        }
    }

    private void SignOut()
    {
        auth.SignOut();
        user = null;
        loggedIn = false;
    }
}