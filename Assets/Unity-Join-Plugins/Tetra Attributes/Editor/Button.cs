using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace TetraCreations.Attributes.Editor
{
    /// <summary>
    /// Store MethodInfo to invoke it on click<br></br>
    /// By default the button label will be the method name
    /// </summary>
    public class Button
    {
        public string Label { get; private set; }
        public MethodInfo Method { get; private set; }
        public ButtonAttribute ButtonAttribute { get; private set; }

        public Button(MethodInfo method, ButtonAttribute buttonAttribute)
        {
            ButtonAttribute = buttonAttribute;
            Label = string.IsNullOrEmpty(buttonAttribute.Label) ? ObjectNames.NicifyVariableName(method.Name) : buttonAttribute.Label;
            Method = method;
        }

        internal void Draw(IEnumerable<object> targets)
        {
            if (!GUILayout.Button(Label)) return;

            foreach (object target in targets)
            {
                Method.Invoke(target, null);
            }
        }
    }
}
