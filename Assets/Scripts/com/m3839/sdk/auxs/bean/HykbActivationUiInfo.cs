using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 激活码UI的数据信息实体类
/// create by chenbaoyang
/// </summary>
namespace com.m3839.sdk.auxs.bean
{
    public class HykbActivationUiInfo
    {
        private string activationTitle;
        private string activationLink;
        private string activationKbLink;
        private string activationLinkTitle;
        private int activationLinkStatus;
        /// <summary>
        /// 激活码UI显示数据
        /// </summary>
        public HykbActivationUiInfo(AndroidJavaObject user)
        {
            this.activationTitle = user.Call<string>("getActivationTitle");
            this.activationLink = user.Call<string>("getActivationLink");
            this.activationKbLink = user.Call<string>("getActivationKbLink");
            this.activationLinkTitle = user.Call<string>("getActivationLinkTitle");
            this.activationLinkStatus = user.Call<int>("getActivationLinkStatus");
        }

        /// <summary>
        /// 获得激活码UI标题
        /// </summary>
        /// <returns>获取礼包UI标题</returns>
        public string getActivationTitle()
        {
            return activationTitle;
        }

        /// <summary>
        /// 获得激活码弹窗链接
        /// </summary>
        /// <returns>激活码弹窗的链接</returns>
        public string getActivationLink()
        {
            return activationLink;
        }

        /// <summary>
        /// 获得激活码弹窗快爆app链接
        /// </summary>
        /// <returns>激活码弹窗快爆app链接</returns>
        public string getActivationKbLink()
        {
            return activationKbLink;
        }

        /// <summary>
        /// 获得激活码弹窗快爆链接
        /// </summary>
        /// <returns>激活码弹窗的链接文案</returns>
        public string getActivationLinkTitle()
        {
            return activationLinkTitle;
        }


        /// <summary>
        /// 链接地址状态：1、显示；0、关闭
        /// </summary>
        /// <returns>链接地址状态</returns>
        public int getActivationLinkStatus()
        {
            return activationLinkStatus;
        }
    }
}