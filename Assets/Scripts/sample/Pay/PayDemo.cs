using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using com.m3839.sdk;
using com.m3839.sdk.login;
using com.m3839.sdk.login.listener;
using com.m3839.sdk.login.bean;
using com.m3839.sdk.pay;
using com.m3839.sdk.pay.bean;
using com.m3839.sdk.pay.listener;
using UnityEngine.SceneManagement;

public class PayDemo : MonoBehaviour
{
    public Text ShowText;
    // Start is called before the first frame update
    void Start()
    {
        HykbUserListenerProxy proxy = new HykbUserListenerProxy(this);
        HykbLogin.SetUserListener(proxy);
    }

    private class HykbUserListenerProxy : HykbUserListener
    {
        private PayDemo instance;

        public HykbUserListenerProxy(PayDemo instance)
        {
            this.instance = instance;
        }

        public override void OnLoginFailed(int code, string message)
        {
            instance.ShowText.text = "code:" + code+ " -message:"+message;
        }

        public override void OnLoginSucceed(HykbUser user)
        {
            instance.ShowText.text = "" + user.getNick();
        }

        public override void OnSwitchUser(HykbUser user)
        {
            instance.ShowText.text = "" + user.getNick();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("我是返回键￣ω￣");
            SceneManager.LoadScene("MainScene");
        }
    }

    /// <summary>
    /// 初始化sdk
    /// </summary>
    public void InitSDK()
    {
        // 游戏ID（Game Id）
        string gameId = "5091";
        // 游戏屏幕方向 （Game Screen Orientation）
        int screenOrienation = HykbContext.SCREEN_PORTRAIT;
        //int screenOrienation = HykbContext.SCREEN_LANDSCAPE;
        // 初始化回调监听（Init callback）
        HykbInitListenerProxy proxy = new HykbInitListenerProxy(this);
        HykbLogin.Init(gameId, screenOrienation, proxy);
    }

    private class HykbInitListenerProxy : HykbV2InitListener
    {
        private PayDemo instance;
        public HykbInitListenerProxy(PayDemo instance)
        {
            this.instance = instance;
        }

        public override void OnSucceed()
        {
            instance.ShowText.text = "初始化成功";
        }

        public override void OnFailed(int code, string message)
        {
            instance.ShowText.text = "" + message;
        }
    }

    /// <summary>
    /// 登录
    /// </summary>
    public void DoLogin()
    {
        HykbUser user = HykbLogin.GetUser();
        if (user != null)
        {
            ShowText.text = user.toString();
            return;
        }
        HykbLogin.Login();
    }

    /// <summary>
    /// 切换账号
    /// </summary>
    public void DoSwitchAccount()
    {
        HykbLogin.SwitchAccount();
    }

    /// <summary>
    /// 获取用户信息
    /// </summary>
    public void GetUser()
    {
        // 获取当前用户信息（Get current User information）
        HykbUser user = HykbLogin.GetUser();
        if (user != null)
        {
            ShowText.text = user.toString();
        }

    }

    /// <summary>
    /// 登出
    /// </summary>
    public void Logout()
    {
        // 登出 （Logout）
        HykbLogin.Logout();
        ShowText.text = "";
    }

    /// <summary>
    /// 支付
    /// </summary>
    public void DoPay()
    {
        int r = Random.Range(0, 10000);
        // 游戏订单编号 （Game Pay Order Number）
        string cpOrderId = "CZX20201119" + r.ToString();
        // 商品名称 （goods Name）
        string goodsName = "vip";
        // 金额
        int money = 1;
        // 区服ID，没有直接传0（server id， default is  0）
        int server = 1;
        // 透传字段，可传空
        string ext = "11";
        HykbPayInfo payInfo = new HykbPayInfo(goodsName, money, server, cpOrderId, ext);
        HykbPayListenerProxy proxy = new HykbPayListenerProxy(this);
        HykbPay.Pay(payInfo, proxy);
    }

    private class HykbPayListenerProxy : HykbV2PayListener
    {
        private PayDemo instance;
        public HykbPayListenerProxy(PayDemo instance)
        {
            this.instance = instance;
        }

        public override void OnFailed(HykbPayInfo payInfo, int code, string message)
        {
            instance.ShowText.text = message;
        }

        public override void OnSucceed(HykbPayInfo payInfo)
        {
            instance.ShowText.text = "支付成功";
        }
    }
}
