using System.Collections;
using System.Collections.Generic;
using TetraCreations.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace ScenesScripts.MiniGame.MusicGame
{
    public class BoomClickEfficeManager : MonoBehaviour
    {
        private static List<Sprite> circleEfficts = new();
        public Image circle;
        private int index = 0;
        private static AudioClip[] Clip_Xiao;
        public AudioSource audioSource;
        private void Start ()
        {

            if (Clip_Xiao == null)
            {
                Clip_Xiao = new AudioClip[10];
                for (int i = 1; i <= 1; i++)
                {
                    Clip_Xiao[i] = Resources.Load<AudioClip>($"Audio/MiniGame/MusicGame/xiao{i}");
                    Debug.Log($"Audio/MiniGame/MusicGame/xiao{i}");
                }
            }
            if (audioSource == null) audioSource = gameObject.GetComponent<AudioSource>();

            audioSource.PlayOneShot(Clip_Xiao[1]);


            if (circleEfficts.Count == 0)
            {
                for (int i = 1; i <= 12; i++)
                {
                    circleEfficts.Add(Resources.Load<Sprite>($"Texture2D/XiaoXiaoLe/effict/{i}"));
                }
                Debug.Log("销毁动画数量: " + circleEfficts.Count);
            }
            StartCoroutine(StartAction());
        }
        public IEnumerator StartAction ()
        {
            while (true)
            {

                if (index <= circleEfficts.Count - 1)
                {
                    circle.sprite = circleEfficts[index];
                    index++;
                }
                else break;

                yield return new WaitForSeconds(0.02f);
            }
            Destroy(this.gameObject);
        }
        [Button(nameof(ReStart), "重新开始销毁动画")]
        public void ReStart ()
        {
            index = 0;
            StartCoroutine(StartAction());
        }

    }
}