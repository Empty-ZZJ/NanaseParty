using UnityEngine;
namespace ScenesScripts.MiniGame.JumpGame

{
    public class ObstacleMovement : MonoBehaviour
    {
        private float Buttom;

        public void Awake ()
        {
            Buttom = GameObject.Find("MainCanvas/Ground/Buttom1").GetComponent<RectTransform>().sizeDelta.x;
            //this.transform.SetParent(GameObject.Find("MainCanvas/Ground/Buttom2").transform);
        }

        private void Update ()
        {
            /*
            Vector3 pos = transform.position;
            float t = Buttom / this.GetComponent<RectTransform>().sizeDelta.x;
            //Debug.Log("speed T:" + t.ToString());
            pos.x -= (MiniGameRunStruct.GameSpeed * (t / 3.5f) * Time.deltaTime);
            transform.position = pos;
            */

            RectTransform rt = this.GetComponent<RectTransform>();
            Vector2 pos = rt.anchoredPosition;

            float newPos = pos.x - (MiniGameRunStruct_Jump.GameSpeed * Time.deltaTime);

            // Wrap the image around when it goes off-screen
            /*
            if (newPos <= -rt.rect.width)
            {
                newPos += rt.rect.width;
            }
            */
            rt.anchoredPosition = new Vector2(newPos, pos.y);
            if (!MiniGameRunStruct_Jump.GameState)
            {
                Destroy(this.gameObject);
            }
        }

        private void OnBecameInvisible ()
        {
            Debug.Log("OnBecameInvisible");
            Destroy(gameObject);
        }
    }
}