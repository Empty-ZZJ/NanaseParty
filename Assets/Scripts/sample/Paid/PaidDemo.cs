using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using com.m3839.sdk.paid;

public class PaidDemo : MonoBehaviour
{
    public Text ShowText;
    // Start is called before the first frame update
    void Start()
    {
        InitSDK();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitSDK()
    {

        string publicKey = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCtC8PkGFU1+1scSbLtoNWGtOld\n" +
                "JIBojR9pK8xkCLqjcdseoZsYmna2+l1UM/VDdr2xTnmo7i2q9UZxunUHksHKF5qY\n" +
                "X6QkFUb08QGeWeP4/9tDlyDFkQIPD50xTgDyig0uS1bBLyvtc1h7noI5G3G09TOL\n" +
                "pih9+5CNHTFL4kavXQIDAQAB";
        string appId = "6032";
        HykbCheckListenerProxy proxy = new HykbCheckListenerProxy(this);
        HykbPaidChecker.CheckLicense(appId, publicKey, proxy);
    }

    private class HykbCheckListenerProxy : HykbCheckListener
    {
        private PaidDemo demo;
        public HykbCheckListenerProxy(PaidDemo demo)
        {
            this.demo = demo;
        }

        public override void OnAllowEnter()
        {
            //校验通过，游戏的其他操作
            demo.ShowText.text = "校验通过";
        }

        public override void OnReject(int code, string message)
        {
            //校验失败的原因
            if(code == 2005)
            {
                Application.Quit();
            }
            Debug.Log("code = " + code + ",message = " + message);
        }
    }
}
