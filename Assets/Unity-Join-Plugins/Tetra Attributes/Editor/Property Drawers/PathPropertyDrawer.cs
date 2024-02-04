using System.IO;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TetraCreations.Attributes.Editor
{
    [CustomPropertyDrawer(typeof(PathReference))]
    public class PathPropertyDrawer : PropertyDrawer
    {
        private string _selectFolderLabel = "Select Folder";
        private string _invalidPathSubFolderText = "Invalid Path : Choose any folder inside the 'Assets' folder.";

        private bool _initialized;
        private Object _obj;
        private SerializedProperty guid;

        private void Init(SerializedProperty property)
        {
            _initialized = false;

            guid = property.FindPropertyRelative("GUI");

            if (guid == null) 
            {
                return; 
            }

            _obj = AssetDatabase.LoadAssetAtPath<Object>(AssetDatabase.GUIDToAssetPath(guid.stringValue));
            _initialized = true;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (!_initialized) 
            { 
                Init(property);
            }

            if (_obj == null && string.IsNullOrEmpty(guid.stringValue) == false)
            {
                _obj = AssetDatabase.LoadAssetAtPath<Object>(AssetDatabase.GUIDToAssetPath(guid.stringValue));
            }

            GUIContent guiContent = EditorGUIUtility.ObjectContent(_obj, typeof(DefaultAsset));

            Rect r = EditorGUI.PrefixLabel(position, label);

            Rect textFieldRect = r;
            textFieldRect.width -= 19f;

            GUIStyle textFieldStyle = new GUIStyle("TextField")
            {
                imagePosition = _obj ? ImagePosition.ImageLeft : ImagePosition.TextOnly
            };

            if (GUI.Button(textFieldRect, guiContent, textFieldStyle) && _obj)
            {
                EditorGUIUtility.PingObject(_obj);
            }

            if (textFieldRect.Contains(Event.current.mousePosition))
            {
                if (Event.current.type == EventType.DragUpdated)
                {
                    Object reference = DragAndDrop.objectReferences[0];
                    string path = AssetDatabase.GetAssetPath(reference);
                    DragAndDrop.visualMode = Directory.Exists(path) ? DragAndDropVisualMode.Copy : DragAndDropVisualMode.Rejected;
                    Event.current.Use();
                }
                else if (Event.current.type == EventType.DragPerform)
                {
                    Object reference = DragAndDrop.objectReferences[0];
                    string path = AssetDatabase.GetAssetPath(reference);
                    if (Directory.Exists(path))
                    {
                        _obj = reference;
                        guid.stringValue = AssetDatabase.AssetPathToGUID(path);
                        property.serializedObject.ApplyModifiedProperties();
                    }
                    Event.current.Use();
                }
                else if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Delete)
                {
                    guid.stringValue = "";
                    _obj = null;
                    property.serializedObject.ApplyModifiedProperties();
                    Event.current.Use();
                }
            }

                Rect objectFieldRect = r;
            objectFieldRect.x = textFieldRect.xMax + 1f;
            objectFieldRect.width = 19f;

            if (GUI.Button(objectFieldRect, "", GUI.skin.GetStyle("IN ObjectField")))
            {
                string path = EditorUtility.OpenFolderPanel(_selectFolderLabel, "Assets", "");

                if (string.IsNullOrEmpty(path)) 
                { 
                    GUIUtility.ExitGUI();
                    return;
                }

                if (path.Contains(Application.dataPath))
                {
                    path = "Assets" + path.Substring(Application.dataPath.Length);

                    _obj = AssetDatabase.LoadAssetAtPath(path, typeof(DefaultAsset));

                    guid.stringValue = AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(_obj));
                    property.serializedObject.ApplyModifiedProperties();
                    GUIUtility.ExitGUI();
                }
                else
                {
                    Debug.LogError(_invalidPathSubFolderText);
                }
                
                GUIUtility.ExitGUI();
            }
        }
    }
}