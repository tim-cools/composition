using System;
using System.Collections.Generic;

namespace Soloco.Composition.Construction
{
    /// <summary>
    /// Interface for the Resolver of the KnownPart Types.
    /// </summary>
    public interface IKnownPartTypesResolver
    {
        /// <summary>
        /// Resolves the KnownPart Types.
        /// </summary>
        /// <param name="compositionType">The type of the composition</param>
        /// <param name="interfaces">The types of the interfaces</param>
        /// <returns>The found KnownPart Types.</returns>
        KnownPartTypes Resolve(Type compositionType, IEnumerable<Type> interfaces);
    }
}