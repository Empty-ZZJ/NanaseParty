using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using com.m3839.sdk.im;
using com.m3839.sdk.im.bean;
using com.m3839.sdk.im.listener;
using UnityEngine.SceneManagement;


public class IMSearchFriend : MonoBehaviour
{

    public InputField KeywordInput;

    public Text LogText;

    public GameObject listObject;

    public GameObject listItemPrefab;

    protected string rightUserId;

    protected RectTransform itemTransform;

    // Start is called before the first frame update
    void Start()
    {
        itemTransform = listObject.GetComponent<RectTransform>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("我是返回键￣ω￣");
            SceneManager.LoadScene("IMScene");
        }
    }

    public void DoSearchFriend()
    {
        string keyword = KeywordInput.text;
        if (keyword == null || keyword.Length == 0)
        {
            LogText.text = "关键词 不能为空";
            return;
        }

        HykbIM.searchFriends(keyword, new UnityIMFriendListListenerProxy(this));
    }


    private class UnityIMFriendListListenerProxy : UnityIMFriendListListener
    {

        private IMSearchFriend searchUser;

        public UnityIMFriendListListenerProxy(IMSearchFriend searchUser)
        {
            this.searchUser = searchUser;
        }

        public override void OnError(int code, string message)
        {
            searchUser.LogText.text = "搜索失败 code = " + code + ",message = " + message;
        }

        public override void OnSuccess(List<HykbIMFriendInfo> value)
        {
            searchUser.LogText.text = "搜索成功: value.Count = "+ value.Count;
            searchUser.ShowData(value);
        }
    }


    private void ShowData(List<HykbIMFriendInfo> value)
    {
        //Transform temp = Instantiate(itemTransform).transform;
        //temp.SetParent(contentTransform);
        //temp.localPosition = Vector3.zero;
        //temp.localRotation = Quaternion.identity;
        //temp.localScale = Vector3.one;

        for (int i = 0; i < value.Count; i++)
        {
            GameObject listItem = GameObject.Instantiate(listItemPrefab) as GameObject;
            listItem.transform.parent = listObject.transform;  //设置为 Content 的子对象

            Debug.Log("chenby listItem = " + listItem);

            Text NickText = listItem.transform.Find("NickText").GetComponent<Text>();
            Text UserIdText = listItem.transform.Find("UserIdText").GetComponent<Text>();
            Image HeaderImage = listItem.transform.Find("HeaderImage").GetComponent<Image>();

            Debug.Log("chenby NickText = " + NickText);
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

            listItem.GetComponent<RectTransform>().anchoredPosition = new Vector2(listObject.GetComponent<RectTransform>().sizeDelta.x, -i * 180);
        }
        listObject.GetComponent<RectTransform>().sizeDelta =
                 new Vector2(listObject.GetComponent<RectTransform>().sizeDelta.x, value.Count * 180);
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
}
