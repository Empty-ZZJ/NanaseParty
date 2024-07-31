using UnityEngine;

namespace Common
{
    public class SimpleOnceComponent : MonoBehaviour
    {
        public Transform Transform_InstantiateObj_SameLevel;
        public void Start ()
        {

        }
        public void OpenUrl (string url)
        {
            Application.OpenURL(url);
        }
        public void DestroyGameObject (GameObject gameObject)
        {
            Destroy(gameObject);

        }
        public void InstantiateObj (GameObject gameObject)
        {
            Instantiate(gameObject);
        }
        public void InstantiateObj_SameLevel (GameObject gameObject)
        {
            Instantiate(gameObject, Transform_InstantiateObj_SameLevel.transform.parent.transform);
        }

    }
}