using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using com.m3839.sdk;
using com.m3839.sdk.auxs;
using com.m3839.sdk.auxs.listener;
using com.m3839.sdk.auxs.bean;

public class AuxsDemo : MonoBehaviour
{
    public GameObject activationObj;
    public Text ShowText;
    // Start is called before the first frame update
    void Start()
    {
        activationObj.SetActive(false);
        activationObj.GetComponent<ActivationUI>().enabled = false;
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

    public void InitSDK()
    {
        //初始化送审SDK
        string gameId = "5091";
        int orientation = HykbContext.SCREEN_PORTRAIT;  //竖屏   横屏：HykbContext.SCREEN_LANDSCAPE
        AuxInitListener InitListener = new AuxInitListener(this);
        HykbAuxsSDK.Init(gameId, orientation, InitListener);
    }

    private class AuxInitListener : HykbV2AuxInitListener
    {
        private AuxsDemo auxsSample;

        public AuxInitListener(AuxsDemo auxsSample)
        {
            this.auxsSample = auxsSample;
        }

        public override void OnFailed(int code, string message)
        {
            auxsSample.ShowText.text = message;
        }

        public override void OnSucceed()
        {
            auxsSample.ShowText.text = "Init OK";
        }
    }

    public void DoActivationCheck()
    {
        AuxActivationListener ActivationListener = new AuxActivationListener(this);
        string device = "xdfghtffff1111";
        HykbAuxsSDK.CheckActivationCode(device, ActivationListener);
    }

    private class AuxActivationListener : HykbV2AuxActivationListener
    {
        private AuxsDemo auxsSample;

        public AuxActivationListener(AuxsDemo auxsSample)
        {
            this.auxsSample = auxsSample;
        }

        public override void OnFailed(int code, string message)
        {
            auxsSample.ShowText.text = message;
        }

        public override void OnSucceed()
        {
            //码兑换成功，游戏可进行商品发放
            auxsSample.ShowText.text = "激活码激活成功";
        }
    }

    public void DoGiftCheck()
    { 
        AuxGiftListener GiftListener = new AuxGiftListener(this);
        string device = "xdfghtffff1111";
        HykbAuxsSDK.CheckGiftCode(device, GiftListener);
    }

    private class AuxGiftListener : HykbV2AuxGiftListener
    {
        private AuxsDemo auxsSample;

        public AuxGiftListener(AuxsDemo auxsSample)
        {
            this.auxsSample = auxsSample;
        }

        public override void OnFailed(int code, string message)
        {
            auxsSample.ShowText.text = message;
        }

        public override void OnSucceed(string giftName)
        {
            //礼包码兑换成功，游戏可进行商品发放
            auxsSample.ShowText.text = "礼包名称：" + giftName;
        }
    }

    public void ShowCustomActivationUI()
    {
        HykbActivationUiInfo activationUiInfo = HykbAuxsSDK.getActivationUiInfo();
        if (activationUiInfo != null)
        {
            activationObj.SetActive(true);
            activationObj.GetComponent<ActivationUI>().enabled = true;
        }
    }

    public void OpenPage(int biz)
    {
        HykbAuxsSDK.openPageDetail(biz);
    }

    public void OpenBizDialog(int biz)
    {
        HykbAuxsSDK.openBizDialog(biz);
    }

    public void OpenCommentDialog()
    {
        HykbAuxsSDK.openCommentDialog();
    }

    public void JoinQQGroup()
    {
        HykbAuxsSDK.joinQQGroup();
    }
}
