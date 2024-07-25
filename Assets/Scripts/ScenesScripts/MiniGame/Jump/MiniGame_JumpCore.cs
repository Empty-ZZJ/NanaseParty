using UnityEngine;

namespace ScenesScripts.MiniGame.JumpGame

{
    public class MiniGame_JumpCore : MonoBehaviour
    {
        public void Set_GameState_Jump (bool _stateValue)
        {
            if (_stateValue)
            {
                MiniGameRunStruct_Jump.GameSpeed = MiniGameRunStruct_Jump.GameInit_Speed;
                GameObject.Find("MainCanvas/Menherachan").GetComponent<MiniGame_MenheraPlayer>().ani.enabled = true;
                GameObject.Find("MainCanvas/Menherachan").GetComponent<MiniGame_MenheraPlayer>().ani.Play("walk");
            }
            MiniGameRunStruct_Jump.GameState = _stateValue;
        }

        public static void Set_GameState_Jump_static (bool _stateValue)
        {
            MiniGameRunStruct_Jump.GameState = _stateValue;
        }
    }
}