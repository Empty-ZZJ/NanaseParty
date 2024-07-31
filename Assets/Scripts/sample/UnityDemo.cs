using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using com.m3839.sdk;


public class UnityDemo : MonoBehaviour
{

    private void Awake()
    {
        //LogUtils.info("chenby34", "Awake");
        ToastUtils.showToast("Awake");
    }
    // Start is called before the first frame update
    void Start()
    {
        //LogUtils.info("chenby34", "Start");
        ToastUtils.showToast("Start");
        LogUtils.debug("chenby","Start");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("我是返回键￣ω￣");
            Application.Quit();
        }
    }

    public void OpenArchivesScene()
    {
        SceneManager.LoadScene("ArchivesScene");
    }

    public void OpenAuxsScene()
    {
        SceneManager.LoadScene("AuxsScene");
    }

    public void OpenSingleScene()
    {
        SceneManager.LoadScene("SingleScene");
    }

    public void OpenLoginScene()
    {
        SceneManager.LoadScene("LoginScene");
    }

    public void OpenPayScene()
    {
        SceneManager.LoadScene("PayScene");
    }

    public void OpenPaidScene()
    {
        SceneManager.LoadScene("PaidScene");
    }

    public void OpenIMScene()
    {
        SceneManager.LoadScene("IMScene");
    }
    public void OpenDLCScene()
    {
        SceneManager.LoadScene("DLCScene");
    }

}
