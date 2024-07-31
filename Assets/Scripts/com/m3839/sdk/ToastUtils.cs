using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.m3839.sdk
{
    public class ToastUtils
    {
        public static void showToast(string text)
        {
            HykbContext.GetInstance().RunOnUIThread(new AndroidJavaRunnable(() =>
            {
                AndroidJavaClass Toast = new AndroidJavaClass("android.widget.Toast");
                Toast.CallStatic<AndroidJavaObject>("makeText", HykbContext.GetInstance().GetActivity(), text, Toast.GetStatic<int>("LENGTH_SHORT")).Call("show");
            }));
        }
    }
}

