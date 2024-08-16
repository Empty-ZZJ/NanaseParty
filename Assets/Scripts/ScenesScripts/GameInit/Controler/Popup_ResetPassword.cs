using Common;
using Common.Network;
using Common.UI;
using DG.Tweening;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

namespace ScenesScripts
{
    public class Popup_ResetPassword : MonoBehaviour
    {
        public InputField InputField_User;
        public InputField InputField_Verification;
        public InputField InputField_NewPassword;
        public Text Text_Verification;
        public RectTransform MainPopup;

        private int _Countdown_Time = 60;


        private bool IsVerification;


        public async void Button_Click_SendVerification ()
        {
            if (IsVerification)
            {
                PopupManager.PopMessage("心急吃不了热豆腐~", "重发验证码请等待60s。");
                return;
            }
            var _account = InputField_User.text;
            var _Verification = InputField_Verification.text;


            var _i = IsEmailOrPhone(_account);//账号类型
            var _res = "";


            if (_i.Equals(0))
            {
                PopupManager.PopMessage("请您规范输入哦~", "请正确输入邮箱或手机号。");
                return;
            }
            var _loading = new ShowLoading("正在加载中");
            if (_i.Equals(1))//邮箱
            {
                _res = await NetworkHelp.Post($"{GameConst.API_URL_Account}/Account/SendVerification_Email", new
                {
                    email = _account
                });
            }
            else//手机号
            {
                _res = await NetworkHelp.Post($"{GameConst.API_URL_Account}/Account/SendVerification_Phone", new
                {
                    phoneNo = _account
                });
            }
            _loading.KillLoading();
            Debug.Log(_res);
            try
            {
                JsonConvert.DeserializeObject<JObject>(_res)["token"].ToString();

                StartCoroutine(CountDownFunction());
                IsVerification = true;

            }
            catch
            {
                PopupManager.PopMessage("错误", "验证码发送失败！");
                return;
            }
        }
        private static int IsEmailOrPhone (string s)
        {
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            string phonePattern = @"^1[3-9]\d{9}$";
            if (Regex.IsMatch(s, emailPattern)) return 1;
            if (Regex.IsMatch(s, phonePattern)) return 2;
            return 0;
        }
        private IEnumerator CountDownFunction ()
        {
            while (_Countdown_Time > 0)
            {
                yield return new WaitForSeconds(1);
                _Countdown_Time--;
                Text_Verification.text = _Countdown_Time.ToString();
            }
            Text_Verification.text = "获取验证码";
            IsVerification = false;
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
        public async void Button_Click_Reset ()
        {
            var _account = InputField_User.text;
            var _Verification = InputField_Verification.text;
            var _InputField_NewPassword = InputField_NewPassword.text;
            if (!IsValidPassword(_InputField_NewPassword))
            {
                PopupManager.PopMessage("密码不符合要求", "密码必须满足以下条件：\n 1. 长度大于5个字符 \n 2. 不能包含空白字符或特殊字符 \n 3. 必须包含至少一个字母（大小写均可）");
                return;
            }
            var _res = await NetworkHelp.Post($"{GameConst.API_URL_Account}/Account/ResetPassword", new
            {
                account = _account,
                newpPassword = GameAPI.GenerateSha256(_InputField_NewPassword),
                verification = _Verification
            });
            if (!JsonConvert.DeserializeObject<JObject>(_res)["status"].Equals("true"))
            {
                PopupManager.PopMessage("重置失败", " 验证码不正确！");
                return;
            }
            PopupManager.PopMessage("重置成功", " 点击登录。");
            Button_Click_Close();



        }

    }
}
