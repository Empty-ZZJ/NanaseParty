using System.Collections;
using UnityEditor;
using UnityEngine;

namespace TetraCreations.Attributes.Editor
{
    [CustomPropertyDrawer(typeof(DrawIfAttribute))]
    public class DrawIfPropertyDrawer : PropertyDrawer
    {
        private DrawIfAttribute _drawIf;
        private SerializedProperty _comparedField;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (!ShowMe(property) && _drawIf.DisablingType == DisablingType.DontDraw)
            {
                return -EditorGUIUtility.standardVerticalSpacing;
            }
            else
            {
                if (property.propertyType == SerializedPropertyType.Generic)
                {
                    int numChildren = 0;
                    float totalHeight = 0.0f;

                    IEnumerator children = property.GetEnumerator();

                    while (children.MoveNext())
                    {
                        SerializedProperty child = children.Current as SerializedProperty;

                        GUIContent childLabel = new GUIContent(child.displayName);

                        totalHeight += EditorGUI.GetPropertyHeight(child, childLabel) + EditorGUIUtility.standardVerticalSpacing;
                        numChildren++;
                    }

                    // Remove extra space at end, (we only want spaces between items)
                    totalHeight -= EditorGUIUtility.standardVerticalSpacing;

                    return totalHeight;
                }

                return EditorGUI.GetPropertyHeight(property, label);
            }
        }

        private bool ShowMe(SerializedProperty property)
        {
            _drawIf = attribute as DrawIfAttribute;

            _comparedField = TryToFindSerializableProperty(_drawIf.ComparedPropertyName, property);

            // We check that exist a Field with the parameter name
            if (_comparedField == null)
            {
                Debug.Log("Error getting the condition Field. Check the name.");
                return true;
            }

            // get the value & compare based on types
            switch (_comparedField.type)
            { // Possible extend cases to support your own type
                case "bool":
                    return _comparedField.boolValue.Equals(_drawIf.ComparedValue);
                case "Enum":
                    return (_comparedField.intValue & (int)_drawIf.ComparedValue) == (int)_drawIf.ComparedValue;
                default:
                    Debug.LogError("Error: " + _comparedField.type + " is not supported");
                    return true;
            }
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // If the condition is met, simply draw the field.
            if (ShowMe(property))
            {
                // A Generic type means a custom class...
                if (property.propertyType == SerializedPropertyType.Generic)
                {
                    IEnumerator children = property.GetEnumerator();

                    Rect offsetPosition = position;

                    while (children.MoveNext())
                    {
                        SerializedProperty child = children.Current as SerializedProperty;

                        GUIContent childLabel = new GUIContent(child.displayName);

                        float childHeight = EditorGUI.GetPropertyHeight(child, childLabel);
                        offsetPosition.height = childHeight;

                        EditorGUI.PropertyField(offsetPosition, child, childLabel);

                        offsetPosition.y += childHeight + EditorGUIUtility.standardVerticalSpacing;
                    }
                }
                else
                {
                    EditorGUI.PropertyField(position, property, label);
                }

            } //...check if the disabling type is read only. If it is, draw it disabled
            else if (_drawIf.DisablingType == DisablingType.ReadOnly)
            {
                GUI.enabled = false;
                EditorGUI.PropertyField(position, property, label);
                GUI.enabled = true;
            }
        }

        /// <summary>
        /// Return SerializedProperty by it's name if it exists, it works for nested objects and arrays
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        private SerializedProperty TryToFindSerializableProperty(string propertyName, SerializedProperty property)
        {
            var serializedProperty = property.serializedObject.FindProperty(propertyName);

            // Find relative
            if (serializedProperty == null)
            {
                string propertyPath = property.propertyPath;
                int idx = propertyPath.LastIndexOf('.');
                if (idx != -1)
                {
                    propertyPath = propertyPath.Substring(0, idx);
                    return property.serializedObject.FindProperty(propertyPath).FindPropertyRelative(propertyName);
                }
            }

            return serializedProperty;
        }
    }
}