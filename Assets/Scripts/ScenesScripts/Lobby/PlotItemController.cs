using TetraCreations.Attributes;
using UnityEngine;
using UnityEngine.UI;
namespace ScenesScripts.Lobby
{
    public class PlotItemController : MonoBehaviour
    {
        /// <summary>
        /// by：张子健 2024年7月30日 16:29:41
        /// </summary>
        [Title("当前剧情item绑定的剧情ID，不建议手动输入")]
        public string ID;
        public Image PlotImg;
        public Text PlotText;
        public string Description;
        private GameObject PlotInfo;

        private void Start ()
        {
            PlotInfo = Resources.Load<GameObject>("GameObject/Scene/UIMain/Plot/PlotInfoCanvas");
        }
        private void Update ()
        {

        }
        public void Button_Click_Select ()
        {
            var _data = Instantiate(PlotInfo).GetComponent<PlotIStartterManager>();
            _data.Title.text = PlotText.text;
            _data.Description.text = PlotText.text;
            _data.PlotImg.sprite = PlotImg.sprite;

        }
    }
}
