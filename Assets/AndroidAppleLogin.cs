#if UNITY_ANDROID
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayNANOO;

public class AndroidAppleLogin : MonoBehaviour
{
    Plugin plugin;

    void Start()
    {
        plugin = Plugin.GetInstance();
    }

    public void SignIn()
    {
        plugin.OpenAppleID((status, errorCode, jsonString, values) =>
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
#endif