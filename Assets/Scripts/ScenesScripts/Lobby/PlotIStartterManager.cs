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
        private Camera mainCamera;
        public Text Title;
        public Text Description;
        public Image PlotImg;
        public static string ID;
        public RectTransform Panel;
        private void Start ()
        {
            mainCamera = Camera.main;
        }
        public async void Button_Click_Start ()
        {
            // 获取主摄像机
            var _loading = new ShowLoading("加载中");
            await Task.Delay(1000);
            var _loadscene = new LoadingSceneManager<string>("Gal_Common");
        }
        public void Button_Click_Close ()
        {
            Panel.DOScale(0, 0.3f).OnComplete(() =>
            {
                Destroy(this.gameObject);
            });
            var ray = mainCamera.ScreenPointToRay(Input.mousePosition); // 从摄像机屏幕点到鼠标位置投射射
            RaycastHit[] hit = Physics.RaycastAll(ray);
            foreach (var item in hit)
            {
                if (item.transform.gameObject.name == "Panel" || item.transform.gameObject.name == "Notice")
                {
                    Close();
                    break;
                }
            }
        }
        private void Close ()
        {

        }
    }

}
