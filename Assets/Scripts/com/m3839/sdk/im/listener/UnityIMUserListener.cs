using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.m3839.sdk.im.bean;

namespace com.m3839.sdk.im.listener
{
    public abstract class UnityIMUserListener : AndroidJavaProxy
    {
        public UnityIMUserListener() : base("com.m3839.sdk.im.unity.listener.UnityIMUserListener") { }

        public void onSuccess(AndroidJavaObject value)
        {
            OnSuccess(new HykbIMUserInfo(value));
        }

        public void onError(int code, string message)
        {
            OnError(code, message);
        }


        public abstract void OnSuccess(HykbIMUserInfo value);

        public abstract void OnError(int code, string message);
    }
}

