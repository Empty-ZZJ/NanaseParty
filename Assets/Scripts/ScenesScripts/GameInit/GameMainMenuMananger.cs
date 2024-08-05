using UnityEngine;
namespace ScenesScripts
{
    public class GameMainMenuMananger : MonoBehaviour
    {
        private Transform ThisCanvas;


        private void Start ()
        {
            ThisCanvas = this.gameObject.GetComponent<Transform>();
            Invoke(nameof(CheckHotUpdate), 2f);
        }

        private async void CheckHotUpdate ()
        {

            Instantiate(Resources.Load<GameObject>("GameObject/Scene/InitGame/HotUpdate"), ThisCanvas).GetComponent<HotUpdateControler>();

        }

    }
}
