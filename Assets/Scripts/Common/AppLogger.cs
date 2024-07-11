using System;
using System.IO;
using System.Text;
using UnityEngine;

public static class AppLogger
{
    private static object Lock_log;
    public static void Log (string msg, string tag_type = "msg")
    {

        string logPath = Application.persistentDataPath + "/log";
        if (!Directory.Exists(logPath)) Directory.CreateDirectory(logPath);
        string lin_msg = "[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss-ffff") + "] " + tag_type + " : " + msg + "\r\n";
        lock (Lock_log)
        {
            File.AppendAllText(logPath + $"/{DateTime.Now:yyyy-MM-dd}.txt", lin_msg, Encoding.UTF8);
        }
    }
}