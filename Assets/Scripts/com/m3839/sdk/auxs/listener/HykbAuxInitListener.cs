using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 初始化回调监听接口
/// create by chenbaoyang
/// </summary>
namespace com.m3839.sdk.auxs.listener
{
    [System.Obsolete("该接口已过时")]
    public abstract class HykbAuxInitListener : AndroidJavaProxy
    {
        /// <summary>
        /// SDK进行初始化的回调监听
        /// </summary>
        public HykbAuxInitListener() : base("com.m3839.sdk.auxs.listener.HykbAuxInitListener") { }

        /// <summary>
        /// 登录操作结束
        /// </summary>
        /// <param name="code">code码，SDK预留</param>
        /// <param name="message">初始化结果，或者激活码的相关信息返回</param>
        public void onInitFinish(int code, string message)
        {
            OnInitFinished(code, message);
        }

        /// <summary>
        /// 登录操作结束
        /// </summary>
        /// <param name="code">code码，SDK预留</param>
        /// <param name="message">初始化结果，或者激活码的相关信息返回</param>
        public abstract void OnInitFinished(int code, string message);
    }
}
