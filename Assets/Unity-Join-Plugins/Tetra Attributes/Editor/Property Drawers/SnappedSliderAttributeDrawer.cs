using System;
using UnityEditor;
using UnityEngine;

namespace TetraCreations.Attributes.Editor
{
    [CustomPropertyDrawer(typeof(SnappedSliderAttribute))]
    public class SnappedSliderAttributeDrawer : PropertyDrawer
    {
        private SnappedSliderAttribute _snappedSliderAttribute;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            _snappedSliderAttribute = attribute as SnappedSliderAttribute;

            if (property.propertyType == SerializedPropertyType.Integer)
            {
                int rawInt = EditorGUI.IntSlider(position, label, property.intValue, (int)_snappedSliderAttribute.Min, (int)_snappedSliderAttribute.Max);
                property.intValue = Step(rawInt, _snappedSliderAttribute);
                
            }
            else if(property.propertyType == SerializedPropertyType.Float)
            {
                float rawFloat = EditorGUI.Slider(position, label, property.floatValue, _snappedSliderAttribute.Min, _snappedSliderAttribute.Max);
                property.floatValue = Step(rawFloat, _snappedSliderAttribute);
            }
            else
            {
                EditorGUI.LabelField(position, label.text, "Use Range with int.");
            }
        }

        internal float Step(float rawValue, SnappedSliderAttribute range)
        {
            float value = rawValue;

            if (range.AllowNonStepReach)
            {
                // In order to ensure a reach, where the difference between rawValue and the max allowed value is less than step
                float topCap = (float)Math.Round(Mathf.Floor(range.Max / range.Step) * range.Step, range.Precision);
                float topRemaining = (float)Math.Round(range.Max - topCap, range.Precision);

                // If this is the special case near the top maximum
                if (topRemaining < range.Step && value > topCap + topRemaining / 2)
                {
                    value = range.Max;
                }
                else
                {
                    // Regular snap
                    value = (float)Math.Round(Snap(value, range.Step), range.Precision);
                }
            }
            else if (!range.AllowNonStepReach)
            {
                value = (float)Math.Round(Snap(value, range.Step), range.Precision);
                // Make sure the value doesn't exceed the maximum allowed range
                if (value > range.Max)
                {
                    value -= range.Step;
                    value = (float)Math.Round(value, range.Precision);
                }
            }

            return value;
        }

        internal int Step(int rawValue, SnappedSliderAttribute range)
        {
            int value = rawValue;

            if (range.AllowNonStepReach)
            {
                // In order to ensure a reach, where the difference between rawValue and the max allowed value is less than step
                int topCap = (int)range.Max / (int)range.Step * (int)range.Step;
                int topRemaining = (int)range.Max - topCap;

                // If this is the special case near the top maximum
                if (topRemaining < range.Step && value > topCap)
                {
                    value = (int)range.Max;
                }
                else
                {
                    // Otherwise we do a regular snap
                    value = (int)Snap(value, range.Step);
                }
            }
            else if (!range.AllowNonStepReach)
            {
                value = (int)Snap(value, range.Step);
                // Make sure the value doesn't exceed the maximum allowed range
                if (value > range.Max)
                {
                    value -= (int)range.Step;
                }
            }

            return value;
        }

        /// <summary>
        /// Snap a value to a interval
        /// </summary>
        /// <param name="value"></param>
        /// <param name="snapInterval"></param>
        /// <returns></returns>
        internal static float Snap(float value, float snapInterval)
        {
            return Mathf.Round(value / snapInterval) * snapInterval;
        }
    }
}