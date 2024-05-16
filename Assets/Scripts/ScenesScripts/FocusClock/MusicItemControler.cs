using Common;
using Common.Network;
using Common.UI;
using Newtonsoft.Json;
using System;
using TetraCreations.Attributes;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace ScenesScripts.FocusClock
{
    public class MusicItemControler : MonoBehaviour
    {
        [Title("歌曲标题")]
        public Text Title;
        [Title("歌曲ID")]
        public long MusicID;




        private AudioSource AudioSource_Music;
        private void Start ()
        {
            AudioSource_Music = GameObject.Find("Audio Source-Music").GetComponent<AudioSource>();
        }
        public async void Button_Click_Play ()
        {
            var _loading = new ShowLoading("加载中");
            try
            {
                var _res = await NetworkHelp.GetAsync($"{MusicManager.API_URL_MUSIC}/song/url", new { id = MusicID });
                var _data = JsonConvert.DeserializeObject<MusicInfoModel>(_res);
                if (_data.code != 200 || _data.data == null)
                {
                    throw new Exception("播放失败！");
                }

                using UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(_data.data[0].url, AudioType.MPEG);
                await www.SendWebRequest();

                if (www.result == UnityWebRequest.Result.ConnectionError ||
                   www.result == UnityWebRequest.Result.ProtocolError)
                {
                    throw new Exception(www.error);
                }
                AudioSource_Music.clip = DownloadHandlerAudioClip.GetContent(www);
                AudioSource_Music.Play();
                PopupManager.PopDynamicIsland("正在播放", Title.text, Resources.Load<Sprite>("Texture2D/FocusClock/PictoIcon_Music_On"));

            }
            catch (Exception ex)
            {
                PopupManager.PopMessage("错误", ex.Message);
            }
            finally
            {
                _loading.KillLoading();
            }



        }
    }
}