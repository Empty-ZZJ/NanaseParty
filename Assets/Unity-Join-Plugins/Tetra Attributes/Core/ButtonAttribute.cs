using System;

namespace TetraCreations.Attributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class ButtonAttribute : Attribute
    {
        public string MethodName { get; private set; }
        public string Label { get; private set; }
        public int Space { get; private set; }
        public string Row { get; private set; }
        public bool HasRow { get; private set; }

        public ButtonAttribute(string methodName, string label = "", float width = default, int space = default, string row = default)
        {
            MethodName = methodName;
            Label = label;
            Space = space;
            Row = row;
            HasRow = !string.IsNullOrEmpty(Row);
        }
    }
}