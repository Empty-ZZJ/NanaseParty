using GameManager;
using UnityEngine;
namespace ScenesScripts.Lobby
{
    public class GameCanvasManager : MonoBehaviour
    {

        private void Start ()
        {

        }

        public void Button_Click_OpenGame (string sceneName)
        {
            var _ = new LoadingSceneManager<string>(sceneName);
        }

    }

}
