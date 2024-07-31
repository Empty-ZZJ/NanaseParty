using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 付费下载校验监听接口
/// create by chenbaoyang
/// </summary>
namespace com.m3839.sdk.paid
{
    public abstract class HykbCheckListener : AndroidJavaProxy
    {
        /// <summary>
        /// 快爆单机防沉迷回调接口
        /// </summary>
        public HykbCheckListener() : base("com.m3839.sdk.paid.HykbCheckListener") { }



        void onAllowEnter()
        {
	OnAllowEnter();
        }

        /// <summary>
        /// 校验不通过回调
        /// <param name="code">code码，SDK预留</param>
        /// <param name="message">返回的信息</param>
        void onReject(int code, string errorMsg)
        {
	        OnReject(code, errorMsg);
        }


        /// <summary>
        /// 校验成功回调
        /// </summary>
        public abstract void OnAllowEnter();

        /// <summary>
        /// 校验失败回调
        /// </summary>
        /// <param name="code">code码，SDK预留</param>
        /// <param name="message">失败的返回的信息</param>
        public abstract void OnReject(int code, string message);


    }
}

