using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.m3839.sdk.im.bean
{
    public class HykbIMUserStatus 
    {
        public static int V2TIM_USER_STATUS_UNKNOWN = 0;
        public static int V2TIM_USER_STATUS_ONLINE = 1;
        public static int V2TIM_USER_STATUS_OFFLINE = 2;
        public static int V2TIM_USER_STATUS_UNLOGINED = 3;

        private AndroidJavaObject userObject;

     
        public HykbIMUserStatus(AndroidJavaObject user)
        {
            this.userObject = user;
        }

        public string GetUserId()
        {
            return userObject.Call<string>("getUserID");
        }

        public string GetStatusType()
        {
            return userObject.Call<string>("getStatusType");
        }

        public string GetCustomStatus()
        {
            return userObject.Call<string>("getCustomStatus");
        }
    }
}
