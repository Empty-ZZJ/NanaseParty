using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using com.m3839.sdk;
using com.m3839.sdk.archives.v2;
using com.m3839.sdk.archives.v2.listener;

public class ArchivesDemo : MonoBehaviour
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

    /// <summary>
    /// 档位列表
    /// </summary>
    public void LoadAllArchives()
    {
        ShowText.text = "LoadAllArchives";
        HykbV2GameArchives archives = new HykbV2GameArchives();
        archives.QueryAllArchive(new HykbV2AllArchivesListenerProxy(this));

    }

    internal class HykbV2AllArchivesListenerProxy : HykbV2AllArchivesListener
    {
        private ArchivesDemo archivesDemo;

        public HykbV2AllArchivesListenerProxy(ArchivesDemo archivesDemo)
        {
            this.archivesDemo = archivesDemo;
        }

        public override void OnSuccess(List<HykbV2GameArchives> dataList)
        {
            ToastUtils.showToast("一共有" + dataList.Count + "条存档");
            archivesDemo.ShowText.text = "一共有"+ dataList.Count + "条存档";
        }

        public override void OnFailed(int code, string message)
        {
            ToastUtils.showToast("读档列表失败" + message);
            archivesDemo.ShowText.text = "读档列表失败" + message;
        }
    }
    
    public void ReadArchives()
    {
        ShowText.text = "ReadArchives";
        HykbV2GameArchives archives = new HykbV2GameArchives();
        archives.SetArchivesId(111);
        archives.ReadArchive(new HykbV2ReadArchivesListenerProxy(this));

    }

    internal class HykbV2ReadArchivesListenerProxy : HykbV2ArchivesListener
    {
        private ArchivesDemo archivesDemo;

        public HykbV2ReadArchivesListenerProxy(ArchivesDemo archivesDemo)
        {
            this.archivesDemo = archivesDemo;
        }

        public override void OnSuccess(AndroidJavaObject bean)
        {
            HykbV2GameArchives data = new HykbV2GameArchives(bean);
            archivesDemo.ShowText.text = data.GetArchivesTitle();
            ToastUtils.showToast(data.GetArchivesTitle());
        }

        public override void OnFailed(int code, string message)
        {
            ToastUtils.showToast("读档失败" + message);
            archivesDemo.ShowText.text = "读档失败" + message;
        }
    }


    public void SaveArchives()
    {
        ShowText.text = "SaveArchives";
        HykbV2GameArchives archives = new HykbV2GameArchives();
        archives.SetArchivesId(111);
        archives.SetArchivesTitle("测试标题");//存档标题
        archives.SetArchivesContent("测试内容");//内容*/
        archives.SaveArchive(new HykbV2SaveArchivesListenerProxy(this));

    }

    internal class HykbV2SaveArchivesListenerProxy : HykbV2ArchivesListener
    {
        private ArchivesDemo archivesDemo;

        public HykbV2SaveArchivesListenerProxy(ArchivesDemo archivesDemo)
        {
            this.archivesDemo = archivesDemo;
        }

        public override void OnSuccess(AndroidJavaObject bean)
        {
            ToastUtils.showToast("存档成功");
            archivesDemo.ShowText.text = "存档成功";
        }

        public override void OnFailed(int code, string message)
        {
            ToastUtils.showToast("存档失败" + message);
            archivesDemo.ShowText.text = "存档失败" + message;
        }
    }
}
