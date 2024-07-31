using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.m3839.sdk.im.bean;

namespace com.m3839.sdk.im.listener
{
    public abstract class UnityIMUserStatusListener : AndroidJavaProxy
    {
        public UnityIMUserStatusListener() : base("com.m3839.sdk.im.unity.listener.UnityIMUserStatusListener") { }

        public void onSuccess(AndroidJavaObject value)
        {
            List<HykbIMUserStatus> dataList = new List<HykbIMUserStatus>();

            if (value != null)
            {
                int size = value.Call<int>("size"); //list
                for (int i = 0; i < size; i++)
                {
                    HykbIMUserStatus data = new HykbIMUserStatus(value.Call<AndroidJavaObject>("get", i));
                    dataList.Add(data);
                }
            }
            OnSuccess(dataList);
        }

        public void onError(int code, string message)
        {
            OnError(code, message);
        }


        public abstract void OnSuccess(List<HykbIMUserStatus> value);

        public abstract void OnError(int code, string message);
    }
}

