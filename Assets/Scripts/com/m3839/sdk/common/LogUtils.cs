using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.m3839.sdk{

    public class LogUtils
    {

        public static AndroidJavaClass sLogJavaClass = new AndroidJavaClass("android.util.Log");

        public static void info(string tag, string msg)
        {
            sLogJavaClass.CallStatic<int>("i", tag, msg);
        }

        public static void debug(string tag, string msg)
        {
            sLogJavaClass.CallStatic<int>("d", tag, msg);
        }
    }
}
