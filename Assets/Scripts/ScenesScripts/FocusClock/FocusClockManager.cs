using System;
using System.Collections;
using TetraCreations.Attributes;
using UnityEngine;
using UnityEngine.UI;
namespace ScenesScripts.FocusClock
{
    public class FocusClockManager : MonoBehaviour
    {
        /// <summary>
        /// 时钟的切片
        /// </summary>
        [Title("时钟的切片")]
        public Image ClockPoint;
        /// <summary>
        /// 胡桃的切片
        /// </summary>
        [Title("胡桃的切片")]
        public Animator MenheraAnimate;
        /// <summary>
        /// 时钟显示数字
        /// </summary>
        [Title("时钟显示数字")]
        public Text ClockText;

        [Title("倒计时")]
        public static int GameTime = 0;

        [Title("系统信息标签")]
        public Text Text_SystemInfo;

        [Title("开始按钮")]
        public Button Button_Start;

        public static class TimeInof
        {
            public static int minutes;
            public static int seconds;
            public static bool IsDoing = false;
        }
        private static Coroutine TimeCoroutine;

        /// <summary>
        /// 点击 开始 按钮 回调
        /// </summary>
        public void Button_Click_Game ()
        {
            Debug.Log($"当前状态:{TimeInof.IsDoing}");

            if (!TimeInof.IsDoing)//暂停或没有开始
            {
                var _obj = Resources.Load<GameObject>("GameObject/Scene/FocusClock/SelectTime");
                Instantiate(_obj, GameObject.Find("Canvas").transform);
                return;
            }

            TimeInof.IsDoing = false;
            Button_Start.GetComponentInChildren<Text>().text = "开始";
            StopCoroutine(TimeCoroutine);

        }
        /// <summary>
        /// 启动
        /// </summary>
        public void StartClock ()
        {
            TimeCoroutine = StartCoroutine(ChangeTime(TimeInof.minutes * 60 + TimeInof.seconds));
            MenheraAnimate.Play("studying");
            TimeInof.IsDoing = true;
            Button_Start.GetComponentInChildren<Text>().text = "取消";

        }

        private IEnumerator ChangeTime (int time)
        {
            float timer = 0;
            GameTime = time;
            while (GameTime > 0)
            {
                ClockPoint.fillAmount = GameTime / 3600f;
                int M = (int)(GameTime / 60);
                float S = GameTime % 60;
                timer += Time.deltaTime;
                if (timer >= 1f)
                {
                    timer = 0;
                    GameTime--;
                    ClockText.text = M + ":" + string.Format("{00:00}", S);
                }
                yield return null;
            }
            TimeInof.IsDoing = false;
            //over

        }
        public void Update ()
        {
            Text_SystemInfo.text = $"电量：{SystemInfo.batteryLevel * 100} %      时间：{DateTime.Now:T}";

        }

    }

}
