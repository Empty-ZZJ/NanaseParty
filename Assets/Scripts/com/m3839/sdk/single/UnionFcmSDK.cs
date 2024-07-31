using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.m3839.sdk;


/// <summary>
/// 快爆单机防沉迷
/// create by chenbaoyang
/// </summary>
namespace com.m3839.sdk.single 
{
    public class UnionFcmSDK 
    {

        static AndroidJavaClass sdkClass = new AndroidJavaClass("com.m3839.sdk.single.UnionFcmSDK");

        [System.Obsolete("该方法已过期")]
        public static void InitSDK(string gameId, int orientation, UnionFcmListener listener)
        {
            AndroidJavaObject paramBuilder = new AndroidJavaObject("com.m3839.sdk.single.UnionFcmParam$Builder");
            AndroidJavaObject param = paramBuilder.Call<AndroidJavaObject>("setGameId", gameId)
                        .Call<AndroidJavaObject>("setOrientation", orientation)
                        .Call<AndroidJavaObject>("build");
            sdkClass.CallStatic("initSDK", HykbContext.GetInstance().GetActivity(), param, listener);
        }

        public static void Init(string gameId, int orientation, UnionV2FcmListener listener)
        {
            AndroidJavaObject paramBuilder = new AndroidJavaObject("com.m3839.sdk.single.UnionFcmParam$Builder");
            AndroidJavaObject param = paramBuilder.Call<AndroidJavaObject>("setGameId", gameId)
                        .Call<AndroidJavaObject>("setOrientation", orientation)
                        .Call<AndroidJavaObject>("build");
            sdkClass.CallStatic("init", HykbContext.GetInstance().GetActivity(), param, listener);
        }

        public static UnionFcmUser GetUser()
        {
            return new UnionFcmUser(sdkClass.CallStatic<AndroidJavaObject>("getUser"));
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

