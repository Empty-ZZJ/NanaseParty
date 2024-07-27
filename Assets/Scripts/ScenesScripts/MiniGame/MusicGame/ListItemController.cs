using UnityEngine;
using UnityEngine.UI;
namespace ScenesScripts.MiniGame.MusicGame
{
    public class ListItemController : MonoBehaviour
    {
        public GameObject Circle;
        public Text Title;
        public string id;
        public static string SelectID = string.Empty;
        public void Button_Click_Select ()
        {
            SelectID = this.gameObject.name;
        }
        private void Update ()
        {
            Circle.SetActive(SelectID == this.gameObject.name);

        }
    }
}

