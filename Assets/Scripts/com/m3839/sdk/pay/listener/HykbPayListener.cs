using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 快爆支付回调接口
/// create by chenbaoyang
/// </summary>
namespace com.m3839.sdk.pay.listener
{
    public abstract class HykbPayListener : AndroidJavaProxy
    {
        /// <summary>
        /// 支付回调监听，继承自SDK源码，为避免untiy使用冲突，另外提供给untiy继承的为首字母大写形式的接口。
        /// </summary>
        public HykbPayListener() : base("com.m3839.sdk.pay.listener.HykbPayListener") { }

        /// <summary>
        /// 支付结果
        /// </summary>
        /// <param name="code">支付结果标识码</param>
        /// <param name="message">提示</param>
        /// <param name="payInfo">支付原始数据</param>
        public void onPayResult(int code, string message, AndroidJavaObject payInfo)
        {
            bean.HykbPayInfo hykbPayInfo = new bean.HykbPayInfo(payInfo);
            HykbPay.unityCallBackReport(hykbPayInfo.cpOrderId);
            OnPayResult(code, message, hykbPayInfo);
        }

        /// <summary>
        /// 支付结果
        /// </summary>
        /// <param name="code">支付结果标识码</param>
        /// <param name="message">提示</param>
        /// <param name="payInfo">支付原始数据</param>
        public abstract void OnPayResult(int code, string message, bean.HykbPayInfo payInfo);
    }
}