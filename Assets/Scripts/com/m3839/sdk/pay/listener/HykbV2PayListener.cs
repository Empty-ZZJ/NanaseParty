using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 快爆支付回调V2接口
/// create by chenbaoyang
/// </summary>
namespace com.m3839.sdk.pay.listener
{
    public abstract class HykbV2PayListener : AndroidJavaProxy
    {
        /// <summary>
        /// 支付回调监听，继承自SDK源码，为避免untiy使用冲突，另外提供给untiy继承的为首字母大写形式的接口。
        /// </summary>
        public HykbV2PayListener() : base("com.m3839.sdk.pay.listener.HykbV2PayListener") { }

        /// <summary>
        /// 充值成功
        /// </summary>
        /// <param name="payInfo">订单信息</param>
        public void onSucceed(AndroidJavaObject payInfo)
        {
            bean.HykbPayInfo hykbPayInfo = new bean.HykbPayInfo(payInfo);
            HykbPay.unityCallBackReport(hykbPayInfo.cpOrderId);
            OnSucceed(hykbPayInfo);
        }

     
        /// <summary>
        /// 充值失败
        /// </summary>
        /// <param name="payInfo">订单信息</param>
        /// <param name="code">失败code</param>
        /// <param name="message">失败原因</param>
        public void onFailed(AndroidJavaObject payInfo, int code, string message)
        {
            bean.HykbPayInfo hykbPayInfo = new bean.HykbPayInfo(payInfo);
            OnFailed(hykbPayInfo, code, message);
        }


        /// <summary>
        /// 充值成功
        /// </summary>
        /// <param name="payInfo">订单信息</param>
        public abstract void OnSucceed(bean.HykbPayInfo payInfo);


        /// <summary>
        /// 充值失败
        /// </summary>
        /// <param name="payInfo">订单信息</param>
        /// <param name="code">失败code</param>
        /// <param name="message">失败原因</param>
        public abstract void OnFailed(bean.HykbPayInfo payInfo, int code, string message);


    }
}

