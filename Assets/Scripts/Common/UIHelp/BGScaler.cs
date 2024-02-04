using UnityEngine;
namespace Common.UI
{
    /// <summary>
    ///全屏背景图片等比例拉伸自适应
    /// </summary>
    [ExecuteInEditMode]
    public class BGScaler : MonoBehaviour
    {
        /// <summary>
        /// 图片原大小(压缩前的)
        /// </summary>
        public Vector2 textureOriginSize = new Vector2(2048, 1024);

        private void Awake ()
        {
            Scaler();
        }

        /// <summary>
        /// 适配
        /// </summary>
        private void Scaler ()
        {
            //当前画布尺寸
            Vector2 canvasSize = gameObject.GetComponentInParent<Canvas>().GetComponent<RectTransform>().sizeDelta;
            //当前画布尺寸长宽比
            float screenxyRate = canvasSize.x / canvasSize.y;

            //图片尺寸 这个得到的结果是 (0,0) ?
            //Vector2 bgSize = bg.mainTexture.texelSize;
            Vector2 bgSize = textureOriginSize;
            //视频尺寸长宽比
            float texturexyRate = bgSize.x / bgSize.y;

            RectTransform rt = (RectTransform)transform;
            //视频x偏长,需要适配y（下面的判断 '>' 改为 '<' 就是视频播放器的视频方式）
            if (texturexyRate > screenxyRate)
            {
                int newSizeY = Mathf.CeilToInt(canvasSize.y);
                int newSizeX = Mathf.CeilToInt((float)newSizeY / bgSize.y * bgSize.x);
                rt.sizeDelta = new Vector2(newSizeX, newSizeY);
            }
            else
            {
                int newVideoSizeX = Mathf.CeilToInt(canvasSize.x);
                int newVideoSizeY = Mathf.CeilToInt((float)newVideoSizeX / bgSize.x * bgSize.y);
                rt.sizeDelta = new Vector2(newVideoSizeX, newVideoSizeY);
            }
        }

        private void Update ()
        {
#if UNITY_EDITOR
            //editor模式下测试用
            Scaler();
#endif
        }

    }


}
