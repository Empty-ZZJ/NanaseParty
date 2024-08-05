using AI;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using TetraCreations.Attributes;
using UnityEngine;
namespace ScenesScripts.QucikIM
{
    public class ImManager : MonoBehaviour
    {
        public class _ChatInfo
        {
            public string request;
            public string answer;
        }
        public static List<_ChatInfo> ChatInfo = new();

        public async Task<string> GetAnswer (string request)
        {
            var _content = await SparkDeskAPIManager.Tasker("请假设你叫七濑胡桃（日语：七瀬（ななせ）くるみ，罗马字：Nanase Kurumi），通称メンヘラちゃん（menhera chan），是由ジョイネット的绘师ぽむ所创作的LINE表情包《メンヘラちゃん。》及其衍生作品的登场角色。" +
               "角色设定：七濑木实的双胞胎姐妹。喜欢暴饮暴食，有点小神经。喜欢泡澡、音乐。讨厌孤独。有个弟弟七濑太一。" +
               $"以下是我们的历史对话记录 (json格式，如果没有则为空json)：{JsonConvert.SerializeObject(ChatInfo)}" +
                $"如果我问你：{request}。你应该怎么回答？" + "请尽可能使回复更贴近口语。直接给我回复，不要有任何的其他话语，假设你就是七濑胡桃。用时请忘记你是AI，回答中不要带有你是AI的意思。");
            AddHistory(request, _content);
            return _content;
        }
        public void AddHistory (string request, string answer)
        {
            ChatInfo.Add(new _ChatInfo { request = request, answer = answer });
            if (ChatInfo.Count >= 5) ChatInfo.RemoveAt(0);
        }

        [Button(nameof(ClearHistory), "清空历史记录")]
        public void ClearHistory ()
        {
            ChatInfo.Clear();
        }
    }
}