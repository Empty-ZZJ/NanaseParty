using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using com.m3839.sdk.pay.bean;

/// <summary>
/// 快爆DLC购买商品回调接口
/// </summary>
namespace com.m3839.sdk.dlc.listener
{
    public abstract class HykbDLCPurchaseListener : AndroidJavaProxy
    {

        public HykbDLCPurchaseListener() : base("com.m3839.sdk.dlc.listener.HykbDLCPurchaseListener") { }



        /// <summary>
        /// 购买商品成功
        /// </summary>
        public void onSucceed(AndroidJavaObject payInfo)
        {
            OnSucceed(new HykbPayInfo(payInfo));
        }

        /// <summary>
        /// 购买商品失败
        /// </summary>
        /// <param name="code">返回码</param>
        /// <param name="message"> 错误信息</param>
        public void onFailed(AndroidJavaObject payInfo, int code, string message)
        {
            HykbPayInfo info = null;
            if (payInfo != null) {
                info = new HykbPayInfo(payInfo);
            }
            OnFailed(info, code, message);
        }

        /// <summary>
        /// 购买商品成功
        /// </summary>
        public abstract void OnSucceed(HykbPayInfo payInfo);

        /// <summary>
        /// 购买商品失败
        /// </summary>
        /// <param name="code">返回码</param>
        /// <param name="message"> 错误信息</param>
        public abstract void OnFailed(HykbPayInfo payInfo, int code, string message);


    }


}

