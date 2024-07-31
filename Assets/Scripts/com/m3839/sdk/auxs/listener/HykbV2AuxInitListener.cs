using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 初始化回调监听V2接口
/// create by chenbaoyang
/// </summary>
namespace com.m3839.sdk.auxs.listener
{
    public abstract class HykbV2AuxInitListener : AndroidJavaProxy
    {

        /// <summary>
        /// SDK进行初始化的回调监听
        /// </summary>
        public HykbV2AuxInitListener() : base("com.m3839.sdk.auxs.listener.HykbV2AuxInitListener") { }

        /// <summary>
        /// 初始化流程结束
        /// </summary>
        public void onSucceed()
        {
            OnSucceed();
        }

        /// <summary>
        ///初始化失败
        /// </summary>
        /// <param name="code">code码，SDK预留</param>
        /// <param name="message">初始化结果，或者激活码的相关信息返回</param>
        public void onFailed(int code, string message)
        {
            OnFailed(code, message);
        }

        /// <summary>
        /// 初始化流程结束
        /// </summary>
        public abstract void OnSucceed();

        /// <summary>
        ///初始化失败
        /// </summary>
        /// <param name="code">code码，SDK预留</param>
        /// <param name="message">初始化结果，或者激活码的相关信息返回</param>
        public abstract void OnFailed(int code, string message);
    }
}

