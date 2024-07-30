using UnityEngine;
using UnityEngine.UI;

namespace ScenesScripts.GalPlot
{

    public class GalController_CharacterImg : MonoBehaviour
    {
        private Image CharacterImg;
        private void Start ()
        {
            CharacterImg = this.gameObject.GetComponent<Image>();

        }
        /// <summary>
        /// 直接传递图片
        /// </summary>
        /// <param name="ImgSprite"></param>
        public void SetImage (Sprite ImgSprite)
        {
            CharacterImg.sprite = ImgSprite;
        }
        /// <summary>
        /// 从Resources资源文件夹读图片
        /// </summary>
        /// <param name="ImgSpriteFilePath"></param>
        public void SetImage (string ImgSpriteFilePath)
        {
            CharacterImg.sprite = Resources.Load<Sprite>(ImgSpriteFilePath);
        }
    }
}