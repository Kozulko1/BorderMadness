using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Unity.Editor;
using Firebase.Database;
public class ScoreBoard : MonoBehaviour
{
    public Text scoreBoard;
    private DatabaseReference DBref;
    private int bestScore;

    private void Awake()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("firebase link goes here");
        DBref = FirebaseDatabase.DefaultInstance.RootReference;
    }
    public void RetrieveData()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            scoreBoard.text = "Loading...";
            FirebaseDatabase.DefaultInstance
                .GetReference("users")
                .OrderByChild("score")
                .LimitToLast(6)
                .GetValueAsync().ContinueWith(task =>
                {
                    if (task.IsCompleted)
                    {
                        List<string> playerScores = new List<string>();
                        DataSnapshot snapshot = task.Result;
                        scoreBoard.text = string.Empty;
                        foreach (var child in snapshot.Children)
                        {
                            playerScores.Add(child.Child("username").Value
                            + " - " + child.Child("score").Value);
                        }
                        int position = 0;
                        for (int i = playerScores.Count - 1; i >= 0; i--)
                        {
                            position++;
                            scoreBoard.text += position.ToString() + ". " + playerScores[i]
                            + System.Environment.NewLine;
                        }
                        if (FBScript.user != null)
                        {
                            FirebaseDatabase.DefaultInstance
                                .GetReference("users")
                                .Child(FBScript.user.UserId)
                                .GetValueAsync().ContinueWith(task1 =>
                                {
                                    if (task1.IsCompleted)
                                    {
                                        bestScore = System.Convert.ToInt32(task1.Result.Child("score").Value);
                                    }
                                    scoreBoard.text += System.Environment.NewLine +
                                    "Your best score: " + bestScore.ToString();
                                });
                        }
                        else
                        {
                            scoreBoard.text += System.Environment.NewLine +
                                    "You are not logged in" + System.Environment.NewLine;
                        }
                    }
                });
        }
        else
        {
            scoreBoard.text = "No internet connection";
        }
    }
}