using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Soloco.Composition.Construction;

namespace Soloco.Composition.Generation
{
    /// <summary>
    /// Creates a composite.
    /// </summary>
    public class CompositeFactory : ICompositeFactory
    {
        #region private fields

        private readonly Type _compositionType;
        private readonly KnownPartTypes _knownPartTypes; 
        private readonly ConstructorInfo _constructor;
        private readonly ICompositePartsFactory _partsFactory;
        private readonly IThisInjector _thisInjector;
        private readonly IEnumerable<Type> _partTypes;

        #endregion

        #region constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="CompositeFactory"/> class.
        /// </summary>
        /// <param name="compositionType">Type of the composition.</param>
        /// <param name="constructor">The constructor.</param>
        /// <param name="knownPartTypes">The known part types.</param>
        /// <param name="partsFactory">The parts factory.</param>
        /// <param name="thisInjector">The this injector.</param>
        public CompositeFactory(
            Type compositionType, 
            ConstructorInfo constructor,
            KnownPartTypes knownPartTypes,
            ICompositePartsFactory partsFactory, 
            IThisInjector thisInjector)
        {
            if (compositionType == null) throw new ArgumentNullException("compositionType");
            if (constructor == null) throw new ArgumentNullException("constructor");
            if (partsFactory == null) throw new ArgumentNullException("partsFactory");
            if (thisInjector == null) throw new ArgumentNullException("thisInjector");
            if (knownPartTypes == null) throw new ArgumentNullException("knownPartTypes");

            _compositionType = compositionType;
            _constructor = constructor;
            _partsFactory = partsFactory;
            _thisInjector = thisInjector;
            _knownPartTypes = knownPartTypes;
            _partTypes = GetPartTypes();
        }

        #endregion

        #region methods

        /// <summary>
        /// Creates a composite.
        /// </summary>
        /// <returns></returns>
        public object Create()
        {
            var mapping = GetCompositeParts(null);
            var instance = Instantiate(mapping.GetInstances());

            InjectThis(mapping);
            
            return instance;
        }

        /// <summary>
        /// Creates a composite based on the specified existing part instance.
        /// </summary>
        /// <param name="existing"></param>
        /// <returns></returns>
        public object Compose(object existing)
        {
            if (existing == null) throw new ArgumentNullException("existing");

            var compositeParts = GetCompositeParts(existing);
            var compositePartInstances = compositeParts.GetInstances();
            var instance = Instantiate(compositePartInstances);

            InjectThis(compositeParts);

            return instance;
        }

        private CompositeParts GetCompositeParts(object existing)
        {
            return _partsFactory.Create(_compositionType, _partTypes, _knownPartTypes, existing);
        }

        private void InjectThis(CompositeParts parts)
        {
            _thisInjector.Inject(parts);
        }

        private object Instantiate(object[] parts)
        {
            return _constructor.Invoke(parts);
        }

        private Type[] GetPartTypes()
        {
            return _constructor.GetParameters().Select(p => p.ParameterType).ToArray();
        }

        #endregion
    }
}