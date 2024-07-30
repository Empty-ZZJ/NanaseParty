using GameManager;
using System.Collections.Generic;
using UnityEngine;
namespace ScenesScripts.Lobby
{
    public class GameCanvasManager : MonoBehaviour
    {
        public static List<string> GameNames = new()
        {
            "FocusClock" ,
            "XiaoXiaoLe",
            "DuiGaoGao",
            "MusicGame",
            "Jump"
        };
        private void Start ()
        {

        }

        public void Button_Click_OpenGame (string sceneName)
        {
            var _ = new LoadingSceneManager<string>(sceneName);
        }
        public void Button_Click_Close ()
        {
            Destroy(gameObject);
        }

    }

}
