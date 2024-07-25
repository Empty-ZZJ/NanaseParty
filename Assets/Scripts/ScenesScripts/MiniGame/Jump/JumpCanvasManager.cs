using GameManager;
using UnityEngine;

namespace ScenesScripts.MiniGame.JumpGame

{
    public class JumpCanvasManager : MonoBehaviour
    {
        public void Button_Click_Quit ()
        {
            Destroy(this.gameObject);
            var _ = new LoadingSceneManager<string>("Game-Lobby");
        }
    }
}