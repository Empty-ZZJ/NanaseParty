namespace Common.Network
{
    using System.Collections;
    using UnityEngine;
    using UnityEngine.Networking;

    public class FileDownloader : MonoBehaviour
    {
        public delegate void DownloadProgressDelegate (float progress);
        public static event DownloadProgressDelegate OnDownloadProgress;

        public delegate void DownloadCompleteDelegate (string filePath);
        public static event DownloadCompleteDelegate OnDownloadComplete;

        public string url = "http://example.com/file.zip"; // 要下载的文件的URL
        public string savePath = "Assets/Downloaded/file.zip"; // 文件下载后的保存路径

        IEnumerator DownloadFile (string url, string savePath)
        {
            using (UnityWebRequest www = UnityWebRequest.Get(url))
            {
                www.downloadHandler = new DownloadHandlerBuffer(); // 使用缓冲区来获取下载进度
                float.TryParse(www.GetResponseHeader("Content-Length"), out float totalBytes);

                float downloadedBytes = 0f;

                while (!www.isDone)
                {
                    if (www.downloadedBytes > downloadedBytes)
                    {
                        downloadedBytes = www.downloadedBytes;
                        float progress = downloadedBytes / totalBytes;
                        OnDownloadProgress?.Invoke(progress);
                    }
                    yield return null;
                }

                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError("下载失败: " + www.error);
                }
                else
                {
                    System.IO.File.WriteAllBytes(savePath, www.downloadHandler.data);
                    Debug.Log("文件下载成功: " + savePath);
                    OnDownloadComplete?.Invoke(savePath);
                }
            }
        }
    }
}