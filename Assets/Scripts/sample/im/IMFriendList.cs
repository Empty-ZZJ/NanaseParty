using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using com.m3839.sdk.im;
using com.m3839.sdk.im.bean;
using com.m3839.sdk.im.listener;
using UnityEngine.SceneManagement;

public class IMFriendList : MonoBehaviour
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
        HykbIM.getFriendList(new UnityIMFriendListListenerProxy(this));
    }

    private class UnityIMFriendListListenerProxy : UnityIMFriendListListener
    {

        private IMFriendList friendList;

        public UnityIMFriendListListenerProxy(IMFriendList friendList)
        {
            this.friendList = friendList;
        }

        public override void OnError(int code, string message)
        {
            friendList.LogText.text = "加载失败 code = " + code + ",message = " + message;
        }

        public override void OnSuccess(List<HykbIMFriendInfo> value)
        {
            friendList.LogText.text = "加载成功: value.Count = " + value.Count;
            friendList.ShowData(value);
        }
    }

    private void clearData()
    {
        int childCount = listObject.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            Destroy(listObject.transform.GetChild(i).gameObject);
        }
    }

    private void ShowData(List<HykbIMFriendInfo> value)
    {
        clearData();
        for (int i = 0; i < value.Count; i++)
        {

            GameObject listItem = GameObject.Instantiate(listItemPrefab) as GameObject;
            listItem.transform.parent = listObject.transform;  //设置为 Content 的子对象

            Debug.Log("chenby listItem = " + listItem);

            Text NickText = listItem.transform.Find("NickText").GetComponent<Text>();
            Text UserIdText = listItem.transform.Find("UserIdText").GetComponent<Text>();
            Image HeaderImage = listItem.transform.Find("HeaderImage").GetComponent<Image>();
            Button DeleteButton = listItem.transform.Find("DeleteButton").GetComponent<Button>();

            Debug.Log("chenby NickText = "+ NickText);
            Debug.Log("chenby UserIdText = " + UserIdText);
            Debug.Log("chenby HeaderImage = " + HeaderImage);

            HykbIMFriendInfo info = value[i];
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

            DeleteButton.onClick.AddListener(() => {
                List<string> userIdList = new List<string>();
                userIdList.Add(info.GetUserId());
                HykbIM.deleteFromFriendList(userIdList, new UnityIMFriendOperationResultListenerProxy(this));
            });


            listItem.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -i * 180);
        }
        //根据当前 item 个数更新 Content 高度 
        listObject.GetComponent<RectTransform>().sizeDelta =
               new Vector2(listObject.GetComponent<RectTransform>().sizeDelta.x, value.Count * 180);

        //itemTransform.sizeDelta = new Vector3(0, -value.Count * 180, 0);
        

    }


    private class UnityIMFriendOperationResultListenerProxy : UnityIMFriendOperationResultListListener
    {
        private IMFriendList friendList;

        public UnityIMFriendOperationResultListenerProxy(IMFriendList friendList)
        {
            this.friendList = friendList;
        }

        public override void OnError(int code, string message)
        {
            friendList.LogText.text = "操作失败 code = " + code + ",message = " + message;
        }

        public override void OnSuccess(List<HykbIMFriendOperationResult> value)
        {
            friendList.LogText.text = "删除好友成功";
            friendList.loadData();
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
