using System;
using UnityEngine;

namespace TetraCreations.Attributes
{
    public enum HelpBoxMessageType { None, Info, Warning, Error }

    [AttributeUsage(AttributeTargets.Field, Inherited = true)]
    public class HelpBoxAttribute : PropertyAttribute
    {
        public string Text { get; private set; }
        public HelpBoxMessageType MessageType { get; private set; }
        public float MinimumHeight { get; private set; }
        public int FontSize { get; private set; }

        public HelpBoxAttribute(string text, HelpBoxMessageType messageType = HelpBoxMessageType.None, float minimumHeight = 20, int fontSize = 12)
        {
            Text = text;
            MessageType = messageType;
            MinimumHeight = minimumHeight;
            FontSize = fontSize;
        }
    }
}
