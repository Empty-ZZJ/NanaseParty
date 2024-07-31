using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 防沉迷相关回调监听V2接口
/// create by chenbaoyang
/// </summary>
namespace com.m3839.sdk.login.listener
{
    public abstract class HykbAntiListener : AndroidJavaProxy
    {
        /// <summary>
        /// 防沉迷相关回回调监听
        /// </summary>
        public HykbAntiListener() : base("com.m3839.sdk.login.listener.HykbAntiListener") { }

        /// <summary>
        /// 防沉迷相关回回调
        /// </summary>
        /// <param name="code">响应code</param>
        /// <param name="message">响应消息</param>
        public void onAnti(int code, string message)
        {
            OnAnti(code, message);
        }

        public abstract void OnAnti(int code, string message);
    }
}

