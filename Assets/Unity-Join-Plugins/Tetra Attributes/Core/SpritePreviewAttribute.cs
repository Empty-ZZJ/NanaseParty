using System;
using UnityEngine;

namespace TetraCreations.Attributes
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true)]
    public class SpritePreviewAttribute : PropertyAttribute
    {
        public const CustomColor DefaultBackgroundColor = CustomColor.DarkGray;

        public bool UseAssetPreview { get; private set; }
        public float MaximumHeight { get; private set; }
        public CustomColor BackgroundColor { get; private set; }
        public string BackgroundColorString { get; private set; }

        /// <summary>
        /// Dispaly the texture below a sprite field.
        /// </summary>
        /// <param name="maximumHeight">Maximum height of the preview (With useAssetPreview set to false)</param>
        /// <param name="backgroundColor">The color behind the texture</param>
        /// <param name="useAssetPreview">If true it will use AssetPreview.GetAssetPreview to draw the texture, the maximumHeight doesn't change anyting</param>
        public SpritePreviewAttribute(float maximumHeight = 256f, CustomColor backgroundColor = DefaultBackgroundColor, bool useAssetPreview = false)
        {
            UseAssetPreview = useAssetPreview;
            MaximumHeight = maximumHeight;
            BackgroundColor = backgroundColor;
            BackgroundColorString = ColorUtility.ToHtmlStringRGB(BackgroundColor.ToColor());
        }
    }
}
