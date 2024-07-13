using Common;
using Common.UI;
using GameManager;
using System.Threading.Tasks;
using UnityEngine;
namespace ScenesScripts.Lobby
{
    public class LobbyManager : MonoBehaviour
    {

        public async void Button_Click_StartRandomGames ()
        {
            var _loading_circle = new ShowLoading("ÕýÔÚµÈ´ý...");
            await Task.Delay(1000);
            var _name = GameCanvasManager.GameNames[GameAPI.GetRandomInAB(0, GameCanvasManager.GameNames.Count - 1)];
            Debug.Log(_name);

            var _loading = new LoadingSceneManager<string>(_name);
        }
    }

}
