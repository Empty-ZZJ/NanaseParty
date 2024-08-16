using DG.Tweening;
using GameManager;
using UnityEngine;
namespace ScenesScripts.MiniGame.MusicGame
{
    public class PausePanelManager : MonoBehaviour
    {
        public RectTransform Popup;
        private void Start ()
        {
            MusicGameManager.IsPlaying = false;
        }
        public void Button_Click_Retry ()
        {
            Popup.DOScale(0, 0.3f).OnComplete(() =>
            {
                GameObject.Find("EventSystem").GetComponent<MusicGameManager>().MusicOver(); Destroy(this.gameObject);
            });
        }
        public void Button_Click_Close ()
        {
            Popup.DOScale(0, 0.3f).OnComplete(() => { var _ = new LoadingSceneManager<string>("Game-Lobby"); });

        }
        public void Button_Click_Continue ()
        {
            Popup.DOScale(0, 0.3f).OnComplete(() =>
            {
                GameObject.Find("EventSystem").GetComponent<MusicGameManager>().AudioPlayer.Play();
                MusicGameManager.IsPlaying = true;
                Destroy(this.gameObject);

            });

        }
    }

}
