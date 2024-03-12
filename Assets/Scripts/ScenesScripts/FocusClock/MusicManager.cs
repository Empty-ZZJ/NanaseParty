using Common;
using Common.Network;
using Common.UI;
using Newtonsoft.Json;
using System.Collections.Generic;
using TetraCreations.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace ScenesScripts.FocusClock
{
    public class MusicManager : MonoBehaviour
    {
        public const string API_URL_MUSIC = "https://music.api.hoilai.cn";

        [Title("歌曲名称输入框")]
        public InputField InputField_Music;


        [Title("搜索结果列表的父组件")]
        public Transform Creat_MusicListPosi;

        private GameObject MusicItem_List;

        private List<GameObject> MusicList_List_Showed = new();
        public void Start ()
        {
            MusicItem_List = Resources.Load<GameObject>("GameObject/Scene/FocusClock/MusicItem");
        }
        public async void Button_Click_Search ()
        {

            var _loading = new ShowLoading("搜索中");
            var _respond = await NetworkHelp.GetAsync($"{API_URL_MUSIC}/search", new { keywords = InputField_Music.text });
            var _data = JsonConvert.DeserializeObject<MusicSearchModel>(_respond);
            if (_data.code != 200 || _data.result == null)
            {
                _loading.KillLoading();
                PopupManager.PopMessage("错误", "搜索失败！");
            }
            _loading.KillLoading();

            foreach (var item in MusicList_List_Showed)
            {
                Destroy(item);
            }
            foreach (var item in _data.result.songs)
            {
                var _obj = Instantiate(MusicItem_List, Creat_MusicListPosi).GetComponent<MusicItemControler>();
                _obj.Title.text = $"{item.artists[0].name} - {item.name}";
                _obj.MusicID = item.id;
                MusicList_List_Showed.Add(_obj.gameObject);
            }
            Debug.Log(JsonConvert.SerializeObject(_data));
        }
    }
}