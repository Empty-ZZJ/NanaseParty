using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 快爆DLC查询商品回调接口
/// </summary>
namespace com.m3839.sdk.dlc.listener
{
    public abstract class HykbDLCQueryListener : AndroidJavaProxy
    {

        public HykbDLCQueryListener() : base("com.m3839.sdk.dlc.listener.HykbDLCQueryListener") { }



        /// <summary>
        /// 查询商品成功
        /// <param name="ok"> 1已购买 0未购买</param>
        /// </summary>
        public void onSucceed(int ok)
        {
            OnSucceed(ok);
        }

        /// <summary>
        /// 查询商品失败
        /// </summary>
        /// <param name="code">返回码</param>
        /// <param name="message"> 错误信息</param>
        public void onFailed(int code, string message)
        {
            OnFailed(code, message);
        }

        /// <summary>
        /// 查询商品成功
        /// </summary>
        public abstract void OnSucceed(int ok);

        /// <summary>
        /// 查询商品失败
        /// </summary>
        /// <param name="code">返回码</param>
        /// <param name="message"> 错误信息</param>
        public abstract void OnFailed(int code, string message);


    }


}

