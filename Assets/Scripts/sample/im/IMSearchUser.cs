using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using com.m3839.sdk.im;
using com.m3839.sdk.im.bean;
using com.m3839.sdk.im.listener;
using UnityEngine.SceneManagement;

public class IMSearchUser : MonoBehaviour
{

    public Text NickText;

    public Text UserIdText;

    public InputField UserIdInput;

    public Image HeaderImage;

    public Text LogText;

    public string rightUserId;

    // Start is called before the first frame update
    void Start()
    {
        // 加载在线图片
        //StartCoroutine(LoadTexture2D("https://docs.unity3d.com/uploads/Main/ShadowIntro.png"));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("我是返回键￣ω￣");
            SceneManager.LoadScene("IMScene");
        }
    }

    public void DoAddFriend()
    {
        if(rightUserId == null || rightUserId.Length <= 0)
        {
            LogText.text = "请输入合法的用户ID";
            return;
        }
        HykbIM.addFriend(rightUserId, "一眼相中你", new HykbIMFriendOperationListenerProxy(this));
    }


    private class HykbIMFriendOperationListenerProxy : UnityIMFriendOperationResultListener
    {
        private IMSearchUser searchUser;
        public HykbIMFriendOperationListenerProxy(IMSearchUser searchUser)
        {
            this.searchUser = searchUser;
        }

        public override void OnError(int code, string message)
        {
            searchUser.LogText.text = "添加好友 code = " + code + ",message = " + message;
        }

        public override void OnSuccess(HykbIMFriendOperationResult value)
        {
            searchUser.LogText.text = "添加好友成功";
        }
    }


    public void DoSearchUser()
    {
        string userId = UserIdInput.text;
        if (userId == null || userId.Length == 0)
        {
            LogText.text = "userId 不能为空";
            return;
        }

        HykbIM.getUserInfo(userId, new HykbIMUserListenerProxy(this));
    }


    private class HykbIMUserListenerProxy : UnityIMUserListener
    {

        private IMSearchUser searchUser;

        public HykbIMUserListenerProxy(IMSearchUser searchUser)
        {
            this.searchUser = searchUser;
        }

        public override void OnError(int code, string message)
        {
            searchUser.LogText.text = "搜索失败 code = " + code + ",message = " + message;
        }

        public override void OnSuccess(HykbIMUserInfo value)
        {
            searchUser.NickText.text = value.GetNickName();
            searchUser.UserIdText.text = "ID:" + value.GetUserId();
            if(value.GetFaceUrl() == null && value.GetFaceUrl().Length <= 0)
            {
                searchUser.LoadImage("https://docs.unity3d.com/uploads/Main/ShadowIntro.png");
            }
            else
            {
                searchUser.LoadImage(value.GetFaceUrl());
            }
            searchUser.rightUserId = value.GetUserId();
        }
    }

    public void LoadImage(string url)
    {
        //StartCoroutine(LoadTexture2D("https://docs.unity3d.com/uploads/Main/ShadowIntro.png"));
        StartCoroutine(LoadTexture2D(url));
    }

    public IEnumerator LoadTexture2D(string path)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(path);
        yield return request.SendWebRequest();

        if (request.isHttpError || request.isNetworkError)
        { }
        else
        {

            var texture = DownloadHandlerTexture.GetContent(request);
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            HeaderImage.sprite = sprite;
        }
    }
}
