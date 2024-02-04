using System.Linq;
using UnityEngine;

namespace TetraCreations.Attributes
{
    public class SnappedSliderAttribute : PropertyAttribute
    {
        public float Step { get; private set; }
        public float Min { get; private set; }
        public float Max { get; private set; }
        public int Precision { get; private set; }
        public bool AllowNonStepReach { get; private set; }
        public bool IsInt { get; private set; }

        /// <summary>
        /// Increase a float value in step<br></br>
        /// Value is clamped by min and max parameters
        /// </summary>
        /// <param name="step">Value to add</param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public SnappedSliderAttribute(float step, float min, float max)
        {
            Step = step;
            Min = min;
            Max = max;
            Precision = CountFloatDigits(step);
        }

        /// <summary>
        /// Increase an int value in step<br></br>
        /// Value is clamped by min and max parameters
        /// </summary>
        /// <param name="step">Value to add</param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="allowNonStepReach"></param>
        public SnappedSliderAttribute(int step, int min, int max, bool allowNonStepReach = true)
        {
            Min = min;
            Max = max;
            Step = step;
            AllowNonStepReach = allowNonStepReach;
            IsInt = true;
        }

        private int CountFloatDigits(float n, int precisionLimit = 7)
        {
            return Mathf.Min(n.ToString(System.Globalization.CultureInfo.InvariantCulture)
                .SkipWhile(c => c != '.')
                .Skip(1)
                .Count(),
                precisionLimit);
        }
    }
}