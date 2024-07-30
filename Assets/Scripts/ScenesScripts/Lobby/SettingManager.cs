using Common;
using OBJScripts;
using TetraCreations.Attributes;
using UnityEngine;
namespace ScenesScripts.Lobby
{
    public class SettingManager : MonoBehaviour
    {
        [Title("创建页面的内容空间")]
        public Transform Content;
        private GameObject PageItem;
        private void Start ()
        {
            Button_Click_Switch("USetting");
            MenuComponentController.id = "Button_Setting";
        }

        public void Button_Click_Switch (string pageName)
        {
            for (int i = 0; i < Content.childCount; i++)
            {
                var transform = Content.GetChild(i);
                Destroy(transform.gameObject);
            }
            switch (pageName)
            {
                case "USetting":
                {
                    PageItem = Instantiate(Resources.Load<GameObject>("GameObject/Scene/UIMain/Setting/Conent-Usetting"), Content);

                    break;
                }
                case "SpecialSetting":
                {
                    PageItem = Instantiate(Resources.Load<GameObject>("GameObject/Scene/UIMain/Setting/Content_Special"), Content);

                    break;
                }
                case "Need":
                {
                    PageItem = Instantiate(Resources.Load<GameObject>("GameObject/Scene/UIMain/Setting/Conent-Need"), Content);
                    break;
                }
                case "About":
                {
                    PageItem = Instantiate(Resources.Load<GameObject>("GameObject/Scene/UIMain/Setting/Conent-About"), Content);
                    break;
                }
                default:
                {
                    PopupManager.PopMessage("警告", "意外错误");
                    AppLogger
                    .Log($"错误的页面切换:{pageName}");
                    break;
                }
            }
            Content.GetComponent<RectTransform>().sizeDelta = new Vector2(Content.GetComponent<RectTransform>().sizeDelta.x, PageItem.GetComponent<RectTransform>().sizeDelta.y);

        }
    }

}
