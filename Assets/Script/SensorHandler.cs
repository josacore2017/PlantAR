using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System;

public class SensorHandler : MonoBehaviour {

    ArrayList leaderBoard = new ArrayList();
    Vector2 scrollPosition = Vector2.zero;
    private Vector2 controlsScrollViewVector = Vector2.zero;

    public GUISkin fb_GUISkin;

    private const int MaxScores = 5;
    private string logText = "";
    private string email = "";
    private int score = 100;
    private Vector2 scrollViewVector = Vector2.zero;
    protected bool UIEnabled = true;

    const int kMaxLogSize = 16382;
    DependencyStatus dependencyStatus = DependencyStatus.UnavailableOther;

    void Start () {
        leaderBoard.Clear();
        leaderBoard.Add("Firebase Top " + MaxScores.ToString() + " Scores");

        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                InitializeFirebase();
            }
            else
            {
                Debug.LogError(
                  "Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });
    }

    protected virtual void InitializeFirebase()
    {
        FirebaseApp app = FirebaseApp.DefaultInstance;
        app.SetEditorDatabaseUrl("https://devise-omniauth-174704.firebaseio.com/");
        if (app.Options.DatabaseUrl != null)
            app.SetEditorDatabaseUrl(app.Options.DatabaseUrl);
        StartListener();
    }

    protected void StartListener()
    {
        FirebaseDatabase.DefaultInstance
          .GetReference("mod").Child("modulo1").OrderByChild("id")
          .ValueChanged += (object sender2, ValueChangedEventArgs e2) => {
              if (e2.DatabaseError != null)
              {
                  Debug.LogError(e2.DatabaseError.Message);
                  return;
              }
              Debug.Log("Received values for Leaders.");
              Debug.Log(" - "+e2.Snapshot.Child("sensor1"));
              string title = leaderBoard[0].ToString();
              leaderBoard.Clear();
              leaderBoard.Add(title);
              if (e2.Snapshot != null && e2.Snapshot.ChildrenCount > 0)
              {
                  foreach (var childSnapshot in e2.Snapshot.Children)
                  {
                      if (childSnapshot.Child("sensor1") == null
                    || childSnapshot.Child("sensor2").Value == null)
                      {
                          Debug.LogError("Bad data in sample.  Did you forget to call SetEditorDatabaseUrl with your project id?");
                          break;
                      }
                      else
                      {
                          Debug.Log("Leaders entry : " +
                        childSnapshot.Child("sensor1").Value.ToString() + " - " +
                        childSnapshot.Child("sensor2").Value.ToString());
                          leaderBoard.Insert(1, childSnapshot.Child("sensor1").Value.ToString()
                        + "  " + childSnapshot.Child("sensor2").Value.ToString());
                      }
                  }
              }
          };
    }

    protected virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
