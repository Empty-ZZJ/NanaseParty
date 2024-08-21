
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Common.UI
{
    /// <summary>
    /// 显示圆圈加载页面，本类在实例化的那一刻起就生效
    /// </summary>
    public class ShowLoading
    {
        private GameObject _GameObjectLoading;

        /// <summary>
        /// 实例化的那一刻起就生效
        /// </summary>
        /// <param name="_Title">提示文本</param>
        public ShowLoading (string _Title = "")
        {

            _GameObjectLoading = MonoBehaviour.Instantiate(Resources.Load<GameObject>("GameObject/Popup/WaitLoading"));

            if (_Title != string.Empty)
            {
                foreach (var t in _GameObjectLoading.GetComponentsInChildren<Transform>())
                {
                    if (t.name == "title")
                    {
                        var _textMeshProUGUI = t.GetComponent<Text>();
                        _textMeshProUGUI.text = _Title;
                    }
                }
            }
        }

        public void SetTitle (string _Title)
        {
            foreach (var t in _GameObjectLoading.GetComponentsInChildren<Transform>())
            {
                if (t.name == "title")
                {
                    var _textMeshProUGUI = t.GetComponent<Text>();
                    _textMeshProUGUI.text = _Title;
                }
            }
        }

        /// <summary>
        /// 关闭当前加载
        /// </summary>
        /// <returns></returns>
        public bool KillLoading ()
        {
            try
            {

                MonoBehaviour.Destroy(_GameObjectLoading.gameObject);
                return true;
            }
            catch (Exception)
            {
                //new PopNewMessage(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 重新显示加载页面
        /// </summary>
        public bool ReShowLoading ()
        {
            if (_GameObjectLoading == null)
            {
                _GameObjectLoading = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Common/Gameobject/WaitLoading"));
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 设置隐藏或显示
        /// </summary>
        /// <returns></returns>
        public bool SetActive (bool ActiveState)
        {
            if (_GameObjectLoading != null)
            {
                _GameObjectLoading.SetActive(ActiveState);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
