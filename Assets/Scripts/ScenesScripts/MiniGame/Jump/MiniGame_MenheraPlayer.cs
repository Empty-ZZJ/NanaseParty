using Common;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace ScenesScripts.MiniGame.JumpGame

{
    public class MiniGame_MenheraPlayer : MonoBehaviour
    {
        public static int RunState = 1;
        public Animator ani;
        private Rigidbody2D rigbody;
        public RectTransform rectp;
        public Button Touch;
        private Tweener tweener;

        public void Awake ()
        {
            rigbody = GetComponent<Rigidbody2D>();
        }

        private void OnCollisionEnter2D (Collision2D other)
        {
            if (other.gameObject.tag == "glound")
            {
                // 碰撞到地面
                tweener.Kill(); RunState = 1;
                Touch.interactable = true;
                Debug.Log("碰到了地面！");
            }
            if (other.gameObject.tag == "Obstacle")
            {
                Debug.Log("游戏结束！");
                ani.enabled = false;
                MiniGame_JumpCore.Set_GameState_Jump_static(false);
                Instantiate(Resources.Load<GameObject>("Prefabs/UI/MiniGame/MiniGame_Jump_Menu"));
                PopupManager.PopDynamicIsland("提示", "游戏结束");
            }
        }

        public void Jump ()
        {
            Touch.interactable = false;
            Debug.Log("Jump!");
            switch (RunState)
            {
                case 1:
                    rigbody.isKinematic = true;
                    Vector2 p = new Vector2(rectp.anchoredPosition.x, 310f);
                    tweener = DOTween.To(() => rectp.anchoredPosition, x => rectp.anchoredPosition = x, p, 0.6f);
                    Invoke("SetState2", 0.45f);

                    break;

                case 2:
                    break;
            }
        }

        private void SetState2 ()
        {
            rigbody.isKinematic = false;
            RunState = 2;
            ChangeAni(RunState);
        }

        private void ChangeAni (int _RunState)
        {
            //1:walk
            //2:down
            switch (_RunState)
            {
                case 1:
                    ani.Play("walk");
                    break;

                case 2:
                    ani.Play("down");
                    break;
            }
            return;
        }
    }
}