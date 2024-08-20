using Common;
using Common.Network;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ScenesScripts
{
    public class HotUpdateControler : MonoBehaviour
    {
        public Slider Slider;
        public Text Text_Info;
        public Text Text_GameVer;
        public GameObject Popup;
        public Text UpdateContent;
        public GameObject View_Content;
        private async void Start ()
        {
            //  PopupManager.PopMessage("提示", "本版本为测试包，仅供花濑内部测试使用，测试包体号：。可能存在内容不完整的情况，请联系工号：00001A。");
            Slider.value = 0;
            UpdateContent.text = "正在检查更新....";
            try
            {
                var _res = JsonConvert.DeserializeObject<JObject>(await NetworkHelp.GetAsync($"{GameConst.API_URL}/info/GetInfo", new { content = "gameVersion" }));
                if (_res["status"].ToString() != "success") throw new Exception("通讯错误！");

                if (!_res["info"].ToString().Equals(Application.version))
                {
                    Popup.SetActive(true);
                    Text_GameVer.text = $"检测到新版本：<color=#FF6400>{_res["info"]}</color>";
                    UpdateContent.text = JsonConvert.DeserializeObject<JObject>(await NetworkHelp.GetAsync($"{GameConst.API_URL}/info/GetInfo", new { content = "updateContent" }))["info"].ToString();
                    if (UpdateContent.text == string.Empty) Text_Info.text = "获取更新内容失败。";
                    return;
                }

                Slider.value = 100; Close();
            }
            catch (Exception ex)
            {
                PopupManager.PopMessage("错误", $"致命错误：{ex.Message}");
            }
        }
        private void Update ()
        {
            View_Content.GetComponent<RectTransform>().sizeDelta = new Vector2(View_Content.GetComponent<RectTransform>().sizeDelta.x, UpdateContent.GetComponent<RectTransform>().sizeDelta.y);
            UpdateContent.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0, 0, 0);

            //
        }

        public void Close ()
        {
            Destroy(this.gameObject);
            var _obj_game = Instantiate(Resources.Load<GameObject>("GameObject/Scene/InitGame/StartGame"), GameObject.Find("Canvas").transform);
        }
        public async void OpenWeb ()
        {
            Destroy(this.gameObject);
            var _obj_game = Instantiate(Resources.Load<GameObject>("GameObject/Scene/InitGame/StartGame"), GameObject.Find("Canvas").transform);
            try
            {

                if (Application.isMobilePlatform)

                    Application.OpenURL(JsonConvert.DeserializeObject<JObject>(await NetworkHelp.GetAsync($"{GameConst.API_URL}/info/GetInfo", new { content = "downloadURL_Android" }))["info"].ToString());
                else
                    Application.OpenURL(JsonConvert.DeserializeObject<JObject>(await NetworkHelp.GetAsync($"{GameConst.API_URL}/info/GetInfo", new { content = "downloadURL_Windows" }))["info"].ToString());

            }
            catch (Exception ex)
            {
                AppLogger.Log(ex.Message + this.name, "error");
            }
        }

    }
}