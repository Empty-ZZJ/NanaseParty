using UnityEngine;

/// <summary>
/// 初始化回调监听接口
/// create by chenbaoyang
/// </summary>
namespace com.m3839.sdk.login.listener
{
    [System.Obsolete("该接口已过时")]
    public abstract class HykbInitListener : AndroidJavaProxy
    {
        /// <summary>
        /// 初始化回调监听，继承自SDK源码，为避免untiy使用冲突，另外提供给untiy继承的为首字母大写形式的接口。
        /// </summary>
        public HykbInitListener() : base("com.m3839.sdk.login.listener.HykbInitListener") { }

        /// <summary>
        /// SDK初始化成功，并根据用户登录情况返回用户信息
        /// </summary>
        /// <param name="user">有用户信息就返回用户信息，无则返回null</param>
        public void onInitSuccess(AndroidJavaObject user)
        {
            if (user == null)
            {
                OnInitSuccess(null);
            }
            else
            {
                OnInitSuccess(new bean.HykbUser(user));
            }
        }

        /// <summary>
        /// 初始化失败，并返回失败码及提示
        /// </summary>
        /// <param name="code">失败码</param>
        /// <param name="message">提示</param>
        public void onInitError(int code, string message)
        {
            OnInitError(code, message);
        }

        /// <summary>
        /// 初始化后有用户信息，但已失效且让玩家重新登录或切换账号
        /// </summary>
        /// <param name="switchUser">是否重新登录或切换账号</param>
        /// <param name="code">code码，SDK预留</param>
        /// <param name="user">有用户信息就返回用户信息，无则返回null</param>
        public void onSwitchUser(bool switchUser, int code, AndroidJavaObject user)
        {
            if (user == null)
            {
                OnSwitchUser(switchUser, code, null);
            }
            else
            {
                OnSwitchUser(switchUser, code, new bean.HykbUser(user));
            }
        }

        /// <summary>
        /// SDK初始化成功，并根据用户登录情况返回用户信息
        /// </summary>
        /// <param name="user">有用户信息就返回用户信息，无则返回null</param>
        public abstract void OnInitSuccess(bean.HykbUser user);

        /// <summary>
        /// 初始化失败，并返回失败码及提示
        /// </summary>
        /// <param name="code">失败码</param>
        /// <param name="message">提示</param>
        public abstract void OnInitError(int code, string message);

        /// <summary>
        /// 初始化后有用户信息，但已失效且让玩家重新登录或切换账号
        /// </summary>
        /// <param name="switchUser">是否重新登录或切换账号</param>
        /// <param name="code">code码，SDK预留</param>
        /// <param name="user">有用户信息就返回用户信息，无则返回null</param>
        public abstract void OnSwitchUser(bool switchUser, int code, bean.HykbUser user);
    }
}
