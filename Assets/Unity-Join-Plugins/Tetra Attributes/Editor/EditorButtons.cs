using UnityEditor;
using UnityEngine;

namespace TetraCreations.Attributes.Editor
{
    /// <summary>
    /// Custom editor for UnityEngine.Object, this will instantiate a ButtonsDrawer.<br></br>
    /// Inside OnInspectorGUI() it will simply draw the default inspector.<br></br>
    /// Then it will draw all buttons. <br></br>
    /// You need to inherit from this class if you want to display buttons inside your custom editor.
    /// </summary>
    [CustomEditor(typeof(Object), true), CanEditMultipleObjects]
    public class EditorButtons : UnityEditor.Editor
    {
        /// <summary>
        /// Set to false if you don't want the buttons to be drawned
        /// </summary>
        private bool _enabled = true;

        private ButtonsDrawer _buttonsDrawer;

        protected virtual void OnEnable()
        {
            _buttonsDrawer = new ButtonsDrawer(target);
        }

        public override void OnInspectorGUI()
        {
            if (serializedObject == null) { return; }

            DrawDefaultInspector();

            if (_enabled && _buttonsDrawer != null)
            {
                _buttonsDrawer.DrawButtons(targets);
            }
        }
    }
}
