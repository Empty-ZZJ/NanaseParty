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
                    if (p.Name == "img") _plotitem.img = $"Texture2D/Menhera/Plot/{p.Value}";
                    if (p.Name == "id") _plotitem.id = p.Value;
                }


                //list和实际panel不绑定。
                PlotInfo.Add(_plotitem);



                var _obj = Instantiate(PlotItem, this.gameObject.transform).GetComponent<PlotItemController>();
                _obj.PlotText.text = _plotitem.title;
                _obj.ID = _plotitem.id;
                _obj.PlotImg.sprite = Resources.Load<Sprite>(_plotitem.img);
                _obj.Description = _plotitem.description;
            }
            Debug.Log($"剧情数：{PlotInfo.Count}");

        }
    }

}
