using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : MonoBehaviour
{
    /// <summary>
    /// 总得分Text
    /// </summary>
    public TextMeshProUGUI totalScoreText;
    /// <summary>
    /// home按钮
    /// </summary>
    public Button homeBtn;

    /// <summary>
    /// 总得分
    /// </summary>
    private int m_totalScore;

    private void Awake ()
    {


        // 注册加分事件
        EventDispatcher.instance.Regist(EventDef.EVENT_ADD_SCORE, OnAddScore);
    }

    private void OnDestroy ()
    {
        // 注销加分事件
        EventDispatcher.instance.UnRegist(EventDef.EVENT_ADD_SCORE, OnAddScore);
    }

    /// <summary>
    /// 加分事件
    /// </summary>
    private void OnAddScore (params object[] args)
    {
        // 更新总分显示
        m_totalScore += (int)args[0];
        totalScoreText.text = m_totalScore.ToString();
    }
}
