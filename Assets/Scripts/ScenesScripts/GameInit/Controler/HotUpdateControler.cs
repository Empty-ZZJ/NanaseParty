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
        public GameObject View_Content;
        private async void Start ()
        {
            Slider.value = 0;
            Text_Info.text = "正在检查更新....";
            try
            {
                var _res = JsonConvert.DeserializeObject<JObject>(await NetworkHelp.GetAsync($"{GameConst.API_URL}/info/GetInfo", new { content = "gameVersion" }));
                if (_res["status"].ToString() != "success") throw new Exception("通讯错误！");

                if (!_res["info"].ToString().Equals(Application.version))
                {
                    Popup.SetActive(true);
                    Text_GameVer.text = $"检测到新版本：<color=#FF6400>{_res["info"]}</color>";
                    View_Content.GetComponentInChildren<Text>().text = JsonConvert.DeserializeObject<JObject>(await NetworkHelp.GetAsync($"{GameConst.API_URL}/info/GetInfo", new { content = "updateContent" }))["info"].ToString();
                    View_Content.GetComponent<RectTransform>().sizeDelta = View_Content.GetComponentInChildren<RectTransform>().sizeDelta;
                    View_Content.GetComponentInChildren<RectTransform>().anchoredPosition3D = new Vector3(0, -View_Content.GetComponentInChildren<RectTransform>().sizeDelta.y / 2 + 10, 0);
                    return;
                }
                Slider.value = 100;

            }
            catch (Exception ex)
            {
                PopupManager.PopMessage("错误", $"致命错误：{ex.Message}");
            }
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
                Application.OpenURL(JsonConvert.DeserializeObject<JObject>(await NetworkHelp.GetAsync($"{GameConst.API_URL}/info/GetInfo", new { content = "downloadURL" }))["info"].ToString());
            }
            catch (Exception ex)
            {

            }
        }

    }
}