using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Soloco.Composition.Unity.IntegrationTests.Research
{
    [TestClass]
    public class When_getting_a_type
    {
        [TestMethod]
        public void given_type_is_not_registered_but_the_dependency_is()
        {
            var container = new UnityContainer();
            container.RegisterType<IDependency, Dependency>();
            
            var unknowType = container.Resolve<UnknowType>();

            Assert.IsNotNull(unknowType);
            Assert.IsNotNull(unknowType.Dependency);
        }

        [TestMethod]
        public void given_generic_type_is_not_registered_but_the_dependency_is()
        {
            var container = new UnityContainer();
            container.RegisterType<IDependency, Dependency>();

            var unknowType = container.Resolve<UnknowType<string>>();

            Assert.IsNotNull(unknowType);
            Assert.IsNotNull(unknowType.Dependency);
        }
    }
}