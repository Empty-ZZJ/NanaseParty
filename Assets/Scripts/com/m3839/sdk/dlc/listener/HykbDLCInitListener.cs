using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.m3839.sdk.dlc.bean;


/// <summary>
/// 快爆DLC初始化回调接口
/// </summary>
namespace com.m3839.sdk.dlc.listener
{
    public abstract class HykbDLCInitListener : AndroidJavaProxy
    {

        public HykbDLCInitListener() : base("com.m3839.sdk.dlc.listener.HykbDLCInitListener") { }



        /// <summary>
        /// 初始化成功
        /// </summary>
        public void onSucceed(AndroidJavaObject initData)
        {
            OnSucceed(new HykbDLCInitData(initData));
        }

        /// <summary>
        /// 初始化失败
        /// </summary>
        /// <param name="code">返回码</param>
        /// <param name="message"> 错误信息</param>
        public void onFailed(int code, string message)
        {
            OnFailed(code, message);
        }

        /// <summary>
        /// 初始化成功
        /// </summary>
        public abstract void OnSucceed(HykbDLCInitData initData);

        /// <summary>
        /// 初始化失败
        /// </summary>
        /// <param name="code">返回码</param>
        /// <param name="message"> 错误信息</param>
        public abstract void OnFailed(int code, string message);


    }


}


