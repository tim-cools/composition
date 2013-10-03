
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Soloco.Composition.Container;

namespace Soloco.Composition.Container.IntegrationTests
{
    [TestClass]
    public class Generic_mixin_behaviour_research
    {
        [TestMethod]
        public void When_creating_a_single_interface_mixin_the_method_of_the_mixin()
        {
            var container = new CompositionContainer();
            container.Register(typeof(IGenericPart1<>), typeof(GenericPart1<>))
                     .RegisterComposite(typeof(IGenericPart1Composite<>));
            
            var part1 = container.Resolve<IGenericPart1Composite<TestEntity>>();

            Assert.IsNotNull(part1);
            Assert.AreEqual(part1.Method1(), null);
        }

        [TestMethod]
        public void When_creating_a_multiple_interface_mixin_the_method_of_the_mixin()
        {
            var container = new CompositionContainer();
            container.Register(typeof(IGenericCombinedServiceCore), typeof(GenericCombinedServiceCore))
                    .Register(typeof(IGenericPart1<>), typeof(GenericPart1<>))
                    .Register(typeof(INonGenericPart2), typeof(NonGenericPart2))
                    .RegisterComposite<IGenericCombinedService>();

            var instance = container.Resolve<IGenericCombinedService>();

            Assert.IsNotNull(instance);
            Assert.AreEqual(instance.Method1(), null);
            Assert.AreEqual(instance.Method2(), "Method2");
            Assert.AreEqual(instance.Method3(), "Method3");
        }
    }
}