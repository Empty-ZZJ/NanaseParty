using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.m3839.sdk.im.bean;

namespace com.m3839.sdk.im.listener
{

    public abstract class UnityIMFriendListListener : AndroidJavaProxy
    {
        public UnityIMFriendListListener() : base("com.m3839.sdk.im.unity.listener.UnityIMFriendListListener") { }

        public void onSuccess(AndroidJavaObject value)
        {
            List<HykbIMFriendInfo> dataList = new List<HykbIMFriendInfo>();
            Debug.Log("chenby onSuccess value = " + value);
            if (value != null)
            {
                int size = value.Call<int>("size"); //list
                Debug.Log("chenby onSuccess size = " + size);
                for (int i = 0; i < size; i++)
                {
                    HykbIMFriendInfo data = new HykbIMFriendInfo(value.Call<AndroidJavaObject>("get", i));
                    dataList.Add(data);
                }
            }
            OnSuccess(dataList);
        }

        public void onError(int code, string message)
        {
            OnError(code, message);
        }


        public abstract void OnSuccess(List<HykbIMFriendInfo> value);

        public abstract void OnError(int code, string message);

    }
}


