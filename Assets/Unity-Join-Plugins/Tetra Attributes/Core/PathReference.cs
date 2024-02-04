#if UNITY_EDITOR
using UnityEditor;
#endif

namespace TetraCreations.Attributes
{
    [System.Serializable]
    public class PathReference
    {
        public string GUI;

        #if UNITY_EDITOR
        public string Path => AssetDatabase.GUIDToAssetPath(GUI);
        #endif
    }
}
