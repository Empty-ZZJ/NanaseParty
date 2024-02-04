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
                // 如果找到一个具有相同名称的游戏对象，且不是当前游戏对象
                if (go.name == gameObject.name && go != gameObject)
                {
                    // 销毁当前游戏对象
                    Destroy(gameObject);
                    break;
                }
            }
            ServerManager.Init();
        }
    }
}