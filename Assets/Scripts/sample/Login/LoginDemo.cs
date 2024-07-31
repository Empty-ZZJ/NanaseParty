using System.Collections;
using System.Collections.Generic;
using com.m3839.sdk;
using com.m3839.sdk.login;
using com.m3839.sdk.login.bean;
using com.m3839.sdk.login.listener;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginDemo : MonoBehaviour
{
    public Text ShowText;
    // Start is called before the first frame update
    void Start()
    {
        HykbUserListenerProxy proxy = new HykbUserListenerProxy(this);
        HykbLogin.SetUserListener(proxy);

       
    }

    /// <summary>
    /// 用户信息相关的监听（登录和切换账号）
    /// </summary>
    private class HykbUserListenerProxy : HykbUserListener
    {
        private LoginDemo instance;

        public HykbUserListenerProxy(LoginDemo instance)
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
        private LoginDemo instance;
        public HykbInitListenerProxy(LoginDemo instance)
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
        if(user != null)
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

  
}
