using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using com.m3839.sdk.pay.bean;
using com.m3839.sdk.dlc.bean;
using com.m3839.sdk.dlc.listener;

/// <summary>
/// 好游快爆DLC
/// </summary>
namespace com.m3839.sdk.dlc
{

    public class HykbDLC
    {
        // 单例中间层对象，方便获取
        private static AndroidJavaClass sDLCJavaClass;
        public static AndroidJavaClass getDLCClass()
        {
            if (sDLCJavaClass == null)
            {
                sDLCJavaClass = new AndroidJavaClass("com.m3839.sdk.dlc.HykbDLC");
            }
            return sDLCJavaClass;
        }


        /// <summary>
        /// 调用初始化
        /// </summary>
        /// <param name="listener">支付结果的回调监听</param>
        public static void init(string gameId,string key, HykbDLCInitListener listener)
        {
            getDLCClass().CallStatic("init", HykbContext.GetInstance().GetActivity(), gameId, key, listener);
        }


        /// <summary>
        /// 调用购买商品
        /// </summary>
        /// <param name="listener"></param>
        public static void purchase(int skuId, string goodsName, HykbDLCPurchaseListener listener)
        {
            getDLCClass().CallStatic("purchase", HykbContext.GetInstance().GetActivity(), skuId, goodsName, listener);
        }

        /// <summary>
        /// 调用查询购买商品
        /// </summary>
        /// <param name="listener"></param>
        public static void query(int skuId, HykbDLCQueryListener listener)
        {
            getDLCClass().CallStatic("query", HykbContext.GetInstance().GetActivity(), skuId, listener);
        }


        /// <summary>
        /// 资源释放
        /// </summary>
        public static void releaseSDK()
        {
            getDLCClass().CallStatic("releaseSDK");
        }

    }
}

