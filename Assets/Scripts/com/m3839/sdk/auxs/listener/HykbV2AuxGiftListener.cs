using UnityEngine;
using System.Collections;

/// <summary>
/// 礼包码回调监听接口
/// create by chenbaoyang
/// </summary>
namespace com.m3839.sdk.auxs.listener
{
    public abstract class HykbV2AuxGiftListener : AndroidJavaProxy
    {
        /// <summary>
        /// 礼包码回调接口
        /// </summary>
        public HykbV2AuxGiftListener() : base("com.m3839.sdk.auxs.listener.HykbV2AuxGiftListener") { }

        /// <summary>
        /// 激活成功
        /// </summary>
        /// <param name="giftName">礼包名称</param>
        public void onSucceed(string giftName)
        {
            OnSucceed(giftName);
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
        /// <param name="giftName">礼包名称</param>
        public abstract void OnSucceed(string giftName);

        /// <summary>
        /// 激活失败
        /// </summary>
        /// <param name="code">返回码</param>
        /// <param name="message"> 错误信息</param>
        public abstract void OnFailed(int code, string message);

    }
}

