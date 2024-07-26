using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using TetraCreations.Attributes;
using UnityEngine;
using UnityEngine.UI;
namespace ScenesScripts.MiniGame.XiaoXiaoLe
{
    public class GamePanel : MonoBehaviour
    {
        /// <summary>
        /// 总得分Text
        /// </summary>
        public Text totalScoreText;

        /// <summary>
        /// 总得分
        /// </summary>
        public static int m_totalScore;
        public static DateTime IntiTime;
        public class GraceItem
        {
            public int Score { get; set; }
            public string Name { get; set; }
        }
        private Queue<GraceItem> GraceQueue = new();

        private void GraceSystem ()
        {
            if (GraceQueue.Count == 0) return;
            var _grace = GraceQueue.First<GraceItem>();
            if (m_totalScore >= _grace.Score)
            {
                var _grace_item = GraceQueue.Dequeue();
                PopupManager.PopDynamicIsland("恭喜", _grace_item.Name);
            }
        }
        private void Awake ()
        {
            // 注册加分事件
            IntiTime = DateTime.Now;
            EventDispatcher.instance.Regist(EventDef.EVENT_ADD_SCORE, OnAddScore);
            GraceQueue.Enqueue(new GraceItem { Name = "1000分大关！", Score = 1000 });
            GraceQueue.Enqueue(new GraceItem { Name = "恭喜来到2000分！", Score = 2000 });
            GraceQueue.Enqueue(new GraceItem { Name = "已经达到一万的一半了！", Score = 5000 });
            GraceQueue.Enqueue(new GraceItem { Name = "7000分了，继续加油！", Score = 7000 });
            GraceQueue.Enqueue(new GraceItem { Name = "恭喜突破一万！继续努力吧！", Score = 10000 });



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

        }
        private void Update ()
        {
            if (!XiaoLeManager.IsGaming) return;
            var _time = DateTime.Now - IntiTime;
            totalScoreText.text = $"得分：{m_totalScore} \n时长：{_time.Minutes}分{_time.Seconds}秒";
            GraceSystem();


        }
        [Button(nameof(Add1000), "加1000分")]
        public void Add1000 ()
        {
            m_totalScore += 1000;
        }

    }
}