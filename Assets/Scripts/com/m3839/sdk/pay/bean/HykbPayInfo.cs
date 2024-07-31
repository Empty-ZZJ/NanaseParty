using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 快爆支付信息实体类
/// create by chenbaoyang
/// </summary>
namespace com.m3839.sdk.pay.bean
{
    public class HykbPayInfo
    {
        public string goodsName;
        public int money;
        public int server;
        public string cpOrderId;
        public string ext;
        /// <summary>
        /// 支付信息包装
        /// </summary>
        /// <param name="goodsName">商品信息</param>
        /// <param name="money">下单金额，单位是元</param>
        /// <param name="server">区服标识</param>
        /// <param name="cpOrderId">游戏的订单编号</param>
        /// <param name="ext">透传字段</param>
        public HykbPayInfo(string goodsName, int money, int server, string cpOrderId, string ext)
        {
            this.goodsName = goodsName;
            this.money = money;
            this.server = server;
            this.cpOrderId = cpOrderId;
            this.ext = ext;

            
        }


        /// <summary>
        /// 支付信息包装
        /// </summary>
        /// <param name="payInfo">支付信java对象</param>
        public HykbPayInfo(AndroidJavaObject payInfo)
        {
            this.goodsName = payInfo.Get<string>("goodsName");
            this.money = payInfo.Get<int>("money");
            this.server = payInfo.Get<int>("server");
            this.cpOrderId = payInfo.Get<string>("cpOrderId");
            this.ext = payInfo.Get<string>("ext");
        }

        public static AndroidJavaObject ToA(HykbPayInfo info)
        {
            AndroidJavaObject payInfo = new AndroidJavaObject("com.m3839.sdk.pay.bean.HykbPayInfo");
            payInfo.Set<string>("cpOrderId", info.cpOrderId);
            payInfo.Set<string>("goodsName", info.goodsName);
            payInfo.Set<int>("money", info.money);
            payInfo.Set<int>("server", info.server);
            payInfo.Set<string>("ext", info.ext);

            return payInfo;
        }
    }
}
