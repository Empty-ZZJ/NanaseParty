using System.IO;
using UnityEngine;
using UnityEngine.UI;
namespace OBJScripts.SettingManager
{
    public class Setting_Sponsor : MonoBehaviour
    {
        public void Button_Save_W (Image img)
        {
            string[] paths = new string[1];
            paths[0] = Path.Combine(GetScreenshotDir() + "/WX.png");



            File.WriteAllBytes(paths[0], DeCompress(img.sprite.texture).EncodeToPNG());
            ScanFile(paths);

        }
        public void Button_Save_A (Image img)
        {
            string[] paths = new string[1];
            paths[0] = Path.Combine(GetScreenshotDir() + "/ALI.png");



            File.WriteAllBytes(paths[0], DeCompress(img.sprite.texture).EncodeToPNG());
            ScanFile(paths);


        }
        /// <summary>
        /// //刷新图片，显示到相册中
        /// </summary>
        /// <param name="path"></param>
        private void ScanFile (string[] path)
        {
            using AndroidJavaClass PlayerActivity = new("com.unity3d.player.UnityPlayer");
            AndroidJavaObject playerActivity = PlayerActivity.GetStatic<AndroidJavaObject>("currentActivity");
            using AndroidJavaObject Conn = new("android.media.MediaScannerConnection", playerActivity, null);
            Conn.CallStatic("scanFile", playerActivity, path, null, null);


        }
        public Texture2D DeCompress (Texture2D source)
        {
            RenderTexture renderTex = RenderTexture.GetTemporary(
                        source.width,
                        source.height,
                        0,
                        RenderTextureFormat.Default,
                        RenderTextureReadWrite.Linear);

            Graphics.Blit(source, renderTex);
            RenderTexture previous = RenderTexture.active;
            RenderTexture.active = renderTex;
            Texture2D readableText = new(source.width, source.height);
            readableText.ReadPixels(new Rect(0, 0, renderTex.width, renderTex.height), 0, 0);
            readableText.Apply();
            RenderTexture.active = previous;
            RenderTexture.ReleaseTemporary(renderTex);
            return readableText;
        }

        private string GetScreenshotDir ()
        {
            string dcimPath = "/storage/emulated/0/DCIM/";

            // 检查 DCIM 文件夹是否存在，如不存在则创建之
            if (!Directory.Exists(dcimPath))
            {
                Directory.CreateDirectory(dcimPath);
            }

            // 拼接目标文件夹路径
            string screenshotDir = System.IO.Path.Combine(dcimPath, "七濑派对");

            if (!Directory.Exists(screenshotDir))
            {
                Directory.CreateDirectory(screenshotDir);
            }

            return screenshotDir;
        }


    }

}
