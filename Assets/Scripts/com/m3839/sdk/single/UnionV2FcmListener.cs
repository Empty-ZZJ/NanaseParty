using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 快爆单机防沉迷回调V2接口
/// create by chenbaoyang
/// </summary>
namespace com.m3839.sdk.single
{
    public abstract class UnionV2FcmListener : AndroidJavaProxy
    {
        /// <summary>
        /// 快爆单机防沉迷回调接口
        /// </summary>
        public UnionV2FcmListener() : base("com.m3839.sdk.single.UnionV2FcmListener") { }


        /// <summary>
        /// 防尘流出结束，返回用户信息
        /// </summary>
        /// <param name="user">快爆用户信息</param>
        public void onSucceed(AndroidJavaObject user)
        {
            OnSucceed(new UnionFcmUser(user));
        }

        /// <summary>
        /// 防沉迷/其他的失败原因
        /// </summary>
        /// <param name="code">失败code</param>
        /// <param name="message">失败原因</param>
        public void onFailed(int code, string message)
        {
            OnFailed(code, message);
        }

        public abstract void OnSucceed(UnionFcmUser user);


        public abstract void OnFailed(int code, string message);
    }

}
