using System;
using System.Collections.Generic;
using TetraCreations.Attributes;
using UnityEngine;
using UnityEngine.UI;
namespace ScenesScripts.FocusClock
{
    public class TimeListItemHighlight : MonoBehaviour
    {
        private Scrollbar ScrollbarTimes;
        [Title("显示时间选项的父物体")]
        public GameObject Content;
        public List<GameObject> SelectItems = new();
        public enum TimeType_Enum
        {
            minutes,
            seconds,
        }
        public TimeType_Enum TimeType;
        private void Awake ()
        {
            ScrollbarTimes = this.gameObject.GetComponent<Scrollbar>();
            foreach (Transform child in Content.transform)
            {
                SelectItems.Add(child.gameObject);
            }
        }
        private void Update ()
        {
            var _progress_value = ScrollbarTimes.value;
            if (_progress_value < 0) _progress_value = 0;
            if (_progress_value > 1) _progress_value = 1;

            var name = $"Text-{Math.Floor(8 - _progress_value / (1 / 0.7) * 10)}";

            if (name == "Text-8") name = "Text-7";
            if (name == "Text-0") name = "Text-1";

            var _higt_obj = SelectItems.Find(e => e.name == name);

            _higt_obj.GetComponent<Text>().color = Color.blue;
            if (TimeType == TimeType_Enum.minutes)
            {
                FocusClockManager.TimeInof.minutes = Convert.ToInt32(_higt_obj.GetComponent<Text>().text);
            }
            else
            {
                FocusClockManager.TimeInof.seconds = Convert.ToInt32(_higt_obj.GetComponent<Text>().text);
            }

            foreach (var item in SelectItems)
            {
                if (item != _higt_obj)
                {
                    var _text_com = item.GetComponent<Text>();
                    _text_com.color = Color.black;

                }
            }
            Debug.Log(FocusClockManager.TimeInof.minutes + "  " + FocusClockManager.TimeInof.seconds);
        }
    }

}
