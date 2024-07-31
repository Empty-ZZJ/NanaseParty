using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace com.m3839.sdk.im.listener
{
    public abstract class HykbIMInitListener : AndroidJavaProxy
    {
        /// <summary>
        /// 初始化回调监听，继承自SDK源码，为避免untiy使用冲突，另外提供给untiy继承的为首字母大写形式的接口。
        /// </summary>
        public HykbIMInitListener() : base("com.m3839.sdk.im.listener.HykbIMInitListener") { }

        /// <summary>
        /// 初始化成功
        /// </summary>
        public void onSuccess()
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

