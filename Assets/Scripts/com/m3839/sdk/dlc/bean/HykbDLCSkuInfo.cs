using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// DLC商品实体类
/// </summary>
namespace com.m3839.sdk.dlc.bean
{
    public class HykbDLCSkuInfo
    {
        private int skuId;
        private string price;
        private string linePrice;


        /// <summary>
        /// 商品信息封装
        /// </summary>
        public HykbDLCSkuInfo(AndroidJavaObject skuInfo)
        {
            this.skuId = skuInfo.Call<int>("getSkuId");
            this.price = skuInfo.Call<string>("getPrice");
            this.linePrice = skuInfo.Call<string>("getLinePrice");
        }

        /// <summary>
        /// 获取商品id
        /// </summary>
        /// <returns>商品id</returns>
        public int getSkuId()
        {
            return skuId;
        }

        /// <summary>
        /// 获取价格
        /// </summary>
        /// <returns>类型字符串</returns>
        public string getPrice()
        {
            return price;
        }

        /// <summary>
        /// 获取原价
        /// </summary>
        /// <returns>类型字符串</returns>
        public string getLinePrice()
        {
            return linePrice;
        }

        public string toString()
        {
            return "HykbDLCSkuInfo{" +
                      "skuId='" + skuId + '\'' +
                      ", price='" + price + '\'' +
                      ", linePrice='" + linePrice + '\'' +
                      '}';
        }

    }

}