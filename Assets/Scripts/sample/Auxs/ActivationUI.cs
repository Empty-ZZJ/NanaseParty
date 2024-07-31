using System.Collections;
using System.Collections.Generic;
using com.m3839.sdk.auxs;
using com.m3839.sdk.auxs.bean;
using com.m3839.sdk.auxs.listener;
using UnityEngine;
using UnityEngine.UI;
using utils;

/// <summary>
/// 这个只是一个举例，自定义激活码弹窗UI
/// </summary>
public class ActivationUI : MonoBehaviour
{
    public GameObject activationObj;
    public InputField codeInput;
    public Text titleText;
    public Text linkText;
    // Start is called before the first frame update

    private AuxActivationListener listener;

    void Start()
    {
        HykbActivationUiInfo activationUiInfo = HykbAuxsSDK.getActivationUiInfo();
        titleText.text = activationUiInfo.getActivationTitle();
        if (activationUiInfo.getActivationLinkStatus() == 1)
        {
            linkText.gameObject.SetActive(true);
            linkText.text = activationUiInfo.getActivationLinkTitle();
        }
        else
        {
            linkText.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }



    private void JumpLink()
    {
        ToastUtils.showToast("外部链接跳转");
    }

    public void Subumit()
    {
        if (codeInput.text == "")
        {
            ToastUtils.showToast("激活码不能为空！");
            return;
        }
        string device = "12hjuysh2k2";
        if (listener == null)
        {
            listener = new AuxActivationListener(this);
        }
        HykbAuxsSDK.CheckActivationCode(device, codeInput.text, listener);
    }

    private class AuxActivationListener : HykbV2AuxActivationListener
    {
        private ActivationUI activationUI;

        public AuxActivationListener(ActivationUI activationUI)
        {
            this.activationUI = activationUI;
        }

        public override void OnFailed(int code, string message)
        {
            ToastUtils.showToast(message);
        }

        public override void OnSucceed()
        {
            //表示激活码校验通过（非自定义激活码UI的情况）
            ToastUtils.showToast("激活码成功");
            activationUI.activationObj.SetActive(false);
        }
    }
}
