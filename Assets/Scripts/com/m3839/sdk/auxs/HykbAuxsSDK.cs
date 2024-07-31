using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.m3839.sdk.auxs.bean;

/// <summary>
/// 强更sdk的 API
/// create by chenbaoyang
/// </summary>
namespace com.m3839.sdk.auxs
{
    public class HykbAuxsSDK
    {
        static AndroidJavaClass sdkClass = new AndroidJavaClass("com.m3839.sdk.auxs.HykbAuxSDK");

        /// <summary>
        /// SDK初始化
        /// </summary>
        /// <param name="gameId">快爆游戏ID</param>
        /// <param name="orientation">屏幕方向</param>
        /// <param name="listener">回调监听</param>
        [System.Obsolete("该方法已过时")]
        public static void initSdk(string gameId, int orientation, listener.HykbAuxInitListener listener)
        {
            sdkClass.CallStatic("init", HykbContext.GetInstance().GetActivity(), gameId, orientation, listener);
        }

        /// <summary>
        /// SDK初始化
        /// </summary>
        /// <param name="gameId">快爆游戏ID</param>
        /// <param name="orientation">屏幕方向</param>
        /// <param name="listener">回调监听</param>
        public static void Init(string gameId, int orientation, listener.HykbV2AuxInitListener listener)
        {
            sdkClass.CallStatic("init", HykbContext.GetInstance().GetActivity(), gameId, orientation, listener);
        }

        /// <summary>
        /// 礼包码（自带UI界面）
        /// </summary>
        /// <param name="device">唯一标识符</param>
        /// <param name="listener">校验礼包码的回调</param>
        [System.Obsolete("该方法已过时")]
        public static void checkGiftCode(string device, listener.HykbAuxGiftListener listener)
        {
            sdkClass.CallStatic("checkGiftCode", device, listener);
        }

        /// <summary>
        /// 礼包码（自带UI界面）
        /// </summary>
        /// <param name="device">唯一标识符</param>
        /// <param name="listener">校验礼包码的回调</param>
        public static void CheckGiftCode(string device, listener.HykbV2AuxGiftListener listener)
        {
            sdkClass.CallStatic("checkGiftCode", device, listener);
        }

        /// <summary>
        /// 礼包码（不带UI界面，该接口给开发自定义UI界面使用）
        /// </summary>
        /// <param name="device">唯一标识符</param>
        /// <param name="code">礼包码</param>
        /// <param name="listener">校验礼包码的回调</param>
        [System.Obsolete("该方法已过时")]
        public static void checkGiftCode(string device, string code, listener.HykbAuxGiftListener listener)
        {
            sdkClass.CallStatic("checkGiftCode", device, code, listener);
        }

        /// <summary>
        /// 礼包码（不带UI界面，该接口给开发自定义UI界面使用）
        /// </summary>
        /// <param name="device">唯一标识符</param>
        /// <param name="code">礼包码</param>
        /// <param name="listener">校验礼包码的回调</param>
        public static void CheckGiftCode(string device, string code, listener.HykbV2AuxGiftListener listener)
        {
            sdkClass.CallStatic("checkGiftCode", device, code, listener);
        }


        /// <summary>
        /// 激活码（自带UI界面）
        /// </summary>
        /// <param name="device">唯一标识符</param>
        /// <param name="listener">校验激活码的回调</param>
        [System.Obsolete("该方法已过时")]
        public static void checkActivationCode(string device, listener.HykbAuxActivationListener listener)
        {
            sdkClass.CallStatic("checkActivationCode", device, listener);
        }

        /// <summary>
        /// 激活码（自带UI界面）
        /// </summary>
        /// <param name="device">唯一标识符</param>
        /// <param name="listener">校验激活码的回调</param>
        public static void CheckActivationCode(string device, listener.HykbV2AuxActivationListener listener)
        {
            sdkClass.CallStatic("checkActivationCode", device, listener);
        }

        /// <summary>
        /// 激活码（不带UI界面，该接口给开发自定义UI界面使用）
        /// </summary>
        /// <param name="device">唯一标识符</param>
        /// <param name="code">激活码</param>
        /// <param name="listener">校验激活码的回调</param>
        [System.Obsolete("该方法已过时")]
        public static void checkActivationCode(string device, string code, listener.HykbAuxActivationListener listener)
        {
            sdkClass.CallStatic("checkActivationCode", device, code, listener);
        }

        /// <summary>
        /// 激活码（不带UI界面，该接口给开发自定义UI界面使用）
        /// </summary>
        /// <param name="device">唯一标识符</param>
        /// <param name="code">激活码</param>
        /// <param name="listener">校验激活码的回调</param>
        public static void CheckActivationCode(string device, string code, listener.HykbV2AuxActivationListener listener)
        {
            sdkClass.CallStatic("checkActivationCode", device, code, listener);
        }


        /// <summary>
        /// 激活码的UI文案数据信息
        /// </summary>
        /// <returns>返回激活码的UI文案数据实例</returns>
        public static HykbActivationUiInfo getActivationUiInfo()
        {
            AndroidJavaObject activationObject = sdkClass.CallStatic<AndroidJavaObject>("getActivationUiInfo");
            return activationObject == null ? null : new HykbActivationUiInfo(activationObject);
        }

        /// <summary>
        /// 礼包码的UI文案数据信息
        /// </summary>
        /// <returns>返回礼包码码的UI文案数据实例</returns>
        public static HykbGiftUiInfo getGiftUiInfo()
        {
            AndroidJavaObject activationObject = sdkClass.CallStatic<AndroidJavaObject>("getGiftUiInfo");
            return activationObject == null ? null : new HykbGiftUiInfo(activationObject);
        }

        /// <summary>
        /// 检测该设备id是否激活过
        /// </summary>
        /// <param name="device">唯一标识符</param>
        /// <param name="listener">校验激活码是否激活的回调</param>
        public static void getActivationStatusByDevice(string device, listener.HykbAuxActivationCheckListener listener)
        {
            sdkClass.CallStatic("getActivationStatusByDevice", device, listener);
        }

        /// <summary>
        /// 页面跳转
        /// </summary>
        /// <param name="biz">业务id</param>
        public static void openPageDetail(int biz)
        {
            sdkClass.CallStatic("openPageDetail",biz);
        }

        /// <summary>
        /// 业务弹窗
        /// </summary>
        /// <param name="biz">业务id</param>
        public static void openBizDialog(int biz)
        {
            sdkClass.CallStatic("openBizDialog", HykbContext.GetInstance().GetActivity(), biz);
        }

        /// <summary>
        /// 评论弹窗
        /// </summary>
        public static void openCommentDialog()
        {
            sdkClass.CallStatic("openCommentDialog", HykbContext.GetInstance().GetActivity());
        }


        /// <summary>
        /// 一键加qq群
        /// </summary>
        public static void joinQQGroup()
        {
            sdkClass.CallStatic("joinQQGroup");
        }
    }
}
