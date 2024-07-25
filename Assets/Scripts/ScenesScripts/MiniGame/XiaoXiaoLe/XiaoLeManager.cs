using Common;
using GameManager;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace ScenesScripts.MiniGame.XiaoXiaoLe
{
    public class XiaoLeManager : MonoBehaviour
    {
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
            Menhera_Img.sprite = Menhera_Medium[GameAPI.GetRandomInAB(0, Menhera_Medium.Count - 1)];

        }
    }


}
