using System;
using System.Reflection;

namespace Soloco.Composition.Generation
{
    internal static class AssemblyExtensions
    {
        internal static bool IsDynamic(this Assembly assembly)
        {
            return assembly.ManifestModule.Name.StartsWith("<", StringComparison.Ordinal);
        }
    }
}