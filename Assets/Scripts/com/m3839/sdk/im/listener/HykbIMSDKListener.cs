using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace com.m3839.sdk.im.listener
{

    public abstract class HykbIMSDKListener : AndroidJavaProxy
    {

        public HykbIMSDKListener() :  base("com.m3839.sdk.im.listener.HykbIMSDKListener") { }


        public void onKickedOffline()
        {
            OnKickedOffline();
        }

        public void onUserSigExpired()
        {
            OnUserSigExpired();
        }


        public abstract void OnKickedOffline();

        public abstract void OnUserSigExpired();
    }
}
