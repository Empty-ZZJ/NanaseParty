using Common;
using DG.Tweening;
using GameManager;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TetraCreations.Attributes;
using UnityEngine;
namespace ScenesScripts.MiniGame.MusicGame
{
    public class MusicGameManager : MonoBehaviour
    {
        [Title("生成面板")]
        public Transform Panel;
        public AudioSource AudioPlayer;
        private static List<MusicBeat> Beats = new();
        public GameObject GamePanel_Mask;
        private static GameObject Food;
        public double TotalSeconds;
        public GameObject Button_Back;
        public void Start ()
        {
            try
            {
                GamePanel_Mask.SetActive(true);
                Food = Resources.Load<GameObject>("GameObject/Scene/MiniGame/MusicGame/Food");

                Debug.Log("节拍数：" + Beats.Count);
            }
            catch (Exception ex)
            {
                PopupManager.PopMessage("错误", ex.Message);
                AppLogger.Log(ex.Message);
                throw;
            }


        }
        public void Button_Click_StartGame (string id = "1")
        {
            try
            {
                if (ListItemController.SelectID == string.Empty)
                {
                    PopupManager.PopMessage("提示", "请选择歌曲项目进行演奏。");
                    return;
                }
                GameObject.Find("MainCanvas/GamePanel-Mask/Panel").GetComponent<RectTransform>().DOAnchorPos3D(new Vector3(0f, -1000f, 0f), 0.5f).OnComplete(() =>
                {
                    Button_Back.SetActive(false);
                    Beats.Clear();
                    GamePanel_Mask.SetActive(false);
                    var _audioclip = Resources.Load<AudioClip>($"Audio/MenheraMusic/music/music ({ListItemController.SelectID})");
                    Beats = JsonConvert.DeserializeObject<List<MusicBeat>>(Resources.Load<TextAsset>($"Audio/MenheraMusic/bets/bets{ListItemController.SelectID}").text);
                    AudioPlayer.clip = _audioclip;
                    AudioPlayer.Play();
                    TotalSeconds = 0;
                    StartCoroutine(CreatBreats());
                    StartCoroutine(TimeEvent());

                });
            }
            catch (Exception ex)
            {
                PopupManager.PopMessage("错误", ex.Message);
                AppLogger.Log(ex.Message);
                throw;
            }
        }
        private IEnumerator CreatBreats ()
        {

            while (true)
            {
                try
                {
                    if (Beats.Count == 0) break;
                    var _beat = Beats.First();
                    var _time = DateTime.Now;

                    if (TotalSeconds - 0.0f + 1.30f >= _beat.seconds)
                    {
                        Beats.RemoveAt(0);
                        Debug.Log(_beat.seconds);
                        var _food = Instantiate(Food, Panel);
                        _food.GetComponent<FoodManager>().Creat(_beat.direction);
                        continue;
                    }
                }
                catch (Exception ex)
                {
                    PopupManager.PopMessage("错误", ex.Message);
                    AppLogger.Log(ex.Message);
                    throw;
                }
                yield return new WaitForSeconds(0.005f);
            }

            while (AudioPlayer.isPlaying)
            {
                yield return new WaitForSeconds(0.005f);
            }

            Button_Back.SetActive(true);
            GamePanel_Mask.SetActive(true);
            GameObject.Find("MainCanvas/GamePanel-Mask/Panel").GetComponent<RectTransform>().DOAnchorPos3D(new Vector3(0f, 0f, 0f), 0.5f);
            StopAllCoroutines();
        }
        private IEnumerator TimeEvent ()
        {
            while (true)
            {
                Tween tween = DOTween.To(() => TotalSeconds, x => TotalSeconds = x, TotalSeconds + 1, 1f).SetEase(Ease.Linear);
                yield return new WaitForSeconds(1);
            }
        }
        public void Button_Click_Close ()
        {
            var _ = new LoadingSceneManager<string>("Game-Lobby");
        }


    }
}


