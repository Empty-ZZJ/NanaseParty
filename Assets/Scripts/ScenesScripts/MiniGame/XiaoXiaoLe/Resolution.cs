using UnityEngine;

/// <summary>
/// 分辨率适配
/// </summary>
public class Resolution : MonoBehaviour
{

    public float BASE_WIDTH = 720f;
    public float BASE_HEIGHT = 1280f;

    private Transform m_tranform;
    private float baseRatio;
    private float percentScale;

    void Start ()
    {
        m_tranform = transform;
        SetScale();
    }

    void SetScale ()
    {
        // 根据宽高比进行缩放
        baseRatio = BASE_WIDTH / BASE_HEIGHT * Screen.height;
        percentScale = Screen.width / baseRatio;
        if (percentScale < 1)
            m_tranform.localScale = new Vector3(m_tranform.localScale.x * percentScale, m_tranform.localScale.y * percentScale, 1);
    }
}
