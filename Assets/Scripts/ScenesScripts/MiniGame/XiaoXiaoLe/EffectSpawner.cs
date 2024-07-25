using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EffectSpawner : MonoBehaviour
{
    /// <summary>
    /// 水果消除特效预设
    /// </summary>
    public GameObject disappearEffectPrefab;

    /// <summary>
    /// 得分特效预设
    /// </summary>
    public GameObject scoreEffectPrefab;

    /// <summary>
    /// 特效对象池
    /// </summary>
    private Queue<GameObject> m_disappearEffectPool = new Queue<GameObject>();

    /// <summary>
    /// 得分效果对象池
    /// </summary>
    private Queue<TextMeshPro> m_scoreEffectPool = new Queue<TextMeshPro>();

    private Transform m_effectRoot;

    private void Awake()
    {
        m_effectRoot = transform;

        EventDispatcher.instance.Regist(EventDef.EVENT_FRUIT_DISAPPEAR, OnFruitDisappear);
    }

    private void OnDestroy()
    {
        EventDispatcher.instance.UnRegist(EventDef.EVENT_FRUIT_DISAPPEAR, OnFruitDisappear);
    }

    private void OnFruitDisappear(params object[] args)
    {
        var pos = (Vector3)args[0];
        ShowDisappearEffect(pos);
        // 先写死加10分
        ShowScoreEffect(pos, 10);
    }

    /// <summary>
    /// 显示水果消除特效
    /// </summary>
    /// <param name="pos">坐标</param>
    public void ShowDisappearEffect(Vector3 pos)
    {
        GameObject obj;
        if (m_disappearEffectPool.Count > 0)
            obj = m_disappearEffectPool.Dequeue();
        else
        {
            obj = Instantiate(disappearEffectPrefab);
            obj.transform.SetParent(m_effectRoot, false);
            // 监听动画帧事件
            var bhv = obj.GetComponent<AnimationEvent>();
            bhv.aniEventCb = (str) =>
            {
                if("finish" == str)
                {
                    // 动画播放结束，回收对象到对象池中
                    obj.SetActive(false);
                    m_disappearEffectPool.Enqueue(obj);
                }
            };
        }
        obj.SetActive(true);
        obj.transform.position = pos;
    }

    /// <summary>
    /// 显示得分特效
    /// </summary>
    public void ShowScoreEffect(Vector3 pos, int addScore)
    {
        TextMeshPro textMesh = null;
        if (m_scoreEffectPool.Count > 0)
            textMesh = m_scoreEffectPool.Dequeue();
        else
        {
            var obj = Instantiate(scoreEffectPrefab);
            obj.transform.SetParent(m_effectRoot, false);
            textMesh = obj.GetComponent<TextMeshPro>();
            var aniEvent = obj.GetComponent<AnimationEvent>();
            aniEvent.aniEventCb = (str) =>
            {
                if ("finish" == str)
                {
                    obj.SetActive(false);
                    m_scoreEffectPool.Enqueue(textMesh);
                }
            };
        }
        textMesh.gameObject.SetActive(true);
        textMesh.transform.position = pos;
        textMesh.text = addScore.ToString();
    }
}
