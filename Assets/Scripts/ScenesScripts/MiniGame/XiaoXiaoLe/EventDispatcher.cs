using UnityEngine;
using System.Collections.Generic;

public delegate void MyEventHandler(params object[] objs);

/// <summary>
/// 事件管理器，订阅事件与事件触发
/// </summary>
public class EventDispatcher
{
    /// <summary>
    /// 订阅事件
    /// </summary>
    public void Regist(string eventName, MyEventHandler handler)
    {
        if (handler == null)
            return;

        if (!listeners.ContainsKey(eventName))
        {
            listeners.Add(eventName, new Dictionary<int, MyEventHandler>());
        }
        var handlerDic = listeners[eventName];
        var handlerHash = handler.GetHashCode();
        if (handlerDic.ContainsKey(handlerHash))
        {
            handlerDic.Remove(handlerHash);
        }
        listeners[eventName].Add(handler.GetHashCode(), handler);
    }

    /// <summary>
    /// 注销事件
    /// </summary>
    public void UnRegist(string eventName, MyEventHandler handler)
    {
        if (handler == null)
            return;

        if (listeners.ContainsKey(eventName))
        {
            listeners[eventName].Remove(handler.GetHashCode());
            if (null == listeners[eventName] || 0 == listeners[eventName].Count)
            {
                listeners.Remove(eventName);
            }
        }
    }

    /// <summary>
    /// 触发事件
    /// </summary>
    public void DispatchEvent(string eventName, params object[] objs)
    {
        if (listeners.ContainsKey(eventName))
        {
            var handlerDic = listeners[eventName];
            if (handlerDic != null && 0 < handlerDic.Count)
            {
                var dic = new Dictionary<int, MyEventHandler>(handlerDic);
                foreach (var f in dic.Values)
                {
                    try
                    {
                        f(objs);
                    }
                    catch (System.Exception ex)
                    {
                        Debug.LogErrorFormat(szErrorMessage, eventName, ex.Message, ex.StackTrace);
                    }
                }
            }
        }
    }


    /// <summary>
    /// 清空事件
    /// </summary>
    /// <param name="key"></param>
    public void ClearEvents(string eventName)
    {
        if (listeners.ContainsKey(eventName))
        {
            listeners.Remove(eventName);
        }
    }

    private Dictionary<string, Dictionary<int, MyEventHandler>> listeners = new Dictionary<string, Dictionary<int, MyEventHandler>>();
    private readonly string szErrorMessage = "DispatchEvent Error, Event:{0}, Error:{1}, {2}";

    private static EventDispatcher s_instance;
    public static EventDispatcher instance
    {
        get
        {
            if (null == s_instance)
                s_instance = new EventDispatcher();
            return s_instance;
        }
    }
}
