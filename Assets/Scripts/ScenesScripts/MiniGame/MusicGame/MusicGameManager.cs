using Common;
using DG.Tweening;
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
        private static Queue<MusicBeat> Beats = new();
        private static List<Sprite> Foods = new();
        private static GameObject Food;
        public double TotalSeconds;
        public void Start ()
        {
            for (int i = 1; i <= 7; i++)
            {
                Foods.Add(Resources.Load<Sprite>($"Texture2D/MusicGame/food{i}"));
            }
            Food = Resources.Load<GameObject>("GameObject/Scene/MiniGame/MusicGame/Food");
            Beats = JsonConvert.DeserializeObject<Queue<MusicBeat>>(Resources.Load<TextAsset>("Audio/MenheraMusic/bets/bets6").text);
            Debug.Log("节拍数：" + Beats.Count);
        }
        public void Button_Click_StartGame ()
        {
            var _audioclip = Resources.Load<AudioClip>("Audio/MenheraMusic/music/music (6)");
            AudioPlayer.clip = _audioclip;
            AudioPlayer.Play();
            TotalSeconds = 0;
            StartCoroutine(CreatBreats());
            StartCoroutine(TimeEvent());
        }
        private IEnumerator CreatBreats ()
        {

            while (true)
            {
                if (Beats.Count == 0) break;
                var _beat = Beats.First();
                var _time = DateTime.Now;

                if (TotalSeconds - 0.0f + 1.50f >= _beat.seconds)
                {
                    Beats.Dequeue();
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
    }
}


