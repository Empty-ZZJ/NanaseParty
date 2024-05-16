using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace OBJScripts
{
    public class PopupDynamicIsland : MonoBehaviour
    {
        public Text Title;
        public Text Content;
        public Sprite ICON;
        public Image Image_ICON;
        public RectTransform MainPopup;

        private void Start ()
        {

            if (ICON == null)
            {
                Destroy(Image_ICON.gameObject);
            }
            Invoke(nameof(Close), 2f);
        }
        public void Close ()
        {
            MainPopup.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InQuint).OnComplete(() =>
            {
                MainPopup.DOAnchorPos3D(new Vector3(0, 120, 0), 0.5f).OnComplete(() =>
                {
                    Destroy(this.gameObject.transform.parent.gameObject);
                });
            });

        }
    }
}