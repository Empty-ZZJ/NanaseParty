using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
namespace ScenesScripts.GalPlot
{
    public class GalController_BlackWait : MonoBehaviour
    {
        public Image image;
        private void Start ()
        {
            image.DOColor(new Color(0, 0, 0, 1), 0.5f).OnComplete(() =>
            {
                image.DOColor(new Color(0, 0, 0, 0), 0.5f).OnComplete(() =>
                {
                    Destroy(this.gameObject);
                    GameObject.Find("EventSystem").GetComponent<GalManager>().Button_Click_NextPlot();
                });
            });
        }
    }

}
