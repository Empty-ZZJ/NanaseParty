using System.IO;

namespace Common
{
    public class FileManager
    {
        /// <summary>
        /// 创建文件，不同的是这个支持自动创建文件夹
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static StreamWriter CreatTextFile (string path)
        {
            if (File.Exists(path)) return new StreamWriter(path);
            var _DirectoryName = Path.GetDirectoryName(path);
            // 如果文件夹不存在，则创建它
            if (!Directory.Exists(_DirectoryName))
            {
                Directory.CreateDirectory(_DirectoryName);
            }
            // 创建文件
            return File.CreateText(path);

        }
    }

}