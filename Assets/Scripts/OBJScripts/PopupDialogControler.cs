using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace OBJScripts
{
    public class PopupDialogControler : MonoBehaviour
    {
        public Text Title;
        public Text Content;
        public Button OKButton;
        public Button CancelButton;
        public RectTransform MainPopup;
        public void Close ()
        {
            MainPopup.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InQuint).OnComplete(() =>
            {
                Destroy(this.gameObject);
            });

        }
    }
}