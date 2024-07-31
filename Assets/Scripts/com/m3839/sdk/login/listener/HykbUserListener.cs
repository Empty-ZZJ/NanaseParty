using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 用户相关回调监听V2接口
/// create by chenbaoyang
/// </summary>
namespace com.m3839.sdk.login.listener
{
    public abstract class HykbUserListener : AndroidJavaProxy
    {
        /// <summary>
        /// 用户相关回回调监听
        /// </summary>
        public HykbUserListener() : base("com.m3839.sdk.login.listener.HykbUserListener") { }

        /// <summary>
        /// 登录成功
        /// </summary>
        /// <param name="user">用户信息</param>
        public void onLoginSucceed(AndroidJavaObject user)
        {
            OnLoginSucceed(new bean.HykbUser(user));
        }

        /// <summary>
        /// 登录失败
        /// </summary>
        /// <param name="code">返回code</param>
        /// <param name="message">失败原因</param>
        public void onLoginFailed(int code, string message)
        {
            OnLoginFailed(code, message);
        }

        /// <summary>
        /// 切换账号
        /// </summary>
        /// <param name="user">用户信息</param>
        public void onSwitchUser(AndroidJavaObject user)
        {
            OnSwitchUser(new bean.HykbUser(user));
        }

        /// <summary>
        /// 登录成功
        /// </summary>
        /// <param name="user">用户信息</param>
        public abstract void OnLoginSucceed(bean.HykbUser user);


        /// <summary>
        /// 登录失败
        /// </summary>
        /// <param name="code">返回code</param>
        /// <param name="message">失败原因</param>
        public abstract void OnLoginFailed(int code, string message);

        /// <summary>
        /// 切换账号
        /// </summary>
        /// <param name="user">用户信息</param>
        public abstract void OnSwitchUser(bean.HykbUser user);
    }
}

