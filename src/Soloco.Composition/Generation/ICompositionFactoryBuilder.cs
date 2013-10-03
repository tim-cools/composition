
using System;
using System.Security.Permissions;

namespace Soloco.Composition.Generation
{
    /// <summary>
    /// Creates a composition factory for the specified type.
    /// </summary>
    public interface ICompositionFactoryBuilder
    {
        /// <summary>
        /// Creates a composition factory for the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        [SecurityPermission(SecurityAction.LinkDemand)]
        ICompositeFactory Create(Type type);
    }
}