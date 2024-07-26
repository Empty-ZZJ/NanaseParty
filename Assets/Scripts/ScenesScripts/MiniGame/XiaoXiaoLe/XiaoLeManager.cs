using Common;
using DG.Tweening;
using GameManager;
using System;
using System.Collections.Generic;
using TetraCreations.Attributes;
using UnityEngine;
using UnityEngine.UI;
namespace ScenesScripts.MiniGame.XiaoXiaoLe
{
    public class XiaoLeManager : MonoBehaviour
    {
        [Title("得分面板")]
        public GameObject GameBoard;

        [Title("开始游戏面板")]
        public GameObject GameStarttingBoard;
        public static bool IsGaming;
        public Image Menhera_Img;
        private List<Sprite> Menhera_Medium = new();
        private void Awake ()
        {
            for (int i = 1; i <= 5; i++)
            {
                var a_ = Resources.Load<Sprite>($"Texture2D/Menhera/Medium/medium{i}");
                Menhera_Medium.Add(a_);
            }

        }
        public void Button_Click_Close ()
        {
            var _ = new LoadingSceneManager<string>("Game-Lobby");

        }
        public void ChangeMenhera ()
        {
            if (!IsGaming) return;
            Menhera_Img.sprite = Menhera_Medium[GameAPI.GetRandomInAB(0, Menhera_Medium.Count - 1)];

        }
        /// <summary>
        /// 无尽模式
        /// </summary>
        /// <returns>返回开始面板动画回调</returns>
        public void StartGame_Infinity ()
        {
            Debug.Log("无尽模式");
            GameStarttingBoard.GetComponent<RectTransform>().DOAnchorPos3D(new Vector3(0f, -1000f, 0f), 0.5f).OnComplete(() =>
           {
               GameBoard.SetActive(true);
               var _ = Instantiate(Resources.Load<GameObject>("GameObject/Scene/MiniGame/XiaoXiaoLe/Play"));
               _.name = "Play";
               GamePanel.IntiTime = DateTime.Now;
               IsGaming = true;
           });

        }
        /// <summary>
        /// 限时模式
        /// </summary>
        public void StartGame_Limit ()
        {
            Debug.Log("限时模式");
            StartGame_Infinity();
            Invoke(nameof(Call_Limit), 60f);
        }

        [Button(nameof(Call_Limit), "限时模式回调")]
        public void Call_Limit ()
        {
            try
            {
                Destroy(GameObject.Find("Play"));
                GameStarttingBoard.GetComponent<RectTransform>().DOAnchorPos3D(new Vector3(0f, 0f, 0f), 0.5f).OnComplete(() =>
                {
                    GameBoard.SetActive(false);
                    PopupManager.PopMessage("游戏结束", $"您的得分为：{GamePanel.m_totalScore}");
                    GamePanel.m_totalScore = 0;
                    IsGaming = false;
                });
            }
            catch (Exception ex)
            {
                PopupManager.PopMessage("错误", $"错误信息：{ex.Message}");
                Debug.LogError(ex.Message);
            }

        }
    }

}
