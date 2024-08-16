using Common.SupperComponent;
using UnityEngine;
using UnityEngine.UI;
namespace OBJScripts
{
    public class MenuComponentController : MonoBehaviour
    {
        private SupperButton ThisButton;
        public static string id = "Button_Setting";
        private Color HighColor = new(97 / 255f, 112 / 255f, 170 / 255f, 1f);
        private void Start ()
        {
            ThisButton = this.gameObject.GetComponent<SupperButton>();

        }
        public void Button_Click ()
        {
            id = this.gameObject.name;
        }
        private void Update ()
        {

            if (id == this.gameObject.name)
            {
                ThisButton.GetComponentInChildren<Text>().color = Color.white;
                this.gameObject.GetComponent<Image>().color = HighColor;
            }
            else
            {

                ThisButton.GetComponentInChildren<Text>().color = HighColor;
                this.gameObject.GetComponent<Image>().color = Color.white;
            }
        }

    }

}
