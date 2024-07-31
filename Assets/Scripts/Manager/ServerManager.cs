using Common;
using Common.Network;
using Common.UI;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameManager
{

    public static class ServerManager
    {
        public static class Network
        {
            public static AsyncTcpClient TcpClient_Game;
        }
        public static class Config
        {
            public static IniConfig GameCommonConfig;
            public static IniConfig CharacterInfo;
            public static IniConfig Department;
        }
        public static class Audio
        {
            public static UIAudioControl AudioControl;
        }
        /// <summary>
        /// 初始化ServerManager
        /// </summary>
        public static void Init ()
        {
            var _path_CharacterInfo = $"{GameAPI.GetWritePath()}/Config/CharacterInfo.ini";
            var _path_Department = $"{GameAPI.GetWritePath()}/Config/Department.ini";


            File.WriteAllText(_path_CharacterInfo, Resources.Load<TextAsset>("Config/CharacterInfo").text);
            File.WriteAllText(_path_Department, Resources.Load<TextAsset>("Config/Department").text);






            //Config
            Config.GameCommonConfig = new($"{GameAPI.GetWritePath()}/Config/GameCommonConfig.ini");
            Config.CharacterInfo = new(_path_CharacterInfo);
            Config.Department = new(_path_Department);


            //Network
            Network.TcpClient_Game = new();
            Debug.Log("初始化完毕！");
        }
        public static async Task LoadScene (string name)
        {
            new ShowLoading("正在加载");
            await SceneManager.LoadSceneAsync(name);

        }

        /// <summary>
        /// 异步的扩展方法
        /// </summary>
        public static TaskAwaiter GetAwaiter (this AsyncOperation asyncOp)
        {
            var tcs = new TaskCompletionSource<object>();
            asyncOp.completed += obj => { tcs.SetResult(null); };
            return ((Task)tcs.Task).GetAwaiter();
        }


    }

}