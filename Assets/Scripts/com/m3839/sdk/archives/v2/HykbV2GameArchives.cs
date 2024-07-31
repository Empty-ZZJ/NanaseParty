using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.m3839.sdk.archives.v2.listener;

namespace com.m3839.sdk.archives.v2
{
    public class HykbV2GameArchives
    {
        private AndroidJavaObject gameArchive;


        public HykbV2GameArchives()
        {
            gameArchive = new AndroidJavaObject("com.m3839.sdk.archives.v2.HykbV2GameArchives");
        }

        public HykbV2GameArchives(AndroidJavaObject gameArchive)
        {
            this.gameArchive = gameArchive;
        }

        /// <summary>
        /// 设置档位ID
        /// </summary>
        /// <param name="archivesId"></param>
        public void SetArchivesId(int archivesId)
        {
            gameArchive.Call("setArchivesId", archivesId);
        }

        /// <summary>
        /// 获取档位ID
        /// </summary>
        /// <returns></returns>
        public int GetArchivesId()
        {
            return gameArchive.Call<int>("getArchivesId");
        }

        /// <summary>
        /// 获取档位标题
        /// </summary>
        /// <returns></returns>
        public string GetArchivesTitle()
        {
            return gameArchive.Call<string>("getArchivesTitle");
        }

        /// <summary>
        /// 设置档位标题
        /// </summary>
        /// <param name="archivesTitle"></param>
        public void SetArchivesTitle(string archivesTitle)
        {
            gameArchive.Call("setArchivesTitle", archivesTitle);
        }

        /// <summary>
        /// 设置档位内容：字符串
        /// </summary>
        /// <param name="archivesContent"></param>
        public void SetArchivesContent(string archivesContent)
        {
            gameArchive.Call("setArchivesContent", archivesContent);
        }

        /// <summary>
        /// 设置档位内容：字节数组
        /// </summary>
        /// <param name="archivesContentBytes"></param>
        public void SetArchivesContentBytes(byte[] archivesContentBytes)
        {
            gameArchive.Call("setArchivesContentBytes", archivesContentBytes);
        }

        /// <summary>
        /// 设置档位文件路径
        /// </summary>
        /// <param name="archiveFilePath"></param>
        public void SetArchiveFilePath(string archiveFilePath)
        {
            gameArchive.Call("setArchiveFilePath", archiveFilePath);
        }

        /// <summary>
        /// 获取档位内容：字符串
        /// </summary>
        /// <returns></returns>
        public string GetArchivesContent()
        {
            return gameArchive.Call<string>("getArchivesContent");
        }

        /// <summary>
        /// 获取档位内容：字节数组
        /// </summary>
        /// <returns></returns>
        public byte[] GetArchivesContentBytes()
        {
            return gameArchive.Call<byte[]>("getArchivesContentBytes");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="updateTime"></param>
        public void SetUpdateTime(string updateTime)
        {
            gameArchive.Call("setUpdateTime", updateTime);
        }

        public string GetUpdateTime()
        {
            return gameArchive.Call<string>("getUpdateTime");
        }

        /// <summary>
        /// 存档
        /// </summary>
        /// <param name="listener"></param>
        public void SaveArchive(HykbV2ArchivesListener listener)
        {
            HykbContext.GetInstance().RunOnUIThread(new AndroidJavaRunnable(() =>
            {
                gameArchive.Call("saveArchive", listener);
            }));
            
        }

        /// <summary>
        /// 取档
        /// </summary>
        /// <param name="listener"></param>
        public void ReadArchive(HykbV2ArchivesListener listener)
        {
            HykbContext.GetInstance().RunOnUIThread(new AndroidJavaRunnable(() =>
            {
                gameArchive.Call("readArchive", listener);
            }));
            
        }

        /// <summary>
        /// 获取档位列表
        /// </summary>
        /// <param name="listener"></param>
        public void QueryAllArchive(HykbV2AllArchivesListener listener)
        {
            gameArchive.Call("queryAllArchive", listener);
        }

    }
}

