using OBJScripts;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace Common
{
    public static class PopupManager
    {
        public static GameObject PopMessage (string title, string content, Action action = null)
        {
            var _ = Resources.Load("GameObject/Popup/Popup_Message").GetComponent<PopupMessageControler>();
            _.Title.text = title;
            _.Content.text = content;
            if (action != null)
                _.Call = action;
            else
                _.OKButton.gameObject.SetActive(false);
            return MonoBehaviour.Instantiate(_.gameObject);
        }
        public static GameObject PopDialog (string title, string content, UnityAction action_OK = null, UnityAction action_cancel = null)
        {
            var _ = Resources.Load("GameObject/Popup/Popup_Dialog").GetComponent<PopupDialogControler>();
            _.Title.text = title;
            _.Content.text = content;
            if (action_OK != null)
                _.OKButton.onClick.AddListener(action_OK);
            if (action_cancel != null)
                _.CancelButton.onClick.AddListener(action_cancel);
            return MonoBehaviour.Instantiate(_.gameObject);
        }
    }
}