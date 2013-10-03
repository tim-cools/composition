
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Soloco.Composition.Container.IntegrationTests.InjectionSpecifications
{
    [TestClass]
    public class When_injecting_a_composit_through_the_container
    {
        [TestMethod]
        public void then_the_dependency_should_be_injected()
        {
            //Create the container
            var container = ContainerFactory.Create();

            //Get the service
            var service = container.Resolve<DeveloperService>();

            //Assert
            Assert.IsNotNull(service);
            Assert.IsNotNull(service.Developer);
        }
    }
}