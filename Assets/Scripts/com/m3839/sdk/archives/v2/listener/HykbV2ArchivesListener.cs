using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.m3839.sdk.archives.v2.listener
{
    public abstract class HykbV2ArchivesListener : AndroidJavaProxy
    {
        /// <summary>
        /// 云存档数据校验回调接口
        /// </summary>
        public HykbV2ArchivesListener() : base("com.m3839.sdk.archives.v2.listener.HykbV2ArchivesListener") { }

        /// <summary>
        /// 成功回调
        /// </summary>
        /// <param name="dataBean">存档数据</param>
        public void onSuccess(AndroidJavaObject dataBean)
        {
            OnSuccess(dataBean);
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
        /// <param name="dataBean">存档数据</param>
        public abstract void OnSuccess(AndroidJavaObject dataBean);

        /// <summary>
        /// 失败回调
        /// </summary>
        /// <param name="code">code码，SDK预留</param>
        /// <param name="message">失败的返回的信息</param>
        public abstract void OnFailed(int code, string message);
    }
}

