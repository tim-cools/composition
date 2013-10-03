using System;
using LinFu.IoC.Interfaces;
using Soloco.Composition.Generation;

namespace Soloco.Composition.LinFu
{
    internal class LinFuCompositeFactory : IFactory
    {
        private readonly ICompositionFactoryBuilder _compositionFactoryBuilder;

        /// <summary>
        /// Initializes a new instance of the <see cref="LinFuCompositeFactory"/> class.
        /// </summary>
        public LinFuCompositeFactory(ICompositionFactoryBuilder compositionFactoryBuilder)
        {
            if (compositionFactoryBuilder == null) throw new ArgumentNullException("compositionFactoryBuilder");

            _compositionFactoryBuilder = compositionFactoryBuilder;
        }

        /// <summary>
        /// Creates a service instance using the given <see cref="T:LinFu.IoC.Interfaces.IFactoryRequest"/> instance.
        /// </summary>
        /// <param name="request">The <see cref="T:LinFu.IoC.Interfaces.IFactoryRequest"/> instance that describes the requested service.</param>
        /// <returns>
        /// An object instance that represents the service to be created. This cannot be <c>null</c>.
        /// </returns>
        public object CreateInstance(IFactoryRequest request)
        {
            var factory = _compositionFactoryBuilder.Create(request.ServiceType);
            return factory.Create();
        }
    }
}