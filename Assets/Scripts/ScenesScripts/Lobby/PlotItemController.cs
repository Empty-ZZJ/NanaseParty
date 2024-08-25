using GameManager;
using Newtonsoft.Json;
using ScenesScripts.GalPlot;
using System.Linq;
using System.Xml.Linq;
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
        private static GameObject PlotInfo;
        /// <summary>
        /// 该剧情是否解锁
        /// </summary>
        public bool IsEnable = true;
        /// <summary>
        /// 奖励
        /// </summary>
        public XElement XE_Reward;
        /// <summary>
        /// 解锁条件
        /// </summary>
        public XElement EnableCondition;
        /// <summary>

        private void Start ()
        {
            foreach (var item in EnableCondition.Elements())
            {
                if (EnableCondition.Elements().Count() == 0)
                {
                    Debug.Log($"无条件解锁：{PlotText.text}");
                    break;
                }
                if (item.Name == "plot")//前置剧情
                {
                    if (GameDataManager.GameData.PlotData is null)
                    {
                        IsEnable = false;
                        continue;
                    }

                    var _ = GameDataManager.GameData.PlotData.Find(e => e.id == item.Value);
                    if (_ is null)
                    {
                        IsEnable = false;
                        continue;
                    }
                    if (_.isDone != true)
                    {
                        IsEnable = false;
                        continue;
                    }
                }
                if (item.Name == "love")//好感度条件
                {

                    float.TryParse(item.Value, out var _condition);
                    if (_condition >= GameDataManager.GameData.LoveLevel)//不满足条件
                    {
                        IsEnable = false;
                    }
                }

            }
            /*
            if (!IsEnable)//不满足解锁条件
            {
                PlotImg.sprite = Resources.Load<Sprite>("Texture2D/Gal/lock");
                PlotText.text += "(未解锁)";

            }*/


            if (PlotInfo == null)
                PlotInfo = Resources.Load<GameObject>("GameObject/Scene/UIMain/Plot/PlotInfoCanvas");

        }
        private void Update ()
        {

        }
        public void Button_Click_Select ()
        {
            var _data = Instantiate(PlotInfo).GetComponent<PlotIStartterManager>();
            _data.Title.text = PlotText.text;
            _data.Description.text = Description;
            _data.PlotImg.sprite = PlotImg.sprite;
            PlotIStartterManager.ID = ID;
            GalManager.PlotData.XE_Reward = XE_Reward;
            Debug.Log(JsonConvert.SerializeObject(XE_Reward));


        }
    }
}
