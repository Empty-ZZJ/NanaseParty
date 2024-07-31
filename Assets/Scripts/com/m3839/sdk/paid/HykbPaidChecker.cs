using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 付费下载校验监听接口
/// create by chenbaoyang
/// </summary>
namespace com.m3839.sdk.paid
{
    public class HykbPaidChecker 
    {
        static AndroidJavaClass sdkClass = new AndroidJavaClass("com.m3839.sdk.paid.HykbPaidChecker");

        public static void CheckLicense(string appId, string publicKey, HykbCheckListener listener)
        {
            sdkClass.CallStatic("checkLicense", HykbContext.GetInstance().GetActivity(), appId, publicKey, listener);
        }

        public static void SetDebug(bool isDebug)
        {
            sdkClass.CallStatic("setDebug", isDebug);
        }

        public static void ReleaseSDK()
        {
            sdkClass.CallStatic("ReleaseSDK");
        }
    }
}

