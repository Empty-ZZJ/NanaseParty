using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 快爆单机防沉迷回调接口
/// create by chenbaoyang
/// </summary>
namespace com.m3839.sdk.single
{
    public abstract class UnionFcmListener : AndroidJavaProxy
    {
        /// <summary>
        /// 快爆单机防沉迷回调接口
        /// </summary>
        public UnionFcmListener() : base("com.m3839.sdk.single.UnionFcmListener") { }

        /// <summary>
        /// 回调
        /// <param name="code">code码，SDK预留</param>
        /// <param name="message">返回的信息</param>
        public void onFcm(int code, string message)
        {
            OnFcm(code, message);
        }

        /// <summary>
        /// 回调
        /// </summary>
        /// <param name="code">code码，SDK预留</param>
        /// <param name="message">返回的信息</param>
        public abstract void OnFcm(int code, string message);
    }
}

