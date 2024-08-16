using Common;
using Common.SupperComponent;
using DG.Tweening;
using TetraCreations.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace ScenesScripts.GalPlot
{
    public class GalController_CharacterAnimate : MonoBehaviour
    {
        /// <summary>
        /// 出入场出场动画
        /// </summary>
        [StringInList("ToShow", "Outside-ToLeft", "Outside-ToRight")] public string Animate_StartOrOutside = "ToShow";
        /// <summary>
        /// 动画
        /// <para>Shake：颤抖</para>
        /// <para>Shake-Y-Once：向下抖动一次</para>
        /// <para>ToGrey：变灰</para>
        /// <para>To - ：不解释了，移动到指定位置</para>
        /// </summary>
        [StringInList("Shake", "Shake-Y-Once", "ToLeft", "ToCenter", "ToRight")] public string Animate_type = "Shake";
        /// <summary>
        /// 角色立绘
        /// </summary>
        private Image CharacterImg;
        [Title("注意，主画布的名称必须是MainCanvas")]
        public Canvas MainCanvas;
        private void Awake ()
        {
            CharacterImg = this.gameObject.GetComponent<Image>();
            if (MainCanvas == null) MainCanvas = GameObject.Find("MainCanvas").GetComponent<Canvas>();
        }
        [Button(nameof(Start), "重新执行入场动画")]
        private void Start ()
        {
            HandleInOrOutsideMessgae(Animate_StartOrOutside);

        }
        [Button(nameof(Start), "重新执行及时动画")]
        public void HandleMessgae ()
        {
            var _rect = CharacterImg.GetComponent<RectTransform>();
            switch (Animate_type)
            {
                case "Shake":
                {
                    _rect.DOShakePosition(0.5f, 30f);
                    break;
                }
                case "Shake-Y-Once":
                {
                    _rect.DOAnchorPosY(_rect.anchoredPosition.y - 50f, 0.6f).OnComplete(() =>
                    {
                        _rect.DOAnchorPosY(_rect.anchoredPosition.y + 50f, 0.6f);
                    });
                    break;
                }
                case "ToLeft":
                {
                    DOTween.To(() => _rect.anchoredPosition, x => _rect.GetComponent<RectTransform>().anchoredPosition = x, PositionImageInside(_rect, -1), 1f);
                    break;
                }
                case "ToCenter":
                {
                    DOTween.To(() => _rect.anchoredPosition, x => _rect.GetComponent<RectTransform>().anchoredPosition = x, PositionImageInside(_rect, 0), 0.8f);
                    break;
                }
                case "ToRight":
                {
                    DOTween.To(() => _rect.anchoredPosition, x => _rect.GetComponent<RectTransform>().anchoredPosition = x, PositionImageInside(_rect, 1), 1f);
                    break;
                }
                case "Quit":
                {
                    CharacterImg.DOFade(0, 0.7f).OnComplete(() =>
                    {
                        Destroy(this.gameObject);
                    });
                    break;
                }
                default:
                {
                    PopupManager.PopMessage("致命错误", "当前剧情文本受损，请重新安装游戏尝试。若故障仍旧请联系客服处理。邮箱：ceo@hoilai.com");
                    break;
                }
            }
        }
        /// <summary>
        /// 处理出场动画消息
        /// </summary>
        /// <param name="Messgae"></param>
        public void HandleInOrOutsideMessgae (string Messgae)
        {

            CharacterImg.color = new Color32(255, 255, 255, 0);//完全透明
            var rect = this.gameObject.GetComponent<RectTransform>();
            switch (Messgae)
            {

                //逐渐显示
                case "ToShow":
                {

                    PositionImageOutside(this.gameObject.GetComponent<RectTransform>(), 0);
                    break;
                }
                //从屏幕边缘滑到左侧
                case "Outside-ToLeft":
                {

                    PositionImageOutside(this.gameObject.GetComponent<RectTransform>(), -1);
                    DOTween.To(() => rect.anchoredPosition, x => rect.GetComponent<RectTransform>().anchoredPosition = x, new Vector2(rect.anchoredPosition.x + CharacterImg.sprite.texture.width, rect.anchoredPosition.y), 1f);
                    break;
                }
                //从屏幕边缘滑到右侧
                case "Outside-ToRight":
                {
                    PositionImageOutside(this.gameObject.GetComponent<RectTransform>(), 1);
                    DOTween.To(() => rect.anchoredPosition, x => rect.GetComponent<RectTransform>().anchoredPosition = x, new Vector2(rect.anchoredPosition.x - CharacterImg.sprite.texture.width, rect.anchoredPosition.y), 1f);
                    break;
                }
                default:
                {
                    PopupManager.PopMessage("致命错误", "当前剧情文本受损，请重新安装游戏尝试。若故障仍旧请联系客服处理。邮箱：ceo@hoilai.com");
                    break;
                }

            }
            //都需要指定的
            {
                CharacterImg.DOFade(1, 0.7f);
            }

        }
        /// <summary>
        /// 设置image的位置到屏幕之外
        /// </summary>
        /// <param name="ImageGameObject"></param>
        /// <param name="Position">-1：左侧 0：中间 1：右侧</param>
        private void PositionImageOutside (RectTransform ImageGameObject, int Position)
        {
            // 获取Image的Rect Transform
            switch (Position)
            {
                case -1:
                    this.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2((-MainCanvas.GetComponent<RectTransform>().sizeDelta.x / 2) - (ImageGameObject.gameObject.GetComponent<Image>().sprite.texture.width / 2), ImageGameObject.anchoredPosition.y);
                    break;
                case 1:

                    this.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2((MainCanvas.GetComponent<RectTransform>().sizeDelta.x / 2) + (ImageGameObject.gameObject.GetComponent<Image>().sprite.texture.width / 2), ImageGameObject.anchoredPosition.y);
                    break;
                case 0:
                    this.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, ImageGameObject.anchoredPosition.y);
                    break;
                default: break;
            }
        }
        /// <summary>
        /// 获取image的位置到屏幕之内的位置
        /// </summary>
        /// <param name="ImageGameObject"></param>
        /// <param name="Position">-1：左侧 0：中间 1：右侧</param>
        private Vector2 PositionImageInside (RectTransform ImageGameObject, int Position)
        {
            // 获取Image的Rect Transform

            switch (Position)
            {
                case -1:
                    return new Vector2((-MainCanvas.GetComponent<RectTransform>().sizeDelta.x / 2) + (ImageGameObject.gameObject.GetComponent<Image>().sprite.texture.width / 2), ImageGameObject.anchoredPosition.y);

                case 1:
                    return new Vector2((MainCanvas.GetComponent<RectTransform>().sizeDelta.x / 2) - (ImageGameObject.gameObject.GetComponent<Image>().sprite.texture.width / 2), ImageGameObject.anchoredPosition.y);

                case 0:
                    return new Vector2(0, ImageGameObject.anchoredPosition.y);

                default:
                {
                    PopupManager.PopMessage("致命错误", "当前剧情文本受损，请重新安装游戏尝试。若故障仍旧请联系客服处理。邮箱：ceo@hoilai.com");
                    return new Vector2(0, 0);
                }
            }
        }
    }

}
