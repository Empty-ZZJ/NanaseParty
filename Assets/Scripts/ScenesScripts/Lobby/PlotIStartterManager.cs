using Common.UI;
using DG.Tweening;
using GameManager;
using System.Threading.Tasks;
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
        public async void Button_Click_Start ()
        {
            var _loading = new ShowLoading("Мгдижа");
            await Task.Delay(1000);
            var _loadscene = new LoadingSceneManager<string>("Gal_Common");
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
