using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 激活码回调监听接口
/// create by chenbaoyang
/// </summary>
namespace com.m3839.sdk.auxs.listener
{
    [System.Obsolete("该接口已过时")]
    public abstract class HykbAuxActivationListener : AndroidJavaProxy
    {
        /// <summary>
        /// 激活码校验回调接口
        /// </summary>
        public HykbAuxActivationListener() : base("com.m3839.sdk.auxs.listener.HykbAuxActivationListener") { }

        /// <summary>
        /// 校验结果回调
        /// </summary>
        /// <param name="code">code码，SDK预留</param>
        /// <param name="message">激活码，校验通过之后，返回的信息</param>
        public void onResult(int code, string message)
        {
            OnResult(code, message);
        }

        /// <summary>
        /// 校验接口回调
        /// </summary>
        /// <param name="code">code码，SDK预留</param>
        /// <param name="message">激活码，校验通过之后，返回的信息</param>
        public abstract void OnResult(int code, string message);
    }
}
