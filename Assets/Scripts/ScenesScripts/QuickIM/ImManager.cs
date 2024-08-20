using AI;
using Common;
using Common.Network;
using Common.UI;
using DG.Tweening;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ScenesScripts.Lobby.AI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TetraCreations.Attributes;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
namespace ScenesScripts.QucikIM
{
    public class ImManager : MonoBehaviour
    {
        [Title("聊天背景框")]
        public Transform BackContent;
        public class _ChatInfo
        {
            public string request;
            public string answer;
        }
        public static List<_ChatInfo> ChatInfo = new();
        public InputField InputField_ChatInfo;
        public static AudioSource MenheraAudioPlayer;
        public RectTransform Popup;

        public async Task<string> GetAnswer (string request)
        {
            var _content = await SparkDeskAPIManager.Tasker("请扮演七濑胡桃，英文名Menherachan或NanaseKurumi，来自画师ぽむ的LINE表情包《メンヘラちゃん。》。" +
                "你的性格是热爱音乐和泡澡，不喜欢孤单，喜欢热闹，性格活泼可爱，机智聪明，比较敏感，你是一个高中生少女，身高151，生日是8月22日，偶尔发发神经。你非常不喜欢菌菇。你和七濑木实是双胞胎姐妹，有一个弟弟七濑太一。" +
                // $"我们的对话记录（JSON格式）：{JsonConvert.SerializeObject(ChatInfo)}" +
                $"如果我问你：{request}，你会怎么回答呢？请用口语化的方式，简洁回答，不超过30个字。就像你是七濑胡桃一样，不要提到AI！");
            AddHistory(request, _content);

            return _content;
        }
        public void AddHistory (string request, string answer)
        {
            ChatInfo.Add(new _ChatInfo { request = request, answer = answer });
            if (ChatInfo.Count >= 5) ChatInfo.RemoveAt(0);
        }

        [Button(nameof(ClearHistory), "清空历史记录")]
        public void ClearHistory ()
        {
            ChatInfo.Clear();
        }
        private void Update ()
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(BackContent.GetComponent<RectTransform>());
        }
        public async void Button_Click_Send ()
        {
            var _loading = new ShowLoading();
            try
            {

                Instantiate(Resources.Load<GameObject>("GameObject/Scene/UIMain/AI/List_Me"), BackContent).GetComponent<AIChatBubble>().Content.text = InputField_ChatInfo.text;
                var _menhera_b = Instantiate(Resources.Load<GameObject>("GameObject/Scene/UIMain/AI/List_Menhera"), BackContent).GetComponent<AIChatBubble>();
                _menhera_b.Content.text = "正在思考中.......";
                _menhera_b.Content.text = await GetAnswer(InputField_ChatInfo.text);
                if (!Application.isMobilePlatform)
                {
                    var _audio = JsonConvert.DeserializeObject<JObject>(await NetworkHelp.Post($"{GameConst.API_URL}/ai/GetTTS", new { content = _menhera_b.Content.text }))["Audio"].ToString();
                    Debug.Log(_audio);
                    var _path = await Base64StringToFile(_audio);
                    StartCoroutine(PlayAudio(_path));
                }


                InputField_ChatInfo.text = string.Empty;
            }
            catch
            {
                PopupManager.PopMessage("错误", "网络错误");
            }
            finally
            {
                _loading.KillLoading();
            }
        }
        private void Start ()
        {
            if (MenheraAudioPlayer is null) MenheraAudioPlayer = GameObject.Find("Menherachan/Menherachan_AIChat/Body").GetComponent<AudioSource>();
            GameObject.Find("AudioSystem/Audio_Back").GetComponent<AudioSource>().DOFade(0.25f, 1f);
        }
        /// <summary>
        /// Base64字符串转文件并保存
        /// </summary>
        /// <param name="base64String">base64字符串</param>
        /// <param name="fileName">保存的文件名</param>
        /// <returns>是否转换并保存成功</returns>
        public async Task<string> Base64StringToFile (string base64String)
        {
            try
            {
                var _path_d = $"{Application.persistentDataPath}/audio";
                if (!Directory.Exists(_path_d)) Directory.CreateDirectory(_path_d);
                string strbase64 = base64String.Trim().Substring(base64String.IndexOf(",") + 1);   //将‘，’以前的多余字符串删除
                MemoryStream stream = new MemoryStream(Convert.FromBase64String(strbase64));
                var _path = $"{_path_d}/{Guid.NewGuid().ToString()}.wav";
                FileStream fs = new FileStream(_path, FileMode.OpenOrCreate, FileAccess.Write);
                byte[] b = stream.ToArray();
                await fs.WriteAsync(b, 0, b.Length);
                fs.Close();
                return _path;
            }
            catch
            {
                return string.Empty;
            }


        }
        private IEnumerator PlayAudio (string fileName)
        {
            //获取.wav文件，并转成AudioClip
            UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(fileName, AudioType.WAV);
            //等待转换完成
            yield return www.SendWebRequest();
            //获取AudioClip
            AudioClip audioClip = DownloadHandlerAudioClip.GetContent(www);


            //设置当前AudioSource组件的AudioClip
            MenheraAudioPlayer.clip = audioClip;
            //播放声音
            MenheraAudioPlayer.Play();
        }
        public void Button_Click_Close ()
        {
            GameObject.Find("AudioSystem/Audio_Back").GetComponent<AudioSource>().DOFade(1f, 1f);
            Popup.DOAnchorPos3D(new Vector3(-500, 0, 0), 0.5f).OnComplete(() =>
            {
                Destroy(this.gameObject);
            });
        }

    }
}