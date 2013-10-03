using System;

namespace Soloco.Composition.InstanceResolver
{
    /// <summary>
    /// Resolves the instance needed to create a composite.
    /// </summary>
    public interface IInstanceResolver
    {
        /// <summary>
        /// Resolves an instance of the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        object Resolve(Type type);
    }
}