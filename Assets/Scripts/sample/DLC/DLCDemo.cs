using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using com.m3839.sdk.dlc;
using com.m3839.sdk.dlc.bean;
using com.m3839.sdk.pay.bean;
using com.m3839.sdk.dlc.listener;
using UnityEngine.SceneManagement;
using com.m3839.sdk;
using com.m3839.sdk.login;
using com.m3839.sdk.login.listener;
using com.m3839.sdk.login.bean;


public class DLCDemo : MonoBehaviour
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
        private DLCDemo instance;

        public HykbUserListenerProxy(DLCDemo instance)
        {
            this.instance = instance;
        }

        public override void OnLoginFailed(int code, string message)
        {
            instance.ShowText.text = "code:" + code + " -message:" + message;
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
        private DLCDemo instance;
        public HykbInitListenerProxy(DLCDemo instance)
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



    public void DoInitDLC()
    {

        HykbDLCInitListenerProxy proxy = new HykbDLCInitListenerProxy(this);
        HykbDLC.init("5091", "834d8569276ebbbdb8e1824ff45c3330", proxy);
    }

    public void DoPurchaseDLC()
    {
        HykbDLCPurchaseListenerProxy proxy = new HykbDLCPurchaseListenerProxy(this);
        HykbDLC.purchase(1, "商品名称", proxy);
    }

    public void DoQueryDLC()
    {
        HykbDLCQueryListenerProxy proxy = new HykbDLCQueryListenerProxy(this);
        HykbDLC.query(1, proxy);
    }

    public void DoReleaseDLC()
    {
        
        HykbDLC.releaseSDK();
    }


    private class HykbDLCInitListenerProxy : HykbDLCInitListener
    {
        private DLCDemo instance;

        public HykbDLCInitListenerProxy(DLCDemo instance)
        {
            this.instance = instance;
        }

        public override void OnSucceed(HykbDLCInitData initData) 
        {
            string content = "ok;";
            foreach (HykbDLCSkuInfo item in initData.getList()) {
                content = content + ",skuId:"+item.getSkuId()+",price:"+item.getPrice();
            }
            instance.ShowText.text = content;
        }


        public override void OnFailed(int code, string message) 
        {
            instance.ShowText.text = "code:" + code + " -message:" + message;
        }
    }

    private class HykbDLCPurchaseListenerProxy : HykbDLCPurchaseListener
    {
        private DLCDemo instance;

        public HykbDLCPurchaseListenerProxy(DLCDemo instance)
        {
            this.instance = instance;
        }

        public override void OnSucceed(HykbPayInfo payInfo)
        {
            instance.ShowText.text = "支付成功";
        }


        public override void OnFailed(HykbPayInfo payInfo, int code, string message)
        {
            instance.ShowText.text = "code:" + code + " -message:" + message;
        }

 
    }


    private class HykbDLCQueryListenerProxy : HykbDLCQueryListener
    {
        private DLCDemo instance;

        public HykbDLCQueryListenerProxy(DLCDemo instance)
        {
            this.instance = instance;
        }

        public override void OnSucceed(int ok)
        {
            instance.ShowText.text = "购买状态:" + ok;
        }


        public override void OnFailed(int code, string message)
        {
            instance.ShowText.text = "code:" + code + " -message:" + message;
        }

    }





}
