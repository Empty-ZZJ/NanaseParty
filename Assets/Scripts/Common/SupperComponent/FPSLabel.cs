using UnityEngine;
using UnityEngine.UI;

namespace Common.SupperComponent
{
    public class FPSLabel : Text
    {
        /// <summary>
        /// 上一次更新帧率的时间
        /// </summary>
        private float m_lastUpdateShowTime = 0f;
        /// <summary>
        /// 更新显示帧率的时间间隔
        /// </summary>
        private readonly float m_updateTime = 0.05f;
        /// <summary>
        /// 帧数
        /// </summary>
        private int m_frames = 0;
        /// <summary>
        /// 帧间间隔
        /// </summary>
        private float m_frameDeltaTime = 0;
        private float m_FPS = 0;
        private Rect m_fps, m_dtime;
        private GUIStyle m_style = new GUIStyle();
        private float m_lastFrameTime = 0f;
        private float m_lowestFrameTime = Mathf.Infinity;
        private float m_averageFrameTime = 0f;
        private int m_frameCount = 0;


        protected override void Start ()
        {
            base.Start();
            m_lastUpdateShowTime = Time.realtimeSinceStartup;
            m_fps = new Rect(0, 0, 100, 100);
            m_dtime = new Rect(0, 100, 100, 100);
            m_style.fontSize = 100;
            m_style.normal.textColor = Color.red;
        }

        private void Update ()
        {
            m_frames++;
            if (Time.realtimeSinceStartup - m_lastUpdateShowTime >= m_updateTime)
            {
                m_FPS = m_frames / (Time.realtimeSinceStartup - m_lastUpdateShowTime);
                m_frameDeltaTime = (Time.realtimeSinceStartup - m_lastUpdateShowTime) / m_frames;
                m_frames = 0;
                m_lastUpdateShowTime = Time.realtimeSinceStartup;


                float currentTime = Time.realtimeSinceStartup;
                float frameTime = currentTime - m_lastFrameTime;
                m_lastFrameTime = currentTime;

                m_lowestFrameTime = Mathf.Min(m_lowestFrameTime, frameTime);
                m_averageFrameTime = (m_averageFrameTime * m_frameCount + frameTime) / (m_frameCount + 1);
                m_frameCount++;
                this.text = "FPS: " + m_FPS + " 间隔: " + m_frameDeltaTime + " 最低帧时间: " + m_lowestFrameTime + "\n" + " 平均帧时间: " + m_averageFrameTime + " 设备名称" + SystemInfo.deviceName + " 显卡名称：" + SystemInfo.graphicsDeviceName;

            }
        }
    }
}