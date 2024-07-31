using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 礼包码回调监听接口
/// create by chenbaoyang
/// </summary>
namespace com.m3839.sdk.auxs.listener
{
    [System.Obsolete("该接口已过时")]
    public abstract class HykbAuxGiftListener : AndroidJavaProxy
    {
        /// <summary>
        /// 礼包码回调接口
        /// </summary>
        public HykbAuxGiftListener() : base("com.m3839.sdk.auxs.listener.HykbAuxGiftListener") { }

        /// <summary>
        /// 校验结果回调
        /// </summary>
        /// <param name="code">code码，SDK预留</param>
        /// <param name="message">礼包码，校验通过之后，返回的信息</param>
        /// <param name="codeName">兑换成功，返回的礼包名称</param>
        public void onResult(int code, string message, string codeName)
        {
            OnResult(code, message, codeName);
        }

        /// <summary>
        /// 校验接口回调
        /// </summary>
        /// <param name="code">code码，SDK预留</param>
        /// <param name="message">礼包码，校验通过之后，返回的信息</param>
        /// <param name="codeName">兑换成功，返回的礼包名称</param>
        public abstract void OnResult(int code, string message, string codeName);
    }
}
