using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
namespace ScenesScripts.MiniGame.MusicGame
{
    public class OverPanelController : MonoBehaviour
    {
        public Text ScoreText;
        public RectTransform Popup;
        private void Start ()
        {

            ScoreText.text = MusicGameManager.Score + " ·Ö";
        }
        public void Button_Click_Close ()
        {
            Popup.DOScale(0, 0.3f).OnComplete(() =>
            {
                GameObject.Find("EventSystem").GetComponent<MusicGameManager>().GamePanel_Mask.SetActive(true);
                GameObject.Find("MainCanvas/GamePanel-Mask/Panel").GetComponent<RectTransform>().DOAnchorPos3D(new Vector3(0f, 0f, 0f), 0.5f);
                Destroy(this.gameObject);

            });
        }


    }

}
