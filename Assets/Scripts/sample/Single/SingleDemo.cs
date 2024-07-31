using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using com.m3839.sdk;
using com.m3839.sdk.single;

public class SingleDemo : MonoBehaviour
{
    public Text ShowText;
    // Start is called before the first frame update
    void Start()
    {
        
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
        string gameId = "5091";
        int orientation = HykbContext.SCREEN_PORTRAIT;
        UnionFcmListenerProxy listenerProxy = new UnionFcmListenerProxy(this);
        UnionFcmSDK.Init(gameId, orientation, listenerProxy);
    }

    private class UnionFcmListenerProxy : UnionV2FcmListener
    {
        private SingleDemo demo;

        public UnionFcmListenerProxy(SingleDemo demo)
        {
            this.demo = demo;
        }

        public override void OnFailed(int code, string message)
        {
            if (2005 == code || 2003 == code)
            {
                Application.Quit();
            }
            else
            {
                demo.ShowText.text = message;
            }
        }

        public override void OnSucceed(UnionFcmUser user)
        {
            if (user != null)
            {
                //登录成功
                demo.ShowText.text = user.toString();
            }
        }
    }
}
