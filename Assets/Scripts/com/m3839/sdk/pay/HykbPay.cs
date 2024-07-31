using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.m3839.sdk.pay.bean;

/// <summary>
/// 好游快爆SDK
/// create by chenbaoyang
/// </summary>
namespace com.m3839.sdk.pay
{
    /// <summary>
    /// 好游快爆SDK的静态方法包装，用于untiy与安卓SDK对接及交互。
    /// </summary>
    public class HykbPay
    {
        // 单例中间层对象，方便获取
        private static AndroidJavaClass sPayJavaClass;
        public static AndroidJavaClass getPayClass()
        {
            if (sPayJavaClass == null)
            {
                sPayJavaClass = new AndroidJavaClass("com.m3839.sdk.pay.HykbPay");
            }
            return sPayJavaClass;
        }

        /// <summary>
        /// 调用支付
        /// </summary>
        /// <param name="payInfo">支付信息包装</param>
        /// <param name="listener">支付结果的回调监听</param>
        [System.Obsolete("该方法已过期")]
        public static void pay(bean.HykbPayInfo payInfo, listener.HykbPayListener listener)
        {
            getPayClass().CallStatic("pay", HykbContext.GetInstance().GetActivity(), HykbPayInfo.ToA(payInfo), listener);
        }

        /// <summary>
        /// 调用支付
        /// </summary>
        /// <param name="payInfo">支付信息包装</param>
        /// <param name="listener">支付结果的回调监听</param>
        public static void Pay(bean.HykbPayInfo payInfo, listener.HykbV2PayListener listener)
        {
            getPayClass().CallStatic("pay", HykbContext.GetInstance().GetActivity(), HykbPayInfo.ToA(payInfo), listener);
        }

        /// <summary>
        /// 商品发放上报
        /// </summary>
        /// <param name="cpOrderId">游戏的订单编号</param>
        /// <param name="money">充值金额</param>
        /// <param name="goodsName">商品名称</param>
        public static void reportNotifyGoods(string cpOrderId, string money, string goodsName)
        {
            getPayClass().CallStatic("reportNotifyGoods", cpOrderId, money, goodsName);
        }

        /// <summary>
        /// 回调通知
        /// </summary>
        /// <param name="cpOrderId">游戏的订单编号</param>
        public static void unityCallBackReport(string cpOrderId)
        {
            getPayClass().CallStatic("unityCallBackReport", cpOrderId);
        }

        /// <summary>
        /// 资源释放
        /// </summary>
        public static void ReleaseSDK()
        {
            getPayClass().CallStatic("releaseSDK");
        }

    }
}
