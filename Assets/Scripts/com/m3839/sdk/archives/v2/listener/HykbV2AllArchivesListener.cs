using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.m3839.sdk.archives.v2.listener
{
    public abstract class HykbV2AllArchivesListener : AndroidJavaProxy
    {
        public HykbV2AllArchivesListener() : base("com.m3839.sdk.archives.v2.listener.HykbV2AllArchivesListener") { }

        /// <summary>
        /// 成功回调
        /// </summary>
        /// <param name="dataBean">存档数据</param>
        public void onSuccess(AndroidJavaObject dataBean)
        {
            List<HykbV2GameArchives> dataList = new List<HykbV2GameArchives>();

            if (dataBean != null)
            {
                int size = dataBean.Call<int>("size"); //list
                for (int i = 0; i < size; i++)
                {
                    HykbV2GameArchives data = new HykbV2GameArchives(dataBean.Call<AndroidJavaObject>("get", i));
                    dataList.Add(data);
                }
            }
            OnSuccess(dataList);
        }

        /// <summary>
        /// 失败回调
        /// </summary>
        /// <param name="code">code码，SDK预留</param>
        /// <param name="message">失败的返回的信息</param>
        public void onFailed(int code, string message)
        {
            OnFailed(code, message);
        }

        /// <summary>
        /// 成功回调
        /// </summary>
        /// <param name="dataList">存档数据</param>
        public abstract void OnSuccess(List<HykbV2GameArchives> dataList);

        /// <summary>
        /// 失败回调
        /// </summary>
        /// <param name="code">code码，SDK预留</param>
        /// <param name="message">失败的返回的信息</param>
        public abstract void OnFailed(int code, string message);
    }



}

