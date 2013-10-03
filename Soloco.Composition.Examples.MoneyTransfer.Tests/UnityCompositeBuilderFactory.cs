using System;
using Microsoft.Practices.Unity;

namespace Soloco.Composition.Examples.MoneyTransfer.Tests
{
    public class UnityCompositeBuilderFactory : ICompositeBuilderFactory
    {
        private readonly IUnityContainer _container;

        public UnityCompositeBuilderFactory(IUnityContainer container)
        {
            if (container == null) throw new ArgumentNullException("container");
            
            _container = container;
        }

        public T NewComposite<T>()
        {
            return _container.Resolve<T>();
        }
    }
}