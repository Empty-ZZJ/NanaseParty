using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.m3839.sdk.im.bean
{
    public class HykbIMFriendApplication 
    {

        private AndroidJavaObject applicationObject;

        /// <summary>
        /// IM用户信息包装
        /// </summary>
        public HykbIMFriendApplication(AndroidJavaObject application)
        {
            this.applicationObject = application;
        }

        public string GetUserId()
        {
            return applicationObject.Call<string>("getUserID");
        }

        public string GetFaceUrl()
        {
            return applicationObject.Call<string>("getFaceUrl");
        }

        public string GetNickName()
        {
            return applicationObject.Call<string>("getNickname");
        }
         
        public string GetAddWording()
        {
            return applicationObject.Call<string>("getAddWording");
        }


        public AndroidJavaObject GetApplicationObject()
        {
            return applicationObject;
        }
    }
}
