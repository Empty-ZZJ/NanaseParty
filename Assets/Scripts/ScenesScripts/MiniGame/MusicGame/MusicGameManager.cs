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
using UnityEngine.UI;
namespace ScenesScripts.MiniGame.MusicGame
{
    public class MusicGameManager : MonoBehaviour
    {
        [Title("生成面板")]
        public Transform Panel;
        public GameObject Left_Button;
        public GameObject Right_Button;
        public AudioSource AudioPlayer;
        private static List<MusicBeat> Beats = new();
        private static List<Sprite> Foods = new();
        private static GameObject Food;
        public double TotalSeconds;
        public void Start ()
        {
            try
            {
                for (int i = 1; i <= 7; i++)
                {
                    Foods.Add(Resources.Load<Sprite>($"Texture2D/MusicGame/food{i}"));
                }
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
        public void Button_Click_StartGame ()
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
                    Destroy(GameObject.Find("MainCanvas/GamePanel-Mask"));
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

                    if (TotalSeconds - 0.0f + 1.50f >= _beat.seconds)
                    {
                        Beats.RemoveAt(0);
                        Debug.Log(_beat.seconds);
                        var _food = Instantiate(Food, Panel);
                        _food.GetComponent<Image>().sprite = Foods[GameAPI.GetRandomInAB(0, Foods.Count - 1)];
                        if (_beat.direction == 1)
                        {
                            _food.GetComponent<RectTransform>().DOMove(Left_Button.transform.position, 1.5f).OnComplete(() =>
                            {
                                Destroy(_food, 2f);
                            }).SetEase(Ease.Linear);
                        }
                        else
                        {
                            _food.GetComponent<RectTransform>().DOMove(Right_Button.transform.position, 1.5f).OnComplete(() =>
                            {
                                Destroy(_food, 2f);
                            }).SetEase(Ease.Linear);

                        }
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


