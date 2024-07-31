using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.m3839.sdk.im.listener
{

    public abstract class HykbIMCallback : AndroidJavaProxy
    {
        public HykbIMCallback() : base("com.m3839.sdk.im.listener.HykbIMCallback") { }


        public void onSuccess()
        {
            OnSuccess();
        }

        public void onError(int code, string message)
        {
            OnError(code, message);
        }

        public abstract void OnSuccess();

        public abstract void OnError(int code, string message);
    }
}