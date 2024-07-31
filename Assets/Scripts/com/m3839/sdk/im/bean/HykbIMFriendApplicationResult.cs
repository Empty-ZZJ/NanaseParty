using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.m3839.sdk.im.bean
{
    public class HykbIMFriendApplicationResult
    {
        private AndroidJavaObject javaObject;

        public HykbIMFriendApplicationResult(AndroidJavaObject javaObject)
        {
            this.javaObject = javaObject;
        }

        public List<HykbIMFriendApplication> GetHykbIMFriendApplicationList()
        {

            AndroidJavaObject applicationList = javaObject.Call<AndroidJavaObject>("getHykbIMFriendApplicationList");
            List<HykbIMFriendApplication> dataList = new List<HykbIMFriendApplication>();

            if (applicationList != null)
            {
                int size = applicationList.Call<int>("size"); //list
                for (int i = 0; i < size; i++)
                {
                    HykbIMFriendApplication data = new HykbIMFriendApplication(applicationList.Call<AndroidJavaObject>("get", i));
                    dataList.Add(data);
                }
            }
            return dataList;
        }


    }
}

