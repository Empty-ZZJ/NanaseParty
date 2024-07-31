using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using com.m3839.sdk.im;
using com.m3839.sdk.im.bean;
using com.m3839.sdk.im.listener;
using UnityEngine.SceneManagement;

public class IMFriendApplicationList : MonoBehaviour
{
    public Text LogText;

    public GameObject listObject;

    public GameObject listItemPrefab;

    // Start is called before the first frame update
    void Start()
    {
        loadData();
    }

    private void loadData()
    {
        HykbIM.getFriendApplicationList(new UnityIMFriendApplicationResultListenerProxy(this));
    }

    private class UnityIMFriendApplicationResultListenerProxy : UnityIMFriendApplicationResultListener
    {

        private IMFriendApplicationList friendList;

        public UnityIMFriendApplicationResultListenerProxy(IMFriendApplicationList friendList)
        {
            this.friendList = friendList;
        }

        public override void OnError(int code, string message)
        {
            friendList.LogText.text = "加载失败 code = " + code + ",message = " + message;
        }

        public override void OnSuccess(HykbIMFriendApplicationResult value)
        {
            friendList.LogText.text = "加载成功";
            
            friendList.ShowData(value.GetHykbIMFriendApplicationList());
        }
    }

    private void clearData()
    {
        int childCount = listObject.transform.childCount;
        for(int i = 0; i < childCount; i++)
        {
            Destroy(listObject.transform.GetChild(i).gameObject);
        }
    }

    private void ShowData(List<HykbIMFriendApplication> value)
    {
        Debug.Log("chenby value.Count = "+value.Count);
        clearData();
        for (int i = 0; i < value.Count; i++)
        {

            GameObject listItem = GameObject.Instantiate(listItemPrefab) as GameObject;
            listItem.transform.parent = listObject.transform;  //设置为 Content 的子对象

            Debug.Log("chenby listItem = " + listItem);

            Text NickText = listItem.transform.Find("NickText").GetComponent<Text>();
            Text UserIdText = listItem.transform.Find("UserIdText").GetComponent<Text>();
            Image HeaderImage = listItem.transform.Find("HeaderImage").GetComponent<Image>();
            Button AgreeButton = listItem.transform.Find("AgreeButton").GetComponent<Button>();
            Button RejectButton = listItem.transform.Find("RejectButton").GetComponent<Button>();
            Button DeleteButton = listItem.transform.Find("DeleteButton").GetComponent<Button>();

            Debug.Log("chenby NickText = " + NickText);
            Debug.Log("chenby UserIdText = " + UserIdText);
            Debug.Log("chenby HeaderImage = " + HeaderImage);

            HykbIMFriendApplication info = value[i];
            Debug.Log("chenby info = " + info.GetUserId());

            NickText.text = info.GetNickName();
            UserIdText.text = "ID:" + info.GetUserId();
            if (info.GetFaceUrl() == null && info.GetFaceUrl().Length <= 0)
            {
                LoadImage(HeaderImage, "https://docs.unity3d.com/uploads/Main/ShadowIntro.png");
            }
            else
            {
                LoadImage(HeaderImage, info.GetFaceUrl());
            }

            AgreeButton.onClick.AddListener(() => {
                HykbIM.acceptFriendApplication(info, new UnityIMFriendOperationResultListenerProxy(this, 1));
            });

            RejectButton.onClick.AddListener(() => {
                HykbIM.refuseFriendApplication(info, new UnityIMFriendOperationResultListenerProxy(this, 2));
            });

            DeleteButton.onClick.AddListener(() => {
                HykbIM.deleteFriendApplication(info, new HykbIMCallbackProxy(this));
            });

            listItem.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -i * 180);
        }
        //根据当前 item 个数更新 Content 高度 
        listObject.GetComponent<RectTransform>().sizeDelta = new Vector2(0, value.Count * 180);

        //itemTransform.sizeDelta = new Vector3(0, -value.Count * 180, 0);

    }

    private class UnityIMFriendOperationResultListenerProxy : UnityIMFriendOperationResultListener
    {
        private IMFriendApplicationList friendList;
        private int type;

        public UnityIMFriendOperationResultListenerProxy(IMFriendApplicationList friendList, int type)
        {
            this.friendList = friendList;
            this.type = type;
        }

        public override void OnError(int code, string message)
        {
            friendList.LogText.text = "操作失败 code = " + code + ",message = " + message;
        }

        public override void OnSuccess(HykbIMFriendOperationResult value)
        {
            if(type == 1)
            {
                friendList.LogText.text = "同意好友申请成功";
            }
            else
            {
                friendList.LogText.text = "拒绝好友申请成功";
            }
            

            friendList.loadData();
        }
    }


    private class HykbIMCallbackProxy : HykbIMCallback
    {
        private IMFriendApplicationList friendApplicationList;

        public HykbIMCallbackProxy(IMFriendApplicationList friendApplicationList)
        {
            this.friendApplicationList = friendApplicationList;
        }

        public override void OnError(int code, string message)
        {
            friendApplicationList.LogText.text = "操作失败 code = " + code + ",message = " + message;
        }

        public override void OnSuccess()
        {
            friendApplicationList.loadData();
        }
    }


    public void LoadImage(Image HeaderImage, string url)
    {
        //StartCoroutine(LoadTexture2D("https://docs.unity3d.com/uploads/Main/ShadowIntro.png"));
        StartCoroutine(LoadTexture2D(HeaderImage, url));
    }

    public IEnumerator LoadTexture2D(Image HeaderImage, string path)
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


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("我是返回键￣ω￣");
            SceneManager.LoadScene("IMScene");
        }
    }
}
