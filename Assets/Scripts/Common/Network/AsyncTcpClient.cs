using GameManager;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Common.Network
{
    /// <summary>
    /// 用于向服务器发送数据和同步数据
    /// </summary>
    public class AsyncTcpClient
    {
        private TcpClient client;
        private readonly byte[] buffer = new byte[1024];
        private StringBuilder responseBuilder;

        public async Task ConnectAsync (string ipAddress, int port, int timeoutInMs, Action onTimeout = null)
        {
            try
            {
                client = new TcpClient();
                var connectTask = client.ConnectAsync(ipAddress, port);

                if (await Task.WhenAny(connectTask, Task.Delay(timeoutInMs)) != connectTask)
                {

                    if (onTimeout != null)
                        onTimeout();
                    return;
                }

                await connectTask;

                //如果之前已经登录了，就尝试使用Token登录
                if (ServerManager.Config.GameCommonConfig.GetValue("UserInfo", "IsLogin") == "True")
                {
                    Thread _UnicomThread = new(async () =>
                    {
                        var _boolean = await UnicomThread();
                        if (!_boolean)
                        {
                            PopupManager.PopMessage("网络错误", "与服务器丢失连接！");

                        }
                        Thread.Sleep(5000);
                    }); _UnicomThread.Start();
                }
                Debug.Log("Connected to server.");
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to connect to server: {e.Message}");

            }
        }

        public async Task<string> SendMessage_TCP (string message)
        {
            if (client == null || !client.Connected)
            {
                Debug.LogError("Not connected to server.");
                return null;
            }
            try
            {
                responseBuilder = new StringBuilder();

                NetworkStream stream = client.GetStream();
                byte[] data = Encoding.UTF8.GetBytes(message + "<EOM>");
                await stream.WriteAsync(data, 0, data.Length);

                while (true)
                {
                    int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                    responseBuilder.Append(Encoding.UTF8.GetString(buffer, 0, bytesRead));

                    if (responseBuilder.ToString().EndsWith("<EOM>"))
                    {
                        responseBuilder.Length -= 5;
                        break;
                    }
                }
                return responseBuilder.ToString();
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to send/receive message: {e}");
                return null;

            }
        }
        /// <summary>
        /// 强行切换与服务器的连接
        /// </summary>
        public void Disconnect ()
        {
            if (client != null)
            {
                client.Close();
                Debug.Log("Disconnected from server.");
            }
        }
        /// <summary>
        /// 当组件被取消激活的时候自动断开连接
        /// </summary>
        private void OnDisable ()
        {
            Disconnect();
        }
        /// <summary>
        /// 当前是否连接
        /// </summary>
        /// <returns></returns>
        public bool IsConnected ()
        {
            return client != null && client.Connected;
        }
        private async Task<bool> UnicomThread ()
        {
            var _pass_token = await SendMessage_TCP(JsonConvert.SerializeObject(new { type = "Connectivity", token = ServerManager.Config.GameCommonConfig.GetValue("UserInfo", "Token") }));
            var _json = JsonConvert.DeserializeObject<JObject>(_pass_token);
            if (_json["status"].ToString() != "success")
            {
                // new PopNewPopup("致命错误", "登录信息已过期请重新登录");
                ServerManager.Config.GameCommonConfig.SetValue("UserInfo", "IsLogin", "False");
                ServerManager.Config.GameCommonConfig.SetValue("UserInfo", "UID", "-1");
                ServerManager.Config.GameCommonConfig.SetValue("UserInfo", "Token", "-1");
                return false;
            }
            else return true;
        }

    }
}