using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace ScenesScripts.FocusClock
{
    public static class LrcCore
    {
        /// <summary>
        /// 获得歌词信息
        /// </summary>
        /// <param name="LrcPath">歌词路径</param>
        /// <returns>返回歌词信息(Lrc实例)</returns>
        public static LrcInfo AnalysisLrc (string LrcString)
        {
            LrcInfo lrc = new();
            string line;
            Dictionary<double, string> dicword = new();
            using StreamReader sr = new(LrcString);
            while ((line = sr.ReadLine()) != null)
            {
                if (line.StartsWith("[ti:"))
                {
                    lrc.Title = SplitInfo(line);
                }
                else if (line.StartsWith("[ar:"))
                {
                    lrc.Artist = SplitInfo(line);
                }
                else if (line.StartsWith("[al:"))
                {
                    lrc.Album = SplitInfo(line);
                }
                else if (line.StartsWith("[by:"))
                {
                    lrc.LrcBy = SplitInfo(line);
                }
                else if (line.StartsWith("[offset:"))
                {
                    lrc.Offset = SplitInfo(line);
                }
                else
                {
                    try
                    {
                        Regex regexword = new(@".*\](.*)");
                        Match mcw = regexword.Match(line);
                        string word = mcw.Groups[1].Value;
                        Regex regextime = new(@"\[([0-9.:]*)\]", RegexOptions.Compiled);
                        MatchCollection mct = regextime.Matches(line);
                        foreach (Match item in mct.Cast<Match>())
                        {
                            double time = TimeSpan.Parse("00:" + item.Groups[1].Value).TotalSeconds;
                            dicword.Add(time, word);
                        }
                    }
                    catch
                    {
                        continue;
                    }
                }

            }
            lrc.LrcWord = dicword.OrderBy(t => t.Key).ToDictionary(t => t.Key, p => p.Value);
            return lrc;
        }
        /// <summary>
        /// 处理信息(私有方法)
        /// </summary>
        /// <param name="line"></param>
        /// <returns>返回基础信息</returns>
        private static string SplitInfo (string line)
        {
            return line[(line.IndexOf(":") + 1)..].TrimEnd(']');
        }
        public class LrcInfo
        {
            /// <summary>
            /// 歌曲
            /// </summary>
            public string Title { get; set; }
            /// <summary>
            /// 艺术家
            /// </summary>
            public string Artist { get; set; }
            /// <summary>
            /// 专辑
            /// </summary>
            public string Album { get; set; }
            /// <summary>
            /// 歌词作者
            /// </summary>
            public string LrcBy { get; set; }
            /// <summary>
            /// 偏移量
            /// </summary>
            public string Offset { get; set; }

            /// <summary>
            /// 歌词
            /// </summary>
            public Dictionary<double, string> LrcWord = new Dictionary<double, string>();
        }

    }
}