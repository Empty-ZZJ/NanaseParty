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

        public void Button_Click_Close ()
        {

            this.GetComponent<RectTransform>().DOScale(0, 0.3f).OnComplete(() =>
            {
                Destroy(this.gameObject.transform.parent.gameObject);
            });
        }
        public void Button_Click_Start ()
        {
            Button_Click_Close();
            GameObject.Find("EventSystem").GetComponent<FocusClockManager>().StartClock();

        }
    }
}

