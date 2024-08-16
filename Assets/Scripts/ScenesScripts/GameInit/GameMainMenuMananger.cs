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
            DontDestroyOnLoad(GameObject.Find("FPSCanvas"));
        }

        private void CheckHotUpdate ()
        {

            Instantiate(Resources.Load<GameObject>("GameObject/Scene/InitGame/HotUpdate"), ThisCanvas).GetComponent<HotUpdateControler>();

        }

    }
}
