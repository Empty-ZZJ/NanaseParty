using GameManager;
using UnityEngine;

namespace OBJScripts
{
    public class ServerManagerInit : MonoBehaviour
    {
        public void Start ()
        {
            // 获取当前场景中的所有游戏对象
            GameObject[] allGameObjects = FindObjectsOfType<GameObject>();

            // 遍历所有游戏对象
            foreach (GameObject go in allGameObjects)
            {
                if (go.GetComponent<ServerManagerInit>() != null && go != this.gameObject)
                {
                    // 销毁当前游戏对象
                    Destroy(gameObject);
                    break;
                }


            }
            ServerManager.Init();
            this.gameObject.AddComponent<GameDataManager>();
        }
    }
}