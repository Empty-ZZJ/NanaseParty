using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 快爆单机防沉迷用户信息实体类
/// create by chenbaoyang
/// </summary>
namespace com.m3839.sdk.single {

    public class UnionFcmUser
    {
        private string userId;
        private string nick;
        private string type;
        private string accessToken;

        /// <summary>
        /// 用户信息包装
        /// </summary>
        public UnionFcmUser(AndroidJavaObject user)
        {
            this.userId = user.Call<string>("getUserId");
            this.nick = user.Call<string>("getNick");
            this.type = user.Call<string>("getType");
            this.accessToken = user.Call<string>("getAccessToken");
        }

        /// <summary>
        /// 获得用户ID
        /// </summary>
        /// <returns>用户编号字符串</returns>
        public string getUserId()
        {
            return userId;
        }

        /// <summary>
        /// 获得昵称
        /// </summary>
        /// <returns>昵称字符串</returns>
        public string getNick()
        {
            return nick;
        }

        /// <summary>
        /// 获得登录类型
        /// </summary>
        /// <returns>类型字符串</returns>
        public string getType()
        {
            return type;
        }

        /// <summary>
        /// 用来校验登录授权过程真实性，accessToken有效期两小时
        /// </summary>
        /// <returns>类型字符串</returns>
        public string getAccessToken()
        {
            return accessToken;
        }

        public string toString()
        {
            return "UnionFcmUser{" +
                    "userId='" + userId + '\'' +
                    ", nick='" + nick + '\'' +
                    ", type='" + type + '\'' +
                    ", accessToken='" + accessToken + '\'' +
                    '}';
        }

    }


}


