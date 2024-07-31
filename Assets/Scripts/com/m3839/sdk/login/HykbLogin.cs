using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 好游快爆SDK
/// create by chenbaoyang
/// </summary>
namespace com.m3839.sdk.login
{
    /// <summary>
    /// 好游快爆SDK的静态方法包装，用于untiy与安卓SDK对接及交互。
    /// </summary>
    public class HykbLogin
    {
        
        // 单例中间层对象，方便获取
        private static AndroidJavaClass sLoginJavaClass;
        public static AndroidJavaClass getLoginClass()
        {
            if (sLoginJavaClass == null)
            {
                sLoginJavaClass = new AndroidJavaClass("com.m3839.sdk.login.HykbLogin");
            }
            return sLoginJavaClass;
        }

        /// <summary>
        /// SDK初始化
        /// </summary>
        /// <param name="appKey">快爆开发者后台申请的appKey</param>
        /// <param name="listener">初始化的回调监听</param>
        [System.Obsolete("该方法已过时")]
        public static void InitSdk(string appKey, int orientation, listener.HykbInitListener listener)
        {
            getLoginClass().CallStatic("initSdk", HykbContext.GetInstance().GetActivity(), appKey, orientation, listener);
        }

        /// <summary>
        /// SDK初始化
        /// </summary>
        /// <param name="appKey">快爆开发者后台申请的appKey</param>
        /// <param name="listener">初始化的回调监听</param>
        public static void Init(string appKey, int orientation, listener.HykbV2InitListener listener)
        {
            getLoginClass().CallStatic("init", HykbContext.GetInstance().GetActivity(), appKey, orientation, listener);
        }

        /// <summary>
        /// 设置用户相关监听
        /// </summary>
        /// <param name="listener">用户相关的监听接口</param>
        public static void SetUserListener(listener.HykbUserListener listener)
        {
            getLoginClass().CallStatic("setUserListener", listener);
        }

        /// <summary>
        /// 设置防沉迷相关监听
        /// </summary>
        /// <param name="listener">防沉迷相关的监听接口</param>
        public static void SetAntiListener(listener.HykbAntiListener listener)
        {
            getLoginClass().CallStatic("setAntiListener", listener);
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="listener">登录操作的回调监听</param>
        [System.Obsolete("该方法已过时")]
        public static void Login(listener.HykbLoginListener listener)
        {
            getLoginClass().CallStatic("login", HykbContext.GetInstance().GetActivity(), listener);
        }

        /// <summary>
        /// 登录
        /// </summary>
        public static void Login()
        {
            getLoginClass().CallStatic("login", HykbContext.GetInstance().GetActivity());
        }

        /// <summary>
        /// 切换账号
        /// </summary>
        /// <param name="listener">切换账号的回调监听</param>
        [System.Obsolete("该方法已过时")]
        public static void SwitchAccount(listener.HykbLoginListener listener)
        {
            getLoginClass().CallStatic("switchAccount", HykbContext.GetInstance().GetActivity(), listener);
        }

        /// <summary>
        /// 切换账号
        /// </summary>
        public static void SwitchAccount()
        {
            getLoginClass().CallStatic("switchAccount", HykbContext.GetInstance().GetActivity());
        }

        /// <summary>
        /// 获得当前用户信息
        /// </summary>
        /// <returns>有用户信息是返回用户信息HykbUser实例，无则返回null</returns>
        public static bean.HykbUser GetUser()
        {
            AndroidJavaObject userObject = getLoginClass().CallStatic<AndroidJavaObject>("getUser");
            return userObject == null ? null : new bean.HykbUser(userObject);
        }

        /// <summary>
        /// 退出登录，注销用户的登录信息
        /// </summary>
        public static void Logout()
        {
            getLoginClass().CallStatic("logout", HykbContext.GetInstance().GetActivity());
        }

    }


}
