using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace ScenesScripts.LOGOStart
{
    /// <summary>
    /// 开场界面LOGO的控制
    /// </summary>
    public class Logo : MonoBehaviour
    {
        public Image LogoImage;
        public float delayTime = 1.5f;
        private void Start ()
        {
            Instantiate(Resources.Load<GameObject>("GameObject/Server/ServerManager"));
            Application.targetFrameRate = 60;
            StartCoroutine(LoadEvent());
        }
        private IEnumerator LoadEvent ()
        {
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(1);
            asyncOperation.allowSceneActivation = false; // 禁止自动跳转
            while (!asyncOperation.isDone && asyncOperation.progress < 0.9f)
            {

                yield return null;
            }
            yield return new WaitForSeconds(delayTime);//动画
            LogoImage.DOFade(1, 1.5f);
            yield return new WaitForSeconds(1.5f);//消失的事件
            asyncOperation.allowSceneActivation = true; // 允许自动跳转
        }
    }

}