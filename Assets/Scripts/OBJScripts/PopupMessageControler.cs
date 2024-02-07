using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace OBJScripts
{
    public class PopupMessageControler : MonoBehaviour
    {
        public Text Title;
        public Text Content;
        public Button OKButton;
        public RectTransform MainPopup;
        public void Close ()
        {
            MainPopup.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InQuint).OnComplete(() =>
            {
                Destroy(this.gameObject);
            });

        }
        public void Close_Parent ()
        {
            MainPopup.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InQuint).OnComplete(() =>
            {
                Destroy(this.gameObject.transform.parent.gameObject);
            });
        }
        public void SetValue (string section_key_value)
        {
            string[] _strings = section_key_value.Split('_');
            GameManager.ServerManager.Config.GameCommonConfig.SetValue(_strings[0], _strings[1], _strings[2]);
        }
    }
}