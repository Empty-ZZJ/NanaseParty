using Common;
using Common.Network;
using UnityEngine;

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
        }
        /// <summary>
        /// 初始化ServerManager
        /// </summary>
        public static void Init()
        {

            //Config

            Config.GameCommonConfig = new($"{GameAPI.GetWritePath()}/Config/GameCommonConfig.ini");
            //Network
            Network.TcpClient_Game = new();

            Debug.Log("初始化完毕！");
        }



    }

}