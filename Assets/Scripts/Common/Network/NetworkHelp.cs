using System;
using System.Net.Http;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace Common.Network
{
    public static class NetworkHelp
    {
        /// <summary>
        /// 基于反射实现的 Get （使用 HttpClient）
        /// </summary>
        /// <param name="url"></param>
        /// <param name="queryParams"></param>
        /// <param name="timeoutSeconds">超时时间</param>
        /// <returns></returns>
        public static async Task<string> GetAsync (string url, object queryParams = null, int timeoutSeconds = 5)
        {
            try
            {
                using HttpClient httpClient = new();
                using CancellationTokenSource cts = new(TimeSpan.FromSeconds(timeoutSeconds));

                UriBuilder uriBuilder = new(url);

                if (queryParams != null)
                {
                    string queryString = "";
                    PropertyInfo[] properties = queryParams.GetType().GetProperties();
                    foreach (PropertyInfo property in properties)
                    {
                        object value = property.GetValue(queryParams);
                        if (value != null)
                        {
                            queryString += $"{property.Name}={value}&";
                        }
                    }

                    uriBuilder.Query = queryString.TrimEnd('&');
                    Debug.Log(uriBuilder.Uri);
                }
                HttpResponseMessage response = await httpClient.GetAsync(uriBuilder.Uri, cts.Token);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (OperationCanceledException)
            {
                return "Request timed out.";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        /// <summary>
        /// 基于反射实现的Post
        /// 可以直接使用反射的特性，new {}
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="queryParams">参数</param>
        /// <param name="action">回调</param>
        ///  <param name="timeoutSeconds">超时时间</param>
        /// <returns></returns>
        public static async Task<string> Post (string url, object queryParams, UnityAction action = null)
        {
            var form = new WWWForm();
            PropertyInfo[] properties = queryParams.GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                object value = property.GetValue(queryParams);
                if (value != null)
                {
                    form.AddField(property.Name, value.ToString());
                }
            }


            using UnityWebRequest www = UnityWebRequest.Post(url, form);
            await www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                action?.Invoke();
                return www.downloadHandler.text;
            }
            else if (www.result == UnityWebRequest.Result.ConnectionError && www.error.Contains("Request timed out"))
            {
                return "Request timed out.";
            }
            else
            {
                return www.result.ToString();
            }
        }
        /// <summary>
        /// 异步的扩展方法
        /// </summary>
        public static TaskAwaiter GetAwaiter (this UnityWebRequestAsyncOperation asyncOp)
        {
            var tcs = new TaskCompletionSource<object>();
            asyncOp.completed += obj => { tcs.SetResult(null); };
            return ((Task)tcs.Task).GetAwaiter();
        }
    }
}