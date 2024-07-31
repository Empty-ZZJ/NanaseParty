using UnityEngine;

/// <summary>
/// For support android context
/// create by chenbaoyang
/// </summary>
namespace com.m3839.sdk
{
    public sealed class HykbContext : MonoBehaviour
    {
        // 横屏
        public static int SCREEN_LANDSCAPE = 0;
        // 竖屏
        public static int SCREEN_PORTRAIT = 1;

        private AndroidJavaObject currentActivity;

        private static readonly HykbContext _HykbContext = new HykbContext();

       
        /// <summary>
        /// 获取当前实例 
        /// </summary>
        /// <returns></returns>
        public static HykbContext GetInstance()
        {
            return _HykbContext;
        }


        /*
         * 获取当前Activity       
         */
        public AndroidJavaObject GetActivity()
        {
            if (null == currentActivity)
            {
                currentActivity = new AndroidJavaClass("com.unity3d.player.UnityPlayer")
                .GetStatic<AndroidJavaObject>("currentActivity");
            }

            return currentActivity;
        }

        /*
         * 运行在主UI线程       
         */
        public void RunOnUIThread(AndroidJavaRunnable runnable)
        {
            GetActivity().Call("runOnUiThread", runnable);
        }


        /*
         * 获取根节点的布局 
         */
        public AndroidJavaObject GetRootLayout()
        {
            AndroidJavaClass R = new AndroidJavaClass("android.R$id");
            return currentActivity.Call<AndroidJavaObject>("findViewById", R.GetStatic<int>("content"));
        }
    }
}

