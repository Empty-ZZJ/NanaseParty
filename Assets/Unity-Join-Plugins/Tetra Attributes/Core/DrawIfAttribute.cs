using UnityEngine;
using System;

namespace TetraCreations.Attributes
{
    /// <summary>
    /// Draws the field/property ONLY if the compared property compared by the comparison type with the value of comparedValue returns true.
    /// Based on: https://forum.unity.com/threads/draw-a-field-only-if-a-condition-is-met.448855/
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
    public class DrawIfAttribute : PropertyAttribute
    {
        #region Fields
        public string ComparedPropertyName { get; private set; }
        public object ComparedValue { get; private set; }
        public DisablingType DisablingType { get; private set; }

        #endregion

        /// <summary>
        /// Only draws the field if the condition is true.<br></br>
        /// Supports Boolean and Enum.
        /// </summary>
        /// <param name="comparedPropertyName">The name of the property that is being compared (case sensitive).</param>
        /// <param name="comparedValue">The value the property is being compared to.</param>
        /// <param name="disablingType">Determine if it will hide the field or make it read only if the condition is NOT met. 
        /// Defaulted to DisablingType.DontDraw.</param>
        public DrawIfAttribute(string comparedPropertyName, object comparedValue, DisablingType disablingType = DisablingType.DontDraw)
        {
            ComparedPropertyName = comparedPropertyName;
            ComparedValue = comparedValue;
            DisablingType = disablingType;
        }
    }

    /// <summary>
    /// Types of comperisons.
    /// </summary>
    public enum DisablingType
    {
        ReadOnly = 2,
        DontDraw = 3
    }
}