using OBJScripts;
using UnityEngine;
namespace GameManager
{
    public class LoadingSceneManager<T>
    {

        /// <summary>
        /// 将要跳转的场景ID或者场景名
        /// </summary>
        private T WillsceneID;
        private LoadingSceneUIController loadingSceneUIController;
        public LoadingSceneManager (T sceneID)
        {
            this.WillsceneID = sceneID;
            var _loading = MonoBehaviour.Instantiate(Resources.Load<GameObject>("GameObject/UI/LoadingSceneManagerCanvas"));
            loadingSceneUIController = _loading.GetComponent<LoadingSceneUIController>();
            if (sceneID.GetType() == typeof(int))
            {
                //场景ID
            }
            else
            {
                //场景名称
                loadingSceneUIController.Load(sceneID.ToString());
            }
        }



    }
}