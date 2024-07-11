using UnityEngine;
namespace ScenesScripts.FocusClock
{
    public class HutaoClockStateManager : MonoBehaviour
    {
        /// <summary>
        /// 设置0的animator播放速度
        /// </summary>
        public void SetZeroSpeed ()
        {
            GameObject.Find("EventSystem").GetComponent<FocusClockManager>().SetZeroSpeed();
        }
    }

}
