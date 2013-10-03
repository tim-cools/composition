using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Soloco.Composition.Generation;

namespace Soloco.Composition.Construction
{
    /// <summary>
    /// 
    /// </summary>
    public class KnownPartTypes
    {
        private readonly IList<KnownPartType> _partImplementations = new List<KnownPartType>();
        private readonly IList<KnownPartType> _compositeImplementations = new List<KnownPartType>();

        /// <summary>
        /// Gets the <see cref="KnownPartType"/> with the specified type.
        /// </summary>
        /// <value></value>
        public Type GetImplementationType(Type type)
        {
            if (type == null) throw new ArgumentNullException("type");

            var knownPartTypes = _compositeImplementations;
            var compositeImplementation = GetKnownPart(knownPartTypes, type);
            return compositeImplementation ?? GetKnownPart(_partImplementations, type);
        }

        private static Type GetKnownPart(IEnumerable<KnownPartType> knownPartTypes, Type type)
        {
            var part = knownPartTypes.SingleOrDefault(t => t.Interfaces.FirstOrDefault(i => i == type) != null);
            if (part != null)
            {
                return part.Implementation;
            }
            return GetGenericDefinitionKnownPart(knownPartTypes, type);
        }

        private static Type GetGenericDefinitionKnownPart(IEnumerable<KnownPartType> knownPartTypes, Type type)
        {
            if (!type.IsGenericType) return null;

            var definition = type.GetGenericTypeDefinition();
            var knownPartType = knownPartTypes.FirstOrDefault(t => t.Interfaces.Any(i =>
                                                                                i.Namespace == definition.Namespace
                                                                                && i.Name == definition.Name));
            if (knownPartType == null)
            {
                return null;
            }
            var genericArguments = type.GetGenericArguments();
            return knownPartType.Implementation.MakeGenericType(genericArguments);
        }

        internal void AddCompositeOverrides(Type[] types)
        {
            if (types == null) throw new ArgumentNullException("types");

            foreach (var type in types)
            {
                Add(_compositeImplementations, type);
            }
        }

        internal void AddPartImplementation(Type implementationType)
        {
            Add(_partImplementations, implementationType);
        }

        private static void Add(ICollection<KnownPartType> implementations, Type implementationType)
        {
            if (implementationType == null) throw new ArgumentNullException("implementationType");

            var interfaces = implementationType.GetInterfaces();

            VerifyInterfacesNotExisting(implementations, interfaces);

            var knownPartType = new KnownPartType(interfaces, implementationType);
            implementations.Add(knownPartType);
        }

        private static void VerifyInterfacesNotExisting(IEnumerable<KnownPartType> implementations, IEnumerable<Type> interfaces)
        {
            foreach (var @interface in interfaces)
            {
                VerifyInterfaceNotExisting(@interface, implementations);
            }
        }

        private static void VerifyInterfaceNotExisting(Type @interface, IEnumerable<KnownPartType> implementations)
        {
            if (implementations.Count(p => p.Interfaces.Count(i => i == @interface) > 0) > 0)
            {
                throw new InvalidCompositionException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "Part interface {0} already added.", @interface));
            }
        }
    }
}