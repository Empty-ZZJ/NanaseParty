using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.m3839.sdk.im.bean
{
    public class HykbIMFriendInfo 
    {
        private AndroidJavaObject infoObject;

        public HykbIMFriendInfo(AndroidJavaObject javaObject)
        {
            infoObject = javaObject;
        }

        public string GetUserId()
        {
            return infoObject.Call<string>("getUserID");
        }

        public string GetFaceUrl()
        {
            return infoObject.Call<AndroidJavaObject>("getUserProfile").Call<string>("getFaceUrl");
        }

        public string GetNickName()
        {
            return infoObject.Call<AndroidJavaObject>("getUserProfile").Call<string>("getNickName");
        }

    }
}
