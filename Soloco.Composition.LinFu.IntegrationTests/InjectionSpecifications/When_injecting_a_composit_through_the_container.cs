
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LinFu.IoC;

namespace Soloco.Composition.LinFu.IntegrationTests.InjectionSpecifications
{
    [TestClass]
    public class When_injecting_a_composit_through_the_container
    {
        [TestMethod]
        public void then_the_dependency_should_be_injected()
        {
            //Create the container
            var container = ContainerFactory.Create();
            var resolver = new LinFuInstanceResolver(container);

            //Get the service
            var service = resolver.Resolve(typeof(DeveloperService)) as DeveloperService;

            //Assert
            Assert.IsNotNull(service);
            Assert.IsNotNull(service.Developer);
        }
    }
}