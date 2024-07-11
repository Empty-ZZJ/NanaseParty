using GameManager;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ScenesScripts
{
    public class StartGameControler : MonoBehaviour
    {
        public Text GameVer;
        /// <summary>
        /// 游玩须知
        /// </summary>
        public static bool IsCanTouch = false;

        private void Start ()
        {
            GameVer.text = $"Ver ： {Application.version}";
            if (ServerManager.Config.GameCommonConfig.GetValue("UserInfo", "IsReadInstructions") != "True")
            {
                Instantiate(Resources.Load<GameObject>("GameObject/Popup/Canvas_Popup_PlayInstructions"));
            }

        }
        public async void StartGame ()
        {
            if (!ServerManager.Config.GameCommonConfig.GetValue("UserInfo", "IsLogin").Equals("True"))
            {
                Instantiate(Resources.Load<GameObject>("GameObject/Popup/Popup_SignIn"), GameObject.Find("Canvas").GetComponent<Transform>());
                return;
            }

            await SceneManager.LoadSceneAsync("Game-Lobby");
        }

        public void Button_Click_UserLogOut ()
        {

        }




        /// <summary>
        /// Cast a ray to test if Input.mousePosition is over any UI object in EventSystem.current. This is a replacement
        /// for IsPointerOverGameObject() which does not work on Android in 4.6.0f3
        /// </summary>
        private bool IsPointerOverUIObject ()
        {
            if (EventSystem.current == null)
                return false;

            // Referencing this code for GraphicRaycaster https://gist.github.com/stramit/ead7ca1f432f3c0f181f
            // the ray cast appears to require only eventData.position.
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            foreach (var item in results)
            {
                Debug.Log(item.gameObject.name);
                if (item.gameObject.GetComponent<Canvas>() != null)
                    return true;

            }
            return false;
        }
    }
}