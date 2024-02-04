using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Common.UI
{
    /// <summary>
    /// gif播放能力
    /// </summary>
    public class GifPlay : MonoBehaviour
    {
        private static Sprite[] LoadingImg;
        private int index = 1;


        private void OnEnable ()
        {
            if (LoadingImg == null)
            {
                LoadingImg = new Sprite[61];
                for (int i = 1; i <= 60; i++)
                {

                    LoadingImg[i] = Resources.Load<Sprite>("Texture2D/Loading/" + i.ToString());
                }
            }
            StartCoroutine(StartAction());
        }

        private void OnDisable ()
        {
            StopCoroutine(StartAction());
        }

        public IEnumerator StartAction ()
        {
            while (true)
            {

                if (index <= 60)
                {
                    // this.GetComponent<Image>().sprite = Resources.Load<Sprite>("Texture2D/Loading/" + index.ToString());
                    gameObject.GetComponent<Image>().sprite = LoadingImg[index];
                    index++;
                }
                else
                {
                    index = 1;
                    //this.GetComponent<Image>().sprite = Resources.Load<Sprite>("Texture2D/Loading/" + index.ToString());
                    gameObject.GetComponent<Image>().sprite = LoadingImg[index];
                }
                yield return new WaitForSeconds(0.01f);
            }
        }
    }
}

