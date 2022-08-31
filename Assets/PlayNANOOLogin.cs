#if UNITY_ANDROID
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using Google;
using PlayNANOO;

public class PlayNANOOLogin : MonoBehaviour
{
    Plugin plugin;
    GoogleSignInConfiguration googleSignInConfiguration;

    void Start()
    {
        plugin = Plugin.GetInstance();
        googleSignInConfiguration = new GoogleSignInConfiguration
        {
            RequestIdToken = true,
            WebClientId = "731589021464-1fnkntgithrc274e8bvb1ggdsbel61lp.apps.googleusercontent.com",
        };
    }

    public void SignIn()
    {
        GoogleSignIn.Configuration = googleSignInConfiguration;
        GoogleSignIn.Configuration.UseGameSignIn = false;
        GoogleSignIn.Configuration.RequestIdToken = true;
        GoogleSignIn.DefaultInstance.SignIn().ContinueWith(OnAuthenticationFinished);
    }

    internal void OnAuthenticationFinished(Task<GoogleSignInUser> task)
    {
        if (task.IsFaulted)
        {
            using (IEnumerator<System.Exception> enumerator = task.Exception.InnerExceptions.GetEnumerator())
            {
                if (enumerator.MoveNext())
                {
                    GoogleSignIn.SignInException error = (GoogleSignIn.SignInException)enumerator.Current;
                    Debug.Log("Got Error: " + error.Status + " " + error.Message);
                }
                else
                {
                    Debug.Log("Got Unexpected Exception?!?" + task.Exception);
                }
            }
        }
        else if (task.IsCanceled)
        {
            Debug.Log("Canceled");
        }
        else
        {
            plugin.AccountSocialSignIn(task.Result.IdToken, Configure.PN_ACCOUNT_GOOGLE, (status, errorCode, jsonString, values) =>
            {
                if (status == Configure.PN_API_STATE_SUCCESS)
                {
                    Debug.Log(values["access_token"].ToString());
                    Debug.Log(values["refresh_token"].ToString());
                    Debug.Log(values["uuid"].ToString());
                    Debug.Log(values["openID"].ToString());
                    Debug.Log(values["nickname"].ToString());
                    Debug.Log(values["linkedID"].ToString());
                    Debug.Log(values["linkedType"].ToString());
                    Debug.Log(values["country"].ToString());
                }
                else
                {
                    Debug.Log("Fail");
                }
            });
        }
    }
}
#endif