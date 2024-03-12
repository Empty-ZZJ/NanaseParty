using Common;
using Common.Network;
using Common.UI;
using DG.Tweening;
using GameManager;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
namespace ScenesScripts
{
    public class Popup_Register : MonoBehaviour
    {

        public InputField InputField_User;
        public InputField InputField_Password;
        public InputField InputField_Password_Repeat;
        public RectTransform MainPopup;
        public async void Button_Click_Register ()
        {
            var _account = InputField_User.text;
            var _password = InputField_Password.text;
            var _password_repeat = InputField_Password_Repeat.text;
            if (!IsEmailOrPhone(_account))
            {
                PopupManager.PopMessage("请您规范输入哦~", "账号格式不正确，应为邮箱地址或手机号。");
                return;
            }
            if (_password.Length < 5)
            {
                PopupManager.PopMessage("请您规范输入哦~", "密码长度不正确。");
                return;
            }
            if (!_password.Equals(_password_repeat))
            {
                PopupManager.PopMessage("错误", "两次输入的密码不匹配！");
                return;
            }
            if (!IsValidPassword(_password))
            {
                PopupManager.PopMessage("密码不符合要求", "密码必须满足以下条件：\n 1. 长度大于5个字符 \n 2. 不能包含空白字符或特殊字符 \n 3. 必须包含至少一个字母（大小写均可）");
                return;
            }

            var _loading = new ShowLoading("正在请求");
            var _params = new
            {
                account = _account,
                password = GameAPI.GenerateSha256(_password),
            };
            Debug.Log(JsonConvert.SerializeObject(_params));
            var _res = await NetworkHelp.Post($"{GameConst.API_URL}/Account/Register", _params);
            _loading.KillLoading();
            //_res为返回的json
            Debug.Log(_res);
            try
            {
                //检查是否正确
                var _json = JsonConvert.DeserializeObject<JObject>(_res);
                if (!_json["status"].Equals("success"))
                {
                    PopupManager.PopMessage("错误", "注册失败！");
                    return;
                }
                //注册成功
                PopupManager.PopMessage("恭喜！", "恭喜您成为七濑排队的一员！", () => { Button_Click_Close(); });
                var _uid = _json["UID"].ToString();
                var _token = _json["token"].ToString();
                ServerManager.Config.GameCommonConfig.SetValue("UserInfo", "UID", _uid);
                ServerManager.Config.GameCommonConfig.SetValue("UserInfo", "Token", _token);
            }
            catch
            {
                PopupManager.PopMessage("错误", "注册失败！");
                return;
            }

        }
        private static bool IsEmailOrPhone (string s)
        {
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            string phonePattern = @"^1[3-9]\d{9}$";

            if (Regex.IsMatch(s, emailPattern) || Regex.IsMatch(s, phonePattern))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private static bool IsValidPassword (string s)
        {
            string passwordPattern = @"^(?=.*[a-zA-Z])[a-zA-Z0-9]{6,}$";

            if (Regex.IsMatch(s, passwordPattern))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void Button_Click_Close ()
        {

            MainPopup.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InQuint).OnComplete(() =>
                {

                    Destroy(this.gameObject);
                });
        }
        /*
            public class Model_Register
        {
            public required string status { get; set; }
            public long UID { get; set; }
            public string? token { get; set; }

        }
          */

    }
}