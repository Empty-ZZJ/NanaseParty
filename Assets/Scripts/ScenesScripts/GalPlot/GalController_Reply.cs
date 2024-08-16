using UnityEngine;
using UnityEngine.UI;
namespace ScenesScripts.GalPlot
{
    public class GalController_Reply : MonoBehaviour
    {
        public RectTransform Content;
        public RectTransform ReplyText;
        public Scrollbar Scrollbar;
        public void Start ()
        {
            ReplyText.GetComponent<Text>().text = string.Empty;
            foreach (var item in GalManager.PlotHistory)
            {
                ReplyText.GetComponent<Text>().text += $"{item}\n";
            }
            ReplyText.anchoredPosition3D = Vector3.zero;
            Content.anchoredPosition3D = new Vector3(-714f, Content.sizeDelta.y, 0f);
            Scrollbar.value = 0;
        }
        private void Update ()
        {
            Content.sizeDelta = new Vector2(Content.sizeDelta.x, ReplyText.sizeDelta.y);
        }

    }

}
