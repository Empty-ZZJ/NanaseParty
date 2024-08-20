using GameManager;
using TetraCreations.Attributes;
using UnityEngine;
namespace OBJScripts.SettingManager
{
    public class Setting_Usetting : MonoBehaviour
    {
        [Button(nameof(Button_Click_LogOut), "µÇ³öÕËºÅ")]
        public void Button_Click_LogOut ()
        {
            ServerManager.Config.GameCommonConfig.SetValue("UserInfo", "IsLogin", "False");
            ServerManager.Config.GameCommonConfig.SetValue("UserInfo", "UID", "");
            ServerManager.Config.GameCommonConfig.SetValue("UserInfo", "Token", "");
            ServerManager.Config.GameCommonConfig.SetValue("UserInfo", "Type", "Local");
            ServerManager.Config.GameCommonConfig.SetValue("UserInfo", "LoginType", "Local");
            var _ = new LoadingSceneManager<string>("Game-Init");
        }
    }

}
