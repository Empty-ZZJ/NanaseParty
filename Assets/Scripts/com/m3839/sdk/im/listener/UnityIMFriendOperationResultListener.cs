using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.m3839.sdk.im.bean;



namespace com.m3839.sdk.im.listener
{
    public abstract class UnityIMFriendOperationResultListener : AndroidJavaProxy
    {
        public UnityIMFriendOperationResultListener() : base("com.m3839.sdk.im.unity.listener.UnityIMFriendOperationResultListener") { }

        public void onSuccess(AndroidJavaObject value)
        {
            OnSuccess(new HykbIMFriendOperationResult(value));
        }

        public void onError(int code, string message)
        {
            OnError(code, message);
        }


        public abstract void OnSuccess(HykbIMFriendOperationResult value);

        public abstract void OnError(int code, string message);
    }
}

