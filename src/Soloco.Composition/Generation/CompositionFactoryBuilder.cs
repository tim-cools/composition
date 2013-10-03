
using System;
using System.Globalization;
using System.Reflection;
using System.Security.Permissions;
using Soloco.Composition.Construction;
using Soloco.Composition.InstanceResolver;

namespace Soloco.Composition.Generation
{
    /// <summary>
    /// Creates a composition factory for the specified type.
    /// </summary>
    public class CompositionFactoryBuilder : ICompositionFactoryBuilder
    {
        private GenerationContext _context;
        private Type _type;
        private readonly IInstanceResolver _instanceResolver;
        private ICompositePartsFactory _partsFactory;
        private IThisInjector _injector;
        private IKnownPartTypesResolver _knownTypeResolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompositionFactoryBuilder"/> class.
        /// </summary>
        /// <param name="instanceResolver">The instance resolver.</param>
        public CompositionFactoryBuilder(IInstanceResolver instanceResolver)
        {
            _instanceResolver = instanceResolver;
        }

        /// <summary>
        /// Creates a composition factory for the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        [SecurityPermission(SecurityAction.LinkDemand)]
        public ICompositeFactory Create(Type type)
        {
            if (type == null) throw new ArgumentNullException("type");

            _type = type;

            VerifyTypeIsInterface();
            InitContext();

            return Build();
        }

        private ICompositeFactory Build()
        {
            new CompositionTypeGenerator(_context).Generate();
            var constructor = GetConstructor();
            return CreateFactory(constructor);
        }

        private CompositeFactory CreateFactory(ConstructorInfo constructor)
        {
            //Get dependencies
            _partsFactory = (ICompositePartsFactory)_instanceResolver.Resolve(typeof(ICompositePartsFactory));
            _injector = (IThisInjector)_instanceResolver.Resolve(typeof(IThisInjector));
            _knownTypeResolver = (IKnownPartTypesResolver)_instanceResolver.Resolve(typeof(IKnownPartTypesResolver));
            var knownTypes = _knownTypeResolver.Resolve(_context.CompositionType, _context.PartInterfaces);

            var factory = new CompositeFactory(
                _context.CompositionType,
                constructor,
                knownTypes,
                _partsFactory,
                _injector);
            return factory;
        }

        private ConstructorInfo GetConstructor()
        {
            var constructors = _context.ComposedType.GetConstructors();
            if (constructors.Length != 1)
            {
                throw new InvalidOperationException("Invalid number of constructors found " + constructors.Length);
            }
            return constructors[0];
        }

        private void InitContext()
        {
            _context = new GenerationContext(_type);

            var interfacesResolver = new CompositionPartInterfacesResolver();

            _context.PartInterfaces = interfacesResolver.Resolve(_type);
        }

        private void VerifyTypeIsInterface()
        {
            if (!_type.IsInterface)
            {
                throw new InvalidOperationException(
                    string.Format(CultureInfo.CurrentCulture,
                                  "Composite type '{0}' should be an interface type.", _type.FullName));
            }
        }
    }
}