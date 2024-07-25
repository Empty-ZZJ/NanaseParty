
using UnityEngine;
using System;

public class AnimationEvent : MonoBehaviour
{
    /// <summary>
    /// 委托
    /// </summary>
    public Action<string> aniEventCb;

    /// <summary>
    /// 动画帧事件响应函数
    /// </summary>
    public void OnAnimationEvent(string str)
    {
        // 调用委托
        if (null != aniEventCb)
            aniEventCb(str);
    }
}
