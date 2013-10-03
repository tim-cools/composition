using LinFu.IoC;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Soloco.Composition.LinFu.IntegrationTests
{
    [TestClass]
    public class Generic_mixin_behaviour_research
    {
        [TestMethod]
        public void When_creating_a_single_interface_mixin_the_method_of_the_mixin()
        {
            var container = new ServiceContainer();
            container.AddService(typeof(IGenericPart1<>), typeof(GenericPart1<>));
            container.RegisterComposite(typeof(IGenericPart1Composite<>));
            
            var part1 = container.GetService<IGenericPart1Composite<TestEntity>>();

            Assert.IsNotNull(part1);
            Assert.AreEqual(part1.Method1(), null);
        }

        [TestMethod]
        public void When_creating_a_multiple_interface_mixin_the_method_of_the_mixin()
        {
            var container = new ServiceContainer();
            container.AddService(typeof(IGenericCombinedServiceCore), typeof(GenericCombinedServiceCore));
            container.AddService(typeof(IGenericPart1<>), typeof(GenericPart1<>));
            container.AddService(typeof(INonGenericPart2), typeof(NonGenericPart2));
            container.RegisterComposite<IGenericCombinedService>();

            var instance = container.GetService<IGenericCombinedService>();

            Assert.IsNotNull(instance);
            Assert.AreEqual(instance.Method1(), null);
            Assert.AreEqual(instance.Method2(), "Method2");
            Assert.AreEqual(instance.Method3(), "Method3");
        }
    }
}