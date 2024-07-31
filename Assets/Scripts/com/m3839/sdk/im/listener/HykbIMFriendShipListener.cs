using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.m3839.sdk.im.bean;

namespace com.m3839.sdk.im.listener
{
    public abstract class HykbIMFriendShipListener : AndroidJavaProxy
    {
        public HykbIMFriendShipListener() : base("com.m3839.sdk.im.listener.HykbIMFriendShipListener") { }

        public void onFriendApplicationListAdded(AndroidJavaObject applicationList)
        {
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
            OnFriendApplicationListAdded(dataList);
        }

        public void onFriendApplicationListDeleted(AndroidJavaObject userIDList)
        {
            List<string> dataList = new List<string>();

            if (userIDList != null)
            {
                int size = userIDList.Call<int>("size"); //list
                for (int i = 0; i < size; i++)
                {
                    dataList.Add(userIDList.Call<string>("get", i));
                }
            }
            OnFriendApplicationListDeleted(dataList);
        }

        public void onFriendListAdded(AndroidJavaObject users)
        {
            List<HykbIMFriendInfo> dataList = new List<HykbIMFriendInfo>();

            if (users != null)
            {
                int size = users.Call<int>("size"); //list
                for (int i = 0; i < size; i++)
                {
                    HykbIMFriendInfo data = new HykbIMFriendInfo(users.Call<AndroidJavaObject>("get", i));
                    dataList.Add(data);
                }
            }
            OnFriendListAdded(dataList);
        }

        public void onFriendListDeleted(AndroidJavaObject userList)
        {
            List<string> dataList = new List<string>();

            if (userList != null)
            {
                int size = userList.Call<int>("size"); //list
                for (int i = 0; i < size; i++)
                {
                    dataList.Add(userList.Call<string>("get", i));
                }
            }
            OnFriendListDeleted(dataList);
        }


        public abstract void OnFriendApplicationListAdded(List<HykbIMFriendApplication> applicationList);

        public abstract void OnFriendApplicationListDeleted(List<string> userIDList);

        public abstract void OnFriendListAdded(List<HykbIMFriendInfo> users);

        public abstract void OnFriendListDeleted(List<string> userList);

    }
}