//
//                       _oo0oo_
//                      o8888888o
//                      88" . "88
//                      (| -_- |)
//                      0\  =  /0
//                    ___/`---'\___
//                  .' \\|     |// '.
//                 / \\|||  :  |||// \
//                / _||||| -:- |||||- \
//               |   | \\\  -  /// |   |
//               | \_|  ''\---/''  |_/ |
//               \  .-\__  '-'  ___/-. /
//             ___'. .'  /--.--\  `. .'___
//          ."" '<  `.___\_<|>_/___.' >' "".
//         | | :  `- \`.;`\ _ /`;.`/ - ` : | |
//         \  \ `_.   \_ __\ /__ _/   .-` /  /
//     =====`-.____`.___ \_____/___.-`___.-'=====
//                       `=---='
//
//     ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
//               佛祖保佑         永无BUG
//    佛曰:
//              写字楼里写字间，写字间里程序员；
//              程序人员写程序，又拿程序换酒钱。
//              酒醒只在网上坐，酒醉还来网下眠；
//              酒醉酒醒日复日，网上网下年复年。
//              但愿老死电脑间，不愿鞠躬老板前；
//              奔驰宝马贵者趣，公交自行程序员。
//              别人笑我忒疯癫，我笑自己命太贱；
//              不见满街漂亮妹，哪个归得程序员？
using UnityEngine;
using UnityEngine.UI;
namespace ScenesScripts.MiniGame.JumpGame

{
    public class Maplogic : MonoBehaviour
    {
        //public float SCROLL_SPEED = 3f;
        //GameSpeed
        [SerializeField] private RectTransform[] backgrounds;

        private float[] imageWidths;
        private Vector2[] originalPositions;

        private void Awake ()
        {
            imageWidths = new float[backgrounds.Length];
            originalPositions = new Vector2[backgrounds.Length];

            for (int i = 0; i < backgrounds.Length; i++)
            {
                RectTransform rt = backgrounds[i];
                imageWidths[i] = rt.rect.width;
                originalPositions[i] = rt.anchoredPosition;
            }
        }

        private void Update ()
        {
            if (MiniGameRunStruct_Jump.GameState)
                for (int i = 0; i < backgrounds.Length; i++)
                {
                    RectTransform rt = backgrounds[i];
                    Vector2 pos = rt.anchoredPosition;

                    float newPos = pos.x - (MiniGameRunStruct_Jump.GameSpeed * Time.deltaTime);

                    // Wrap the image around when it goes off-screen
                    if (newPos <= -imageWidths[i])
                    {
                        newPos += imageWidths[i] * backgrounds.Length;
                    }

                    rt.anchoredPosition = new Vector2(newPos, pos.y);
                }
        }

        public void ChangeNextBackground (Sprite newBackground)
        {
            // Wait until all backgrounds are off the screen
            bool changed = false;
            foreach (var background in backgrounds)
            {
                if (background.anchoredPosition.x > -(imageWidths[0] - 10f))
                {
                    changed = true;
                    break;
                }
            }
            if (!changed)
            {
                // Find the last background image and set its sprite to the new background
                RectTransform lastBackground = backgrounds[backgrounds.Length - 1];
                Image lastBackgroundImage = lastBackground.GetComponent<Image>();
                lastBackgroundImage.sprite = newBackground;

                // Shift the background image positions by one
                for (int i = backgrounds.Length - 1; i > 0; i--)
                {
                    backgrounds[i].anchoredPosition = backgrounds[i - 1].anchoredPosition;
                }

                // Set the position of the first background image to the right of the screen
                Vector2 firstBackgroundPos = backgrounds[0].anchoredPosition;
                firstBackgroundPos.x += imageWidths[0] * backgrounds.Length;
                backgrounds[0].anchoredPosition = firstBackgroundPos;

                // Shift the imageWidths and originalPositions arrays by one
                float firstImageWidth = imageWidths[0];
                Vector2 firstOriginalPos = originalPositions[0];
                for (int i = 0; i < backgrounds.Length - 1; i++)
                {
                    imageWidths[i] = imageWidths[i + 1];
                    originalPositions[i] = originalPositions[i + 1];
                }
                imageWidths[backgrounds.Length - 1] = firstImageWidth;
                originalPositions[backgrounds.Length - 1] = firstOriginalPos;
            }
        }
    }
}