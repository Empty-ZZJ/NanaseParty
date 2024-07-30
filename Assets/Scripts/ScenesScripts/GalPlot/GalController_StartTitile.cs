using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
namespace ScenesScripts.GalPlot
{
    public class GalController_StartTitile : MonoBehaviour
    {
        public Image Imageframe;
        public Text Title;
        public void Start ()
        {
            Invoke(nameof(Close), 2f);
        }
        public void ShowTitle (string title, Sprite back)
        {
            Title.text = title;

            this.gameObject.GetComponent<RawImage>().texture = back.texture;
        }
        public void Close ()
        {
            Imageframe.DOFade(0, 0.7f);
            Title.DOFade(0, 0.7f).OnComplete(() =>
            {
                var _ = GameObject.Find("EventSystem").GetComponent<GalManager>();
                _.CloseTitleAnimate_Recall();
                Destroy(this.gameObject);
            });
        }
    }

}
