using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 快爆DLC初始化实体类
/// </summary>
namespace com.m3839.sdk.dlc.bean
{
    public class HykbDLCInitData
    {
        // 商品列表
        private List<HykbDLCSkuInfo> list = new List<HykbDLCSkuInfo>();

        /// <summary>
        /// 商品列表包装
        /// </summary>
        /// <param name="payInfo">DLC商品列表对象</param>
        public HykbDLCInitData(AndroidJavaObject data) {

            AndroidJavaObject listObject = data.Call<AndroidJavaObject>("getList");
            int size = listObject.Call<int>("size");
            for(int i = 0; i<size; i++) 
            {
                HykbDLCSkuInfo info = new HykbDLCSkuInfo(listObject.Call<AndroidJavaObject>("get", i));
                list.Add(info);
            }
        }

        public List<HykbDLCSkuInfo> getList() 
        {
            return list;
        }







    }
}

