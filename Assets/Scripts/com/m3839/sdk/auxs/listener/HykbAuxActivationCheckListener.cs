using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 激活码状态检测回调监听接口
/// create by chenbaoyang
/// </summary>
namespace com.m3839.sdk.auxs.listener
{
    public abstract class HykbAuxActivationCheckListener : AndroidJavaProxy
    {
        /// <summary>
        /// 激活码状态
        /// </summary>
        public HykbAuxActivationCheckListener() : base("com.m3839.sdk.auxs.listener.HykbAuxActivationCheckListener") { }

        /// <summary>
        /// 校验结果回调
        /// </summary>
        /// <param name="hasActivation">是否激活过</param>
        public void onActivationCheck(bool hasActivation)
        {
            OnActivationCheck(hasActivation);
        }

        /// <summary>
        /// 校验接口回调
        /// </summary>
        /// <param name="hasActivation">是否激活过</param>
        public abstract void OnActivationCheck(bool hasActivation);
    }
}
