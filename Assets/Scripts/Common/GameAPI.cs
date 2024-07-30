using System;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace Common
{
    /// <summary>
    /// 游戏内通用API
    /// </summary>
    public static class GameAPI
    {
        /// <summary>
        /// 返回可读可写路径
        /// PC端：streamingAssetsPath
        /// 移动端：Application.persistentDataPath
        /// </summary>
        /// <returns></returns>
        public static string GetWritePath ()
        {
            return Application.persistentDataPath;
        }

        /// <summary>
        /// 暴力查找一个物体，找不到返回Null
        /// </summary>
        /// <param name="_Name"></param>
        /// <returns></returns>
        public static GameObject FindGameObject_Force (string _Name)
        {

            GameObject[] all = Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[];
            for (int i = 0; i < all.Length; i++)
            {
                var item = all[i];
                if (item.name == _Name) return item;
            }
            return null;

        }
        /// <summary>
        /// 生成SHA256值
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GenerateSha256 (string input)
        {
            using SHA256 sha256Hash = SHA256.Create();
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }

            return builder.ToString().ToUpper();
        }

        /// <summary>
        /// 在[minA，maxB]中产生一个随机数，并返回它
        /// </summary>
        /// <param name="minA"></param>
        /// <param name="maxB"></param>
        /// <returns></returns>
        public static int GetRandomInAB (int minA, int maxB)
        {
            try
            {
                if (maxB > minA)
                {
                    byte[] bytes = new byte[4];
                    RandomNumberGenerator.Fill(bytes);
                    int seed = BitConverter.ToInt32(bytes, 0);
                    string strTick = Convert.ToString(DateTime.Now.Ticks);
                    if (strTick.Length > 8)
                        strTick = strTick.Substring(strTick.Length - 8, 8);
                    seed += Convert.ToInt32(strTick);
                    var random = new System.Random(seed);
                    return random.Next(minA, maxB);
                }
                else
                {
                    return -1; // 返回 -1 表示参数无效
                }
            }
            catch
            {

                return -1; // 返回 -1 表示参数无效
            }



        }


    }
}
