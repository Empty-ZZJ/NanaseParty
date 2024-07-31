using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 登录回调监听接口
/// create by chenbaoyang
/// </summary>
namespace com.m3839.sdk.login.listener
{
    [System.Obsolete("该接口已过时")]
    public abstract class HykbLoginListener : AndroidJavaProxy
    {
        /// <summary>
        /// 登录或者切换账号回调监听，继承自SDK源码，为避免untiy使用冲突，另外提供给untiy继承的为首字母大写形式的接口。
        /// </summary>
        public HykbLoginListener() : base("com.m3839.sdk.login.listener.HykbLoginListener") { }

        /// <summary>
        /// 登录操作结束
        /// </summary>
        /// <param name="login">是否有登录</param>
        /// <param name="code">code码，SDK预留</param>
        /// <param name="user">有用户信息就返回用户信息，无则返回null</param>
        public void onLoginFinished(bool login, int code, AndroidJavaObject user)
        {
            if (user == null)
            {
                OnLoginFinished(login, code, null);
            }
            else
            {
                OnLoginFinished(login, code, new bean.HykbUser(user));
            }
        }


        /// <summary>
        /// 登录操作结束
        /// </summary>
        /// <param name="login">是否有登录</param>
        /// <param name="code">code码，SDK预留</param>
        /// <param name="user">有用户信息就返回用户信息，无则返回null</param>
        public abstract void OnLoginFinished(bool login, int code, bean.HykbUser user);
    }
}
