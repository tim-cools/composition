
using System;

namespace Soloco.Composition.Construction
{
    /// <summary>
    /// Attribute that mark private fields that need to injected with parts
    /// of the composite.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = true)]
    public sealed class ThisAttribute : Attribute
    {
    }
}