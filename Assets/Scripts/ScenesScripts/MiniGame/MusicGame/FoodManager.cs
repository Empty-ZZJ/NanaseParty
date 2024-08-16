using Common;
using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ScenesScripts.MiniGame.MusicGame
{
    [RequireComponent(typeof(AudioSource))]
    public class FoodManager : MonoBehaviour
    {
        private static List<Sprite> FoodsIMG = new();
        private static GameObject BoomClickEffice;
        public Image FoodImgObj;

        public string Direction;
        private bool IsClick;
        private Tweener Event_AutoKill;
        private static RectTransform CanvasRect;
        private Vector3 ListCreate;

        private void Start ()
        {
            //这里给安可写会好很多，安可宝宝♥
            if (CanvasRect == null) CanvasRect = GameObject.Find("MainCanvas").GetComponent<RectTransform>();

            if (BoomClickEffice == null) BoomClickEffice = Resources.Load<GameObject>("GameObject/Scene/MiniGame/MusicGame/BoomClickEffice");
            if (FoodsIMG.Count == 0)
            {
                for (int i = 1; i <= 7; i++)
                {
                    FoodsIMG.Add(Resources.Load<Sprite>($"Texture2D/MusicGame/food{i}"));
                }
            }
            FoodImgObj.sprite = FoodsIMG[GameAPI.GetRandomInAB(0, FoodsIMG.Count - 1)];
            // 设置食物图片的RectTransform到屏幕的随机位置
            Event_AutoKill = this.FoodImgObj.DOColor(Color.white, 2f).OnComplete(() =>
            {
                //这个颜色变化没有用，只是为了0.72f销毁。
                Destroy(this.gameObject);
            });

        }
        public void Button_Click_Xiao ()
        {


            Event_AutoKill.Kill();
            var _obj = Instantiate(BoomClickEffice, this.transform.parent);
            _obj.transform.position = this.transform.position;
            MusicGameManager.Score += GameAPI.GetRandomInAB(100, 150);
            Destroy(this.gameObject);
        }
        public void Create (int direction)
        {
            if (CanvasRect == null) CanvasRect = GameObject.Find("MainCanvas").GetComponent<RectTransform>();
            Direction = direction == 1 ? "左侧" : "右侧";
            // 获取屏幕的宽度和高度

            float screenWidth = CanvasRect.rect.width - 200;
            float screenHeight = CanvasRect.rect.height - 200;

            float randomX; float randomY;
            if (direction == 1)
            {
                // 出现在左侧
                randomX = GameAPI.GetRandomInAB(-Convert.ToInt32(screenWidth / 2), 0);
                randomY = GameAPI.GetRandomInAB(-Convert.ToInt32(screenHeight / 2), Convert.ToInt32(screenHeight / 2));
            }
            else if (direction == 2)
            {
                // 出现在右侧
                randomX = GameAPI.GetRandomInAB(0, Convert.ToInt32(screenWidth / 2));
                randomY = GameAPI.GetRandomInAB(-Convert.ToInt32(screenHeight / 2), Convert.ToInt32(screenHeight / 2));
            }
            else
            {
                return;
            }
            if (randomX <= -670 || randomY >= 169)
            {
                Create(direction);
                return;
            }



            ListCreate = new Vector3(randomX, randomY, 0);
            //float objectCenterX = randomX - (this.gameObject.GetComponent<RectTransform>().rect.width / 2);
            // 设置FoodImgObj的RectTransform位置
            this.gameObject.GetComponent<RectTransform>().anchoredPosition3D = ListCreate;
        }




    }
}