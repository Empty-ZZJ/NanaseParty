using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 激活码回调监听接口
/// create by chenbaoyang
/// </summary>
namespace com.m3839.sdk.auxs.listener
{
    public abstract class HykbV2AuxActivationListener : AndroidJavaProxy
    {

        /// <summary>
        /// 激活码校验回调接口
        /// </summary>
        public HykbV2AuxActivationListener() : base("com.m3839.sdk.auxs.listener.HykbV2AuxActivationListener") { }

        /// <summary>
        /// 激活成功
        /// </summary>
        public void onSucceed()
        {
            OnSucceed();
        }

        /// <summary>
        /// 激活失败
        /// </summary>
        /// <param name="code">返回码</param>
        /// <param name="message"> 错误信息</param>
        public void onFailed(int code, string message)
        {
            OnFailed(code, message);
        }

        /// <summary>
        /// 激活成功
        /// </summary>
        public abstract void OnSucceed();

        /// <summary>
        /// 激活失败
        /// </summary>
        /// <param name="code">返回码</param>
        /// <param name="message"> 错误信息</param>
        public abstract void OnFailed(int code, string message);
    }
}

