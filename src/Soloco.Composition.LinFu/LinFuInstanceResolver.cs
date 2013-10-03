
using System;
using LinFu.IoC;
using LinFu.IoC.Interfaces;
using Soloco.Composition.InstanceResolver;

namespace Soloco.Composition.LinFu
{
    class LinFuInstanceResolver : IInstanceResolver
    {
        private readonly IServiceContainer _container;

        public LinFuInstanceResolver(IServiceContainer container)
        {
            if (container == null) throw new ArgumentNullException("container");

            _container = container;
        }

        public object Resolve(Type type)
        {
            return _container.Contains(type) 
                ? _container.GetService(type)
                : _container.AutoCreate(type);
        }
    }
}