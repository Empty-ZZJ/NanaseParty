namespace GameManager
{
    public class LoadingSceneManager<T>
    {
        /// <summary>
        /// 将要跳转的场景ID或者场景名
        /// </summary>
        private T WillsceneID;
        public LoadingSceneManager (T sceneID)
        {
            this.WillsceneID = sceneID;
            if (sceneID.GetType() == typeof(int))
            {
                //场景ID
            }
            else
            {
                //场景名称
            }
        }

    }
}