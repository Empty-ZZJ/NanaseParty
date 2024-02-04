using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;

namespace TetraCreations.Attributes.Editor
{
    /// <summary>
    /// Used to draw buttons inside inspector using Button attribute on an method by using Reflection.<br></br>
    /// Buttons can be group together by using Row property like so :<br></br>
    /// [Button(nameof(ButtonCallback), "First Button", 100f, row: "first")]
    /// [Button(nameof(ButtonCallback2), "Second Button", 100f, row: "first")]
    /// </summary>
    public class ButtonsDrawer
    {
        public List<IGrouping<string, Button>> ButtonGroups { get; private set; }

        public ButtonsDrawer(object target)
        {
            const BindingFlags flags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
            var methods = target.GetType().GetMethods(flags);
            var buttons = new List<Button>();
            var rowNumber = 0;

            // Create a Button foreach methods with the ButtonAttribute 
            foreach (MethodInfo method in methods)
            {
                var buttonAttribute = method.GetCustomAttribute<ButtonAttribute>();

                if (buttonAttribute == null)
                {
                    continue;
                }

                buttons.Add(new Button(method, buttonAttribute));
            }

            ButtonGroups = buttons.GroupBy(button =>
            {
                var attribute = button.ButtonAttribute;
                var hasRow = attribute.HasRow;
                return hasRow ? attribute.Row : $"__{rowNumber++}";
            }).ToList();
        }

        /// <summary>
        /// Draw buttons by group
        /// </summary>
        /// <param name="targets"></param>
        public void DrawButtons(IEnumerable<object> targets)
        {
            foreach (var buttonGroup in ButtonGroups)
            {
                if (buttonGroup.Count() > 0)
                {
                    var space = buttonGroup.First().ButtonAttribute.Space;
                    if (space != 0) EditorGUILayout.Space(space);
                }
                using (new EditorGUILayout.HorizontalScope())
                {
                    foreach (var button in buttonGroup)
                    {
                        button.Draw(targets);
                    }
                }
            }
        }
    }
}