using Common;
using Common.UI;
using System.Collections;
using System.Collections.Generic;
using TetraCreations.Attributes;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace OBJScripts
{
    public class LoadingSceneUIController : MonoBehaviour
    {
        public AsyncOperation sceneLoadingOperation;
        private LoadingSceneUIController()
        {

        }
        private static List<Sprite> Imgs = new();
        [Title("背景图片")]
        public Image LoadingIMG;


        [Title("加载提示")]
        public Text LoadingTitle;

        [Title("加载进度条")]
        public Scrollbar LoadingProgress;
        private bool isLoading;
        private void Start()
        {
            if (Imgs.Count == 0)
            {
                for (int i = 1; i < 24; i++)
                {
                    var _sprite = Resources.Load<Sprite>($"Texture2D/illustration/{i}");
                    Imgs.Add(_sprite);
                }
            }

            var _scale = LoadingIMG.gameObject.GetComponent<BGScaler>();
            var _random_sprite = Imgs[GameAPI.GetRandomInAB(1, 24)];
            _scale.textureOriginSize = new Vector2(_random_sprite.texture.width, _random_sprite.texture.height);
            LoadingIMG.sprite = _random_sprite;

        }
        public void Load(string name)
        {
            StartCoroutine(LoadScene(name));

        }
        public void Load(int id)
        {

        }
        private IEnumerator LoadScene(string name)
        {
            isLoading = true;
            sceneLoadingOperation = SceneManager.LoadSceneAsync(name);
            sceneLoadingOperation.allowSceneActivation = false;
            //
            yield return sceneLoadingOperation;

        }
        private void Update()
        {
            if (!isLoading)
            {
                return;
            }
            LoadingProgress.value = sceneLoadingOperation.progress + 0.1f;
            if (sceneLoadingOperation.progress >= 0.95f || LoadingProgress.value >= 0.95f)
            {
                sceneLoadingOperation.allowSceneActivation = true;
            }
        }

    }
}
