using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Messaging;

public class FirebaseManager : MonoBehaviour
{
    FirebaseApp _app;
    private void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            if (task.Result == DependencyStatus.Available)
            {
                _app = FirebaseApp.DefaultInstance;
                Firebase.Messaging.FirebaseMessaging.TokenReceived += OnTokenReceived;
                Firebase.Messaging.FirebaseMessaging.MessageReceived += OnMessageReceived;


            }
            else
            {
                Debug.LogError("Could not resolce all: " + task.Result);
            }
        });
    }
    public void OnTokenReceived(object sender, Firebase.Messaging.TokenReceivedEventArgs token)
    {
        UnityEngine.Debug.Log("Received Registration Token: " + token.Token);
    }

    public void OnMessageReceived(object sender, Firebase.Messaging.MessageReceivedEventArgs e)
    {
        UnityEngine.Debug.Log("Received a new message from: " + e.Message.From);
    }
    //public void OnTokenReceived(Object sender, TokenReceivedEventArgs e)
    //{
    //    if(e != null)
    //    {
    //        Debug.LogFormat("[FIREBASE] Token: {0}", e.Token);
    //    }
    //}

    //public void OnMessageReceived(Object sender, MessageReceivedEventArgs e)
    //{
    //    if(e != null && e.Message != null && e.Message.Notification != null)
    //    {
    //        Debug.LogFormat("[FIREBASE] From: {0}, Title: {1}, Text: {2}",
    //            e.Message.From,
    //            e.Message.Notification.Title,
    //            e.Message.Notification.Body);
    //    }
    //}
}
