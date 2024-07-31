using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.m3839.sdk.im.bean;


namespace com.m3839.sdk.im.listener
{
    public abstract class UnityIMFriendApplicationResultListener : AndroidJavaProxy
    {
        public UnityIMFriendApplicationResultListener() : base("com.m3839.sdk.im.unity.listener.UnityIMFriendApplicationResultListener") { }

        public void onSuccess(AndroidJavaObject value)
        {
            OnSuccess(new HykbIMFriendApplicationResult(value));
        }

        public void onError(int code, string message)
        {
            OnError(code, message);
        }


        public abstract void OnSuccess(HykbIMFriendApplicationResult value);

        public abstract void OnError(int code, string message);
    }
}


