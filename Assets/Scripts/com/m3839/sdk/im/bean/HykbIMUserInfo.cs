using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace com.m3839.sdk.im.bean {

    public class HykbIMUserInfo 
    {
        private AndroidJavaObject userObject;

        /// <summary>
        /// IM用户信息包装
        /// </summary>
        public HykbIMUserInfo(AndroidJavaObject user)
        {
            this.userObject = user;
        }

        public string GetUserId()
        {
            return userObject.Call<string>("getUserID");
        }

        public string GetFaceUrl()
        {
            return userObject.Call<string>("getFaceUrl");
        }

        public string GetNickName()
        {
            return userObject.Call<string>("getNickName");
        }

    }
}


