using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
namespace ScenesScripts.Lobby
{
    public class PlotIStartterManager : MonoBehaviour
    {
        public Text Title;
        public Text Description;
        public Image PlotImg;
        public static string ID;
        public RectTransform Panel;
        public void Button_Click_Start ()
        {

        }
        public void Button_Click_Close ()
        {
            Panel.DOScale(0, 0.3f).OnComplete(() =>
            {
                Destroy(this.gameObject);
            });
        }
    }

}
