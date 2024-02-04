using UnityEditor;
using UnityEngine;

namespace TetraCreations.Attributes.Editor
{
    [CustomPropertyDrawer(typeof(RequiredAttribute))]
    public class RequiredPropertyDrawer : PropertyDrawer
    {
        private Rect _propertyRect;
        private Rect _helpBoxRect;

        private bool _drawHelpbox = false;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (_drawHelpbox)
            {
                return EditorGUI.GetPropertyHeight(property, label, true) + 40f;
            }

            return EditorGUI.GetPropertyHeight(property, label, true);
        }

        private void SetElementsPositions(Rect position)
        {
            if(_drawHelpbox == false) { return; }

            _propertyRect = new Rect(position.xMin, position.yMin + 40f, position.width, position.height);
            _helpBoxRect = new Rect(position.xMin, position.yMin, position.width, position.height - 20f);
        }

        /// <summary>
        /// Determine if the SerializedProperty value is null
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        private bool IsNull(SerializedProperty property)
        {
            switch (property.propertyType)
            {
                case SerializedPropertyType.String:
                    return string.IsNullOrEmpty(property.stringValue);
                case SerializedPropertyType.ObjectReference:
                    return property.objectReferenceValue == null;
                case SerializedPropertyType.ExposedReference:
                    return property.exposedReferenceValue == null;
                #if UNITY_2021_2_OR_NEWER
                case SerializedPropertyType.ManagedReference:
                    return property.managedReferenceValue == null;
                #endif
            }

            return false;
        }
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            _drawHelpbox = false;

            if (IsPropertyTypeSupported(property.propertyType) == false)
            {
                _drawHelpbox = true;
                SetElementsPositions(position);

                EditorGUI.HelpBox(_helpBoxRect,
                                  string.Format("{0} doesn't support the property type : {1}", nameof(RequiredAttribute), property.propertyType),
                                  MessageType.Warning);

                EditorGUI.PropertyField(_propertyRect, property, label, true);
                return;
            }

            if (IsNull(property))
            {
                _drawHelpbox = true;
                SetElementsPositions(position);
                EditorGUI.HelpBox(_helpBoxRect, string.Format("{0} field value is required.", property.name), MessageType.Error);
                EditorGUI.PropertyField(_propertyRect, property, label, true);
                return;
            }

            // Drawing Property
            EditorGUI.PropertyField(position, property, label, true);
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
                case SerializedPropertyType.String:
                    return true;
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