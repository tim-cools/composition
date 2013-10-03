using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace Soloco.Composition.Construction
{
    /// <summary>
    /// Resolves the KnownPart by Attribute.
    /// </summary>
    public class AttributeKnownPartTypesResolver : IKnownPartTypesResolver
    {
        private KnownPartTypes _knownPartTypes;

        /// <summary>
        /// Resolves the KnownParts by Attribute.
        /// </summary>
        public KnownPartTypes Resolve(Type compositionType, IEnumerable<Type> interfaces)
        {
            if (interfaces == null) throw new ArgumentNullException("interfaces");
            
            VerifyIsInterface(compositionType);

            _knownPartTypes = new KnownPartTypes();

            GetPartInterfaceKnownPartTypes(interfaces);
            OverrideWithCompositeKnownPartTypes(compositionType);

            return _knownPartTypes;
        }

        private static void VerifyIsInterface(Type type)
        {
            if (type == null) throw new ArgumentNullException("type");

            if (!type.IsInterface)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Type {0} is no interface.", type));
            }
        }

        private void OverrideWithCompositeKnownPartTypes(ICustomAttributeProvider compositionType)
        {
            var attribute = compositionType
                .GetCustomAttributes(typeof(CompositeAttribute), true)
                .SingleOrDefault() as CompositeAttribute;
            if (attribute != null)
            {
                _knownPartTypes.AddCompositeOverrides(attribute.Types);
            }
        }

        private void GetPartInterfaceKnownPartTypes(IEnumerable<Type> partInterfaces)
        {
            foreach (var partInterface in partInterfaces)
            {
                GetPartInterfaceKnownPartTypes(partInterface);
            }
        }

        private void GetPartInterfaceKnownPartTypes(Type partInterface)
        {
            VerifyIsInterface(partInterface);

            var attribute = partInterface
                .GetCustomAttributes(typeof (ImplementationAttribute), true)
                .SingleOrDefault() as ImplementationAttribute;
            if (attribute != null)
            {
                _knownPartTypes.AddPartImplementation(attribute.ImplementationType);
            }
        }
    }
}