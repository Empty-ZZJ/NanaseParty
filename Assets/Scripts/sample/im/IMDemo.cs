using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using com.m3839.sdk.im;
using com.m3839.sdk.im.listener;
using com.m3839.sdk.im.bean;
using com.m3839.sdk;
using UnityEngine.SceneManagement;

public class IMDemo : MonoBehaviour
{
    private InviteDialog dialog;

    public GameObject dialogObject;

    public Text ShowText;

    public InputField input;

    // Start is called before the first frame update
    void Start()
    {
        HykbIM.addHykbIMSignalingListener(new HykbIMSignalingListenerProxy(this));
        Debug.Log("chenby IMDemo Start");
        dialogObject.SetActive(false);
    }

    private class HykbIMSignalingListenerProxy : HykbIMSignalingListener
    {

        private IMDemo demo;

        public HykbIMSignalingListenerProxy(IMDemo demo)
        {
            this.demo = demo;
        }

        public override void OnInvitationCancelled(string inviteID, string inviter, string data)
        {
            
        }

        public override void OnInvitationTimeout(string inviteID, List<string> inviteeList)
        {
            
        }

        public override void OnInviteeAccepted(string inviteID, string invitee, string data)
        {
            demo.ShowText.text = "对方同意邀请：invitee =" + invitee;
        }

        public override void OnInviteeRejected(string inviteID, string invitee, string data)
        {
            demo.ShowText.text = "对方拒绝邀请：invitee =" + invitee;
        }

        public override void OnReceiveNewInvitation(string inviteID, string inviter, string groupID, List<string> inviteeList, string data)
        {
            Debug.Log("chenby OnReceiveNewInvitation");
            Debug.Log("chenby inviteID = "+ inviteID);
            Debug.Log("chenby inviter = " + inviter);
            Debug.Log("chenby data = " + data);
            demo.showDialog(inviteID, data);
            

        }
    }

    void showDialog(string inviteID, string data)
    {
        //var dialogObject = Resources.Load<GameObject>("DialogPanel");
        dialogObject.SetActive(true);
        dialog = dialogObject.transform.GetComponent<InviteDialog>();
        dialog.setTitle("邀请弹窗");
        dialog.setTitle(data);
        dialog.OnAgree += (() => {
            HykbIM.accept(inviteID, data, new HykbIMCallbackProxy(this));
        });
        dialog.OnRefuse += (() => {
            HykbIM.reject(inviteID, data, new HykbIMCallbackProxy(this));
        });
        //dialog.OnAgree += DoAgree;
    }

    private class HykbIMCallbackProxy : HykbIMCallback
    {
        private IMDemo demo;

        public HykbIMCallbackProxy(IMDemo demo)
        {
            this.demo = demo;
        }

        public override void OnError(int code, string message)
        {
            demo.ShowText.text = "code:" + code + " -message:" + message;
        }

        public override void OnSuccess()
        {
            demo.ShowText.text = "OnSuccess";
        }
    }

    private void DoAgree()
    {
        
    }


    public void SendInvite()
    {
        string userId = input.text;
        if(userId == null || userId.Length == 0)
        {
            ShowText.text = "userId 不能为空";
        }
        HykbIM.invite(userId, "一起来开黑吧", new HykbIMCallbackProxy(this));
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
    /// 初始化SDK
    /// </summary>
    public void InitSDK()
    {
        int screenOrienation = HykbContext.SCREEN_PORTRAIT;
        HykbIM.initWithHykb("5091", screenOrienation, new HykbIMInitListenerProxy(this));
    }

    private class HykbIMInitListenerProxy : HykbIMInitListener
    {

        private IMDemo demo;

        public HykbIMInitListenerProxy(IMDemo demo)
        {
            this.demo = demo;
        }

        public override void OnFailed(int code, string message)
        {
            demo.ShowText.text = "初始化失败 code:" + code + " -message:" + message;
        }

        public override void OnSucceed()
        {
            demo.ShowText.text = "初始化成功";
        }
    }

    /// <summary>
    /// 登录调用
    /// </summary>
    public void LoginIM()
    {
        HykbIM.loginWithHykb(new HykbIMLoginListener(this));
    }

    private class HykbIMLoginListener : UnityIMUserListener
    {

        private IMDemo demo;

        public HykbIMLoginListener(IMDemo demo)
        {
            this.demo = demo;
        }

        public override void OnError(int code, string message)
        {
            demo.ShowText.text = "登录失败 code:" + code + " -message:" + message;
        }

        public override void OnSuccess(HykbIMUserInfo info)
        {
            demo.ShowText.text = "userInfo:" + info.GetUserId(); 
        }
    }

    /// <summary>
    /// 登出
    /// </summary>
    public void logoutIM()
    {
        HykbIM.logoutWithHykb(new HykbIMCallbackLogout(this));
    }

    private class HykbIMCallbackLogout : HykbIMCallback
    {
        private IMDemo demo;

        public HykbIMCallbackLogout(IMDemo demo)
        {
            this.demo = demo;
        }

        public override void OnError(int code, string message)
        {
            demo.ShowText.text = "登出失败 code:" + code + " -message:" + message;
        }

        public override void OnSuccess()
        {
            demo.ShowText.text = "登出成功";
        }
    }

    public void OpenSearchUser()
    {
        SceneManager.LoadScene("IMSearchUserScene");
    }

    public void OpenSearchFriend()
    {
        SceneManager.LoadScene("IMSearchFriendScene");
    }

    public void OpenFriendList()
    {
        SceneManager.LoadScene("IMFriendListScene");
    }

    public void OpenFriendApplicationList()
    {
        SceneManager.LoadScene("IMFriendApplicationListScene");
    }
}
