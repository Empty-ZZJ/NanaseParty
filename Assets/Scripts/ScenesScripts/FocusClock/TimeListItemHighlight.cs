using UnityEngine;
using UnityEngine.UI;
namespace ScenesScripts.FocusClock
{
    public class TimeListItemHighlight : MonoBehaviour
    {
        private Text text;
        private void Start ()
        {
            text = this.gameObject.GetComponent<Text>();
        }
        private void Update ()
        {

            var _transForm = this.gameObject.GetComponent<RectTransform>().anchoredPosition3D;
        }

    }

}
