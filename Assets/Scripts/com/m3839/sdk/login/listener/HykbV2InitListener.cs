using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 初始化回调监听V2接口
/// create by chenbaoyang
/// </summary>
namespace com.m3839.sdk.login.listener
{

    public abstract class HykbV2InitListener : AndroidJavaProxy
    {
        /// <summary>
        /// 初始化回调监听，继承自SDK源码，为避免untiy使用冲突，另外提供给untiy继承的为首字母大写形式的接口。
        /// </summary>
        public HykbV2InitListener() : base("com.m3839.sdk.login.listener.HykbV2InitListener") { }

        /// <summary>
        /// 初始化成功
        /// </summary>
        public void onSucceed()
        {
            OnSucceed();
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
        public abstract void OnSucceed();

        /// <summary>
        /// 初始化失败
        /// </summary>
        /// <param name="code">返回码</param>
        /// <param name="message"> 错误信息</param>
        public abstract void OnFailed(int code, string message);
    }

}

