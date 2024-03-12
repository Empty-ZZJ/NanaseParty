using Common;
using Common.Network;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using UnityEngine;
namespace ScenesScripts
{
    public class GameMainMenuMananger : MonoBehaviour
    {
        private Transform ThisCanvas;
        private void Start ()
        {
            ThisCanvas = this.gameObject.GetComponent<Transform>();
            Invoke(nameof(CheckHotUpdate), 2f);
        }

        private async void CheckHotUpdate ()
        {
            //先检查更新

            var _obj_hot = Instantiate(Resources.Load<GameObject>("GameObject/Scene/InitGame/HotUpdate"), ThisCanvas).GetComponent<HotUpdateControler>();
            _obj_hot.Slider.value = 0;
            _obj_hot.Text_Info.text = "正在检查更新....";
            var _res = JsonConvert.DeserializeObject<JObject>(await NetworkHelp.GetAsync($"{GameConst.API_URL}/Game/NanaseParty/GameInfo/GetGameInfo"));

            if (!_res["Ver"].ToString().Equals(Application.version))
            {
                PopupManager.PopMessage("检测到更新", $"更新版本号：{_res["Ver"]}", () => { Application.OpenURL("https://www.3839.com/a/157221.htm"); });

                return;
            }

            _obj_hot.Slider.value = 100;
            Destroy(_obj_hot.gameObject);
            await Task.Delay(500);
            var _obj_game = Instantiate(Resources.Load<GameObject>("GameObject/Scene/InitGame/StartGame"), ThisCanvas).GetComponent<HotUpdateControler>();

        }
    }
}
