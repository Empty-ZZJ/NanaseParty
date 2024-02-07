using System;
using System.Collections;
using System.IO;
using UnityEngine;

namespace Common
{
    /// <summary>
    /// 游戏配置文件的读取，使用INI
    /// </summary>
    public class IniConfig
    {
        private Hashtable keyPairs = new();
        private string iniFilePath;
        private struct SectionPair
        {
            public string Section;
            public string Key;
        }
        /// <summary>
        /// 在给定的路径上打开INI文件并枚举IniParser中的值。
        /// </summary>
        /// <param name="iniPath">Full path to INI file.</param>
        public IniConfig (string iniPath)
        {
            TextReader iniFile = null;
            string strLine = null;
            string currentRoot = null;
            string[] keyPair = null;
            iniFilePath = iniPath;
            if (File.Exists(iniPath))
            {
                try
                {
                    iniFile = new StreamReader(iniPath);
                    strLine = iniFile.ReadLine();
                    while (strLine != null)
                    {
                        strLine = strLine.Trim();
                        if (strLine != "")
                        {
                            if (strLine.StartsWith("[") && strLine.EndsWith("]"))
                            {
                                currentRoot = strLine.Substring(1, strLine.Length - 2);
                            }
                            else
                            {
                                keyPair = strLine.Split(new char[] { '=' }, 2);
                                SectionPair sectionPair;
                                String value = null;
                                if (currentRoot == null)
                                    currentRoot = "ROOT";
                                sectionPair.Section = currentRoot;
                                sectionPair.Key = keyPair[0];
                                if (keyPair.Length > 1)
                                    value = keyPair[1];
                                keyPairs.Add(sectionPair, value);
                            }
                        }
                        strLine = iniFile.ReadLine();
                    }
                }
                catch (Exception ex)
                {
                    Debug.LogError(ex.Message);
                    throw ex;
                }
                finally
                {
                    if (iniFile != null)
                        iniFile.Close();
                }
            }
            else
            {

                Save();
            }
        }

        /// <summary>
        /// 返回给定section的值，key对。
        /// </summary>
        /// <param name="sectionName">Section name</param>
        /// <param name="settingName">Key name</param>
        public string GetValue (string sectionName, string settingName)
        {
            try
            {


                SectionPair sectionPair;
                sectionPair.Section = sectionName;
                sectionPair.Key = settingName;

                if (!keyPairs.ContainsKey(sectionPair))
                    return string.Empty;

                return (string)keyPairs[sectionPair];
            }
            catch { return string.Empty; }

        }
        /// <summary>
        /// 列出给定的Section的所有行
        /// </summary>
        /// <param name="sectionName">Section to enum.</param>
        public string[] EnumSection (string sectionName)
        {
            ArrayList tmpArray = new ArrayList();
            foreach (SectionPair pair in keyPairs.Keys)
            {
                if (pair.Section == sectionName)
                    tmpArray.Add(pair.Key);
            }
            return (string[])tmpArray.ToArray(typeof(string));
        }
        /// <summary>
        /// 向要保存的节添加或替换Value。
        /// </summary>
        /// <param name="sectionName">Section to add under.</param>
        /// <param name="settingName">Key name to add.</param>
        /// <param name="settingValue">Value of key.</param>
        public void SetValue (string sectionName, string settingName, string settingValue)
        {
            SectionPair sectionPair;
            sectionPair.Section = sectionName;
            sectionPair.Key = settingName;
            if (keyPairs.ContainsKey(sectionPair))
                keyPairs.Remove(sectionPair);
            keyPairs.Add(sectionPair, settingValue);
            Save();

        }
        /// <summary>
        /// 删除设置
        /// </summary>
        /// <param name="sectionName">指定Section</param>
        /// <param name="settingName">添加的Key</param>
        public void Delete (string sectionName, string settingName)
        {
            SectionPair sectionPair;
            sectionPair.Section = sectionName;
            sectionPair.Key = settingName;
            if (keyPairs.ContainsKey(sectionPair))
                keyPairs.Remove(sectionPair);
            Save();
        }
        /// <summary>
        /// 保存到新文件。
        /// </summary>
        /// <param name="newFilePath">新的文件路径。</param>
        public void SaveSettings (string newFilePath)
        {
            ArrayList sections = new ArrayList();
            string strToSave = "";
            foreach (SectionPair sectionPair in keyPairs.Keys)
            {
                if (!sections.Contains(sectionPair.Section))
                    sections.Add(sectionPair.Section);
            }
            foreach (string section in sections)
            {
                strToSave += ("[" + section + "]\r\n");
                foreach (SectionPair sectionPair in keyPairs.Keys)
                {
                    if (sectionPair.Section == section)
                    {
                        string tmpValue = (string)keyPairs[sectionPair];
                        if (tmpValue != null)
                            tmpValue = "=" + tmpValue;
                        strToSave += (sectionPair.Key + tmpValue + "\r\n");
                    }
                }
                strToSave += "\r\n";
            }
            try
            {
                var _file = FileManager.CreatTextFile(newFilePath);
                _file.Write(strToSave);
                _file.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 将设置保存回ini文件。
        /// </summary>
        public void Save ()
        {
            SaveSettings(iniFilePath);
        }
        public static string GetValue (string path, string SectionName, string settingName)
        {
            var _ = new IniConfig(path);
            var __ = _.GetValue(SectionName, settingName);
            return __;
        }
        public static IniConfig SetValue (string path, string sectionName, string settingName, string settingValue)
        {
            var _ = new IniConfig(path);
            _.SetValue(sectionName, settingName, settingValue);
            return _;

        }
    }
}
