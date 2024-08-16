using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

namespace ScenesScripts.Lobby
{
    public class PlotListManager : MonoBehaviour
    {
        public class _PlotInfo
        {
            public string title { get; set; }
            public string description { get; set; }
            public string id { get; set; }
            public string img { get; set; }
            /// <summary>
            /// 奖励
            /// </summary>
            public XElement XE_Reward { get; set; }
            /// <summary>
            /// 解锁条件
            /// </summary>
            public XElement EnableCondition { get; set; }

        };
        public static List<_PlotInfo> PlotInfo = new();
        private GameObject PlotItem;
        private void Start ()
        {
            PlotItem = Resources.Load<GameObject>("GameObject/Scene/UIMain/Plot/StageList");
            XDocument doc = XDocument.Parse(Resources.Load<TextAsset>("Config/plots").text);
            foreach (var item in doc.Root.Elements())
            {
                var _plotitem = new _PlotInfo();

                //取节点值
                foreach (var p in item.Elements())
                {
                    if (p.Name == "title") _plotitem.title = p.Value;
                    if (p.Name == "description") _plotitem.description = p.Value;
                    if (p.Name == "img") _plotitem.img = $"Texture2D/Menhera/Plot/mask/{p.Value}";
                    if (p.Name == "id") _plotitem.id = p.Value;
                    if (p.Name == "reward") _plotitem.XE_Reward = p;
                    if (p.Name == "condition") _plotitem.EnableCondition = p;
                }


                //list和实际panel不绑定。
                PlotInfo.Add(_plotitem);


                var _obj = Instantiate(PlotItem, this.gameObject.transform).GetComponent<PlotItemController>();
                _obj.PlotText.text = _plotitem.title;
                _obj.ID = _plotitem.id;
                _obj.PlotImg.sprite = Resources.Load<Sprite>(_plotitem.img);
                _obj.Description = _plotitem.description;
                _obj.XE_Reward = _plotitem.XE_Reward;
                _obj.EnableCondition = _plotitem.EnableCondition;

            }
            Debug.Log($"剧情数：{PlotInfo.Count}");

        }
    }

}
