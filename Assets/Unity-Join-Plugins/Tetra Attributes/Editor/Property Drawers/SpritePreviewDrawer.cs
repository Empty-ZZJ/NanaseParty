using UnityEditor;
using UnityEngine;

namespace TetraCreations.Attributes.Editor
{
    /// <summary>
    /// Draw a preview for a sprite, we can choose to use AssetPreview.GetAssetPreview or loading the original sprite texture
    /// to be able to choose the maximum height of the preview
    /// </summary>
    [CustomPropertyDrawer(typeof(SpritePreviewAttribute))]
    public class SpritePreviewDrawer : PropertyDrawer
    {
        SpritePreviewAttribute _previewAttribute;
        Texture2D _texture;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if(_previewAttribute == null) { _previewAttribute = (SpritePreviewAttribute)attribute; }

            if (IsPropertyTypeSupported(property.propertyType))
            {
                // Draw normal sprite property field
                EditorGUILayout.PropertyField(property);

                if (_previewAttribute.UseAssetPreview)
                {
                    _texture = AssetPreview.GetAssetPreview(property.objectReferenceValue);
                }
                else
                {
                    Sprite sprite = property.objectReferenceValue as Sprite;
                    if (sprite != null)
                    {
                        _texture = sprite.texture;
                    }
                }

                // Without texture we leave there
                if(_texture == null) { return; }

                // Drawing the texture preview
                GUILayout.Label("Preview");
                EditorGUILayout.BeginVertical("box");
                var horizRect = EditorGUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();

                // Sprite will be displayed on top of the rectangle using custom background color
                EditorGUI.DrawRect(
                    new Rect(horizRect.x + 2.5f, horizRect.y, horizRect.width - 2.5f, horizRect.height), 
                    _previewAttribute.BackgroundColor.ToColor());

                GUILayout.Label(_texture, GUILayout.MaxHeight(_previewAttribute.MaximumHeight), GUILayout.ExpandWidth(true));
                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.EndVertical();
            }
        }

        /// <summary>
        /// Determine if the SerializedPropertyType is supported by the Required attribute<br></br>
        /// Unity versions before the 2021.2 doesn't have a getter for SerializedPropertyType.ManagedReference.
        /// </summary>
        /// <param name="serializedPropertyType"></param>
        /// <returns></returns>
        private bool IsPropertyTypeSupported(SerializedPropertyType serializedPropertyType)
        {
            switch (serializedPropertyType)
            {
                case SerializedPropertyType.ObjectReference:
                    return true;
                case SerializedPropertyType.ExposedReference:
                    return true;
                #if UNITY_2021_2_OR_NEWER
                case SerializedPropertyType.ManagedReference:
                    return true;
                #endif
            }

            return false;
        }
    }
}
