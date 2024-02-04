using Common;
using Common.Network;
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
            _obj_hot.Text_Info.text = "正在检查更新....";
            var _res = await NetworkHelp.GetAsync("https://api.nanasekurumi.top/Account/");
            _obj_hot.Slider.value = 10;
            PopupManager.PopMessage("连接超时", _res, () =>
            {

                //失败

                Destroy(_obj_hot.gameObject);
                StartGameControler.IsCanTouch = true;
                // Destroy();
            });

            var _obj_game = Instantiate(Resources.Load<GameObject>("GameObject/Scene/InitGame/StartGame"), ThisCanvas).GetComponent<HotUpdateControler>();

        }
    }
}
