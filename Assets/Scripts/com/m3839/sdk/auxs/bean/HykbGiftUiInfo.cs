using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 礼包UI的数据信息实体类
/// create by chenbaoyang
/// </summary>
namespace com.m3839.sdk.auxs.bean
{
    public class HykbGiftUiInfo
    {
        private string giftTitle;
        private string giftLink;
        private string giftKbLink;
        private string giftLinkTitle;
        private int giftLinkStatus;

        /// <summary>
        /// 礼包码UI显示数据
        /// </summary>
        public HykbGiftUiInfo(AndroidJavaObject user)
        {
            this.giftTitle = user.Call<string>("getGiftTitle");
            this.giftLink = user.Call<string>("getGiftLink");
            this.giftKbLink = user.Call<string>("getGiftKbLink");
            this.giftLinkTitle = user.Call<string>("getGiftLinkTitle");
            this.giftLinkStatus = user.Call<int>("getGiftLinkStatus");
        }

        /// <summary>
        /// 礼包码UI标题
        /// </summary>
        /// <returns>用户编号字符串</returns>
        public string getGiftTitle()
        {
            return giftTitle;
        }

        /// <summary>
        /// 礼包码UI弹窗链接
        /// </summary>
        /// <returns>礼包码UI弹窗链接</returns>
        public string getGiftLink()
        {
            return giftLink;
        }

        /// <summary>
        /// 礼包码UI弹窗快爆app链接
        /// </summary>
        /// <returns>礼包码UI弹窗快爆app链接</returns>
        public string getGiftKbLink()
        {
            return giftKbLink;
        }

        /// <summary>
        /// 礼包码UI弹窗链接标题
        /// </summary>
        /// <returns>礼包码UI弹窗链接标题</returns>
        public string getGiftLinkTitle()
        {
            return giftLinkTitle;
        }

        /// <summary>
        /// 链接地址状态：1、显示；0、关闭
        /// </summary>
        /// <returns>链接地址状态</returns>
        public int getGiftLinkStatus()
        {
            return giftLinkStatus;
        }


    }

}
