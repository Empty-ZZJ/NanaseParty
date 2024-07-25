using UnityEngine;
namespace ScenesScripts.MiniGame.JumpGame

{
    public class MiniGameRunStruct_Jump : MonoBehaviour
    {
        /// <summary>
        /// 游戏的进行状态
        /// </summary>
        public static bool GameState = false;

        /// <summary>
        /// 游戏的初始速度
        /// </summary>
        public const float GameInit_Speed = 400;

        /// <summary>
        /// 当前的游戏速度
        /// </summary>
        public static float GameSpeed = 400;

        /// <summary>
        /// 定时器，上次加快速度到现在的时间
        /// </summary>
        public static float timer = 0.0f;

        /// <summary>
        /// 时间间隔
        /// </summary>
        public static float incrementInterval = 10.0f;

        /// <summary>
        /// 增加的值
        /// </summary>
        public static int incrementValue = 50;

        /// <summary>
        /// 游戏的最大速度
        /// </summary>
        public static int maxGameSpeed = 900;

        private void Awake ()
        {
            Instantiate(Resources.Load<GameObject>("GameObject/Scene/MiniGame/Jump/MiniGame_Jump_Menu"));
        }

        private void Update ()
        {
            if (MiniGameRunStruct_Jump.GameState)
            {
                timer += Time.deltaTime;
                if (timer > incrementInterval)
                {
                    timer -= incrementInterval;
                    if (GameSpeed + incrementValue <= maxGameSpeed)
                    {
                        GameSpeed += incrementValue;
                        Debug.Log("加速  当前速度：" + GameSpeed.ToString());
                    }
                    else
                    {
                        GameSpeed = maxGameSpeed;
                    }
                }
            }
        }
    }
}