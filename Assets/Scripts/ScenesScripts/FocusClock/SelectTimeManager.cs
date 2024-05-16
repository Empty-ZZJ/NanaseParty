using DG.Tweening;
using TetraCreations.Attributes;
using UnityEngine;
using UnityEngine.UI;
namespace ScenesScripts.FocusClock
{
    public class SelectTimeManager : MonoBehaviour
    {
        [Title("分钟 列表")]
        public ScrollRect ScrollRect_Minute;
        [Title("分钟 内容")]
        public GameObject Content_Minute;
        private void FindCenterItem (ScrollRect scrollRect, GameObject content)
        {
            float contentHeight = content.GetComponent<RectTransform>().rect.height;
            float viewportHeight = scrollRect.viewport.rect.height;
            float centerLine = contentHeight * 0.5f - viewportHeight * 0.5f;

            float minDistance = Mathf.Infinity;


            for (int i = 0; i < content.transform.childCount; i++)
            {
                GameObject child = content.transform.GetChild(i).gameObject;
                RectTransform childRectTransform = child.GetComponent<RectTransform>();
                float childCenterY = childRectTransform.localPosition.y + childRectTransform.rect.height * 0.5f;
                float distance = Mathf.Abs(centerLine - childCenterY);

                if (distance < minDistance)
                {
                    minDistance = distance;
                    Debug.Log(child.name);
                }
            }


        }
        private void Update ()
        {
            FindCenterItem(ScrollRect_Minute, Content_Minute);
        }
        public void Button_Click_Close ()
        {

            this.GetComponent<RectTransform>().DOScale(0, 0.3f).OnComplete(() =>
            {
                Destroy(this.gameObject.transform.parent.gameObject);
            });
        }
    }
}

