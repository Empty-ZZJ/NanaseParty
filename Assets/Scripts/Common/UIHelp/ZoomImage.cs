using TetraCreations.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace Common.UI
{
    /// <summary>
    /// 图片按比例缩放
    /// </summary>
    public class ZoomImage : MonoBehaviour
    {
        private Image _Image;
        private AspectRatioFitter _AspectRatioFitter;
        private Sprite lastSprite;
        private void Start ()
        {

            _Image = GetComponent<Image>();
            _AspectRatioFitter = GetComponent<AspectRatioFitter>();
            if (_AspectRatioFitter == null) _AspectRatioFitter = this.gameObject.AddComponent<AspectRatioFitter>();
            _AspectRatioFitter.aspectMode = AspectRatioFitter.AspectMode.HeightControlsWidth;
            lastSprite = _Image.sprite;
            RedsizeImage();
        }
        private void Update ()
        {
            if (Application.isPlaying && lastSprite != _Image.sprite)
            {
                lastSprite = _Image.sprite;
                RedsizeImage();
            }
        }
        [Button(nameof(RedsizeImage), "重新设置比例")]
        public void RedsizeImage ()
        {
            if (GetComponent<Image>().sprite == null) return;
            _AspectRatioFitter.aspectRatio = _Image.sprite.texture.width / (float)_Image.sprite.texture.height;
        }

    }

}
