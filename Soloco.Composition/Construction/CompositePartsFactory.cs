using System;
using System.Collections.Generic;
using System.Linq;
using Soloco.Composition.InstanceResolver;

namespace Soloco.Composition.Construction
{
    /// <summary>
    /// Factory for CompositeParts objects.
    /// </summary>
    public class CompositePartsFactory : ICompositePartsFactory
    {
        private readonly IInstanceResolver _resolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompositePartsFactory"/> class.
        /// </summary>
        /// <param name="resolver">The resolver.</param>
        public CompositePartsFactory(IInstanceResolver resolver)
        {
            _resolver = resolver;
        }

        /// <summary>
        /// Creates the mapping based on the specified parameters and the specified existing part.
        /// </summary>
        /// <param name="compositionType">Type of the composition.</param>
        /// <param name="partTypes">The types.</param>
        /// <param name="knownPartTypes">The known part types.</param>
        /// <param name="existing">The existing.</param>
        /// <returns></returns>
        public CompositeParts Create(
            Type compositionType,
            IEnumerable<Type> partTypes,
            KnownPartTypes knownPartTypes,
            object existing)
        {
            if (compositionType == null) throw new ArgumentNullException("compositionType");
            if (partTypes == null) throw new ArgumentNullException("partTypes");

            var mapping = new CompositeParts();
            var existingInterfaces = existing == null ? null : existing.GetType().GetInterfaces();

            foreach (var type in partTypes)
            {
                CreatePart(type, knownPartTypes, existing, existingInterfaces, mapping);
            }

            return mapping;
        }

        private void CreatePart(Type partType, KnownPartTypes knownPartTypes, object existing, IEnumerable<Type> existingInterfaces, CompositeParts mapping)
        {
            var implementationType = knownPartTypes == null ? null : knownPartTypes.GetImplementationType(partType);
            if (implementationType != null)
            {
                AddKnownPartType(implementationType, partType, mapping);
            }
            else
            {
                AddExistingParameterValues(existing, partType, existingInterfaces, mapping);
            }
        }

        private void AddKnownPartType(Type implementationType, Type partType, CompositeParts mapping)
        {
            var instance = _resolver.Resolve(implementationType);
            mapping.Add(partType, instance);
        }

        private void AddExistingParameterValues(object existing, Type partType, IEnumerable<Type> existingInterfaces, CompositeParts mapping)
        {
            if (ExistingImplementsPartInterface(existingInterfaces, partType))
            {
                mapping.Add(partType, existing);
            }
            else
            {
                AddTypeValues(partType, mapping);
            }
        }

        private static bool ExistingImplementsPartInterface(IEnumerable<Type> existingInterfaces, Type type)
        {
            return existingInterfaces != null && existingInterfaces.Count(i => i == type) != 0;
        }

        private void AddTypeValues(Type type, CompositeParts mapping)
        {
            var value = GetValue(type);
            if (value != null)
            {
                mapping.Add(type, value);
            }
        }

        private object GetValue(Type partType)
        {
            return _resolver.Resolve(partType);
        }
    }
}
