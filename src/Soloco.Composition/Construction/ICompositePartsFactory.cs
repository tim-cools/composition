using System.Collections.Generic;
using System;

namespace Soloco.Composition.Construction
{
    /// <summary>
    /// Factory for CompositionParts.
    /// </summary>
    public interface ICompositePartsFactory
    {
        /// <summary>
        /// Creates the mapping based on the specified parameters and the specified existing part.
        /// </summary>
        /// <param name="compositionType">Type of the composition.</param>
        /// <param name="partTypes">The types.</param>
        /// <param name="knownPartTypes">The know part types.</param>
        /// <param name="existing">The existing.</param>
        /// <returns></returns>
        CompositeParts Create(
            Type compositionType, 
            IEnumerable<Type> partTypes, 
            KnownPartTypes knownPartTypes,
            object existing);
    }
}