using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.m3839.sdk.im.bean;

namespace com.m3839.sdk.im.listener
{

    public abstract class UnityIMUserListListener : AndroidJavaProxy
    {
        public UnityIMUserListListener() : base("com.m3839.sdk.im.unity.listener.UnityIMUserListListener") { }

        public void onSuccess(AndroidJavaObject value)
        {
            List<HykbIMUserInfo> dataList = new List<HykbIMUserInfo>();

            if (value != null)
            {
                int size = value.Call<int>("size"); //list
                for (int i = 0; i < size; i++)
                {
                    HykbIMUserInfo data = new HykbIMUserInfo(value.Call<AndroidJavaObject>("get", i));
                    dataList.Add(data);
                }
            }
            OnSuccess(dataList);
        }

        public void onError(int code, string message)
        {
            OnError(code, message);
        }


        public abstract void OnSuccess(List<HykbIMUserInfo> value);

        public abstract void OnError(int code, string message);

    }
}
