#if UNITY_IOS
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppleAuth;
using AppleAuth.Native;
using AppleAuth.Enums;
using AppleAuth.Extensions;
using AppleAuth.Interfaces;
using PlayNANOO;
using System.Text;

public class AppleLogin : MonoBehaviour
{
    IAppleAuthManager _appleAuthManager;
    Plugin plugin;
    void Start()
    {
        plugin = Plugin.GetInstance();

        if (AppleAuthManager.IsCurrentPlatformSupported)
        {
            var deserializer = new PayloadDeserializer();
            _appleAuthManager = new AppleAuthManager(deserializer);
        }
    }

    private void Update()
    {
        _appleAuthManager?.Update();
    }

    public void SignIn()
    {
        var loginArgs = new AppleAuthLoginArgs();

        this._appleAuthManager.LoginWithAppleId(
            loginArgs,
            credential =>
            {
                var appleIdCredential = credential as IAppleIDCredential;
                if (appleIdCredential != null)
                {
                    string idToken = Encoding.UTF8.GetString(appleIdCredential.IdentityToken, 0, appleIdCredential.IdentityToken.Length);
                    plugin.AccountSocialSignIn(idToken, Configure.PN_ACCOUNT_APPLE_ID, (status, errorCode, jsonString, values) =>
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
            },
            error =>
            {
                // Something went wrong
                var authorizationErrorCode = error.GetAuthorizationErrorCode();
            }
            );
    }
}
#endif