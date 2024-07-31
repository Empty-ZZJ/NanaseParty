using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.m3839.sdk.im.bean
{
    public class HykbIMFriendOperationResult : MonoBehaviour
    {
        private AndroidJavaObject userObject;

        public HykbIMFriendOperationResult(AndroidJavaObject user)
        {
            this.userObject = user;
        }

        public string GetUserId()
        {
            return userObject.Call<string>("getUserId");
        }

        public string GetCode()
        {
            return userObject.Call<string>("getCode");
        }

        public string GetMessagee()
        {
            return userObject.Call<string>("getMessage");
        }
    }

}
