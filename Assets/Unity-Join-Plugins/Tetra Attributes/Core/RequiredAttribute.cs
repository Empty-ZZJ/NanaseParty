using System;
using UnityEngine;

namespace TetraCreations.Attributes
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true)]
    public class RequiredAttribute : PropertyAttribute
    {

    }
}