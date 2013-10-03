using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Soloco.Composition.Unity.IntegrationTests.Research
{
    [TestClass]
    public class Generic_mixin_behaviour_research
    {
        [TestMethod]
        public void When_creating_a_single_interface_mixin_the_method_of_the_mixin()
        {
            var container = new UnityContainer();
            container
                .RegisterType(typeof(IGenericPart1<>), typeof(GenericPart1<>))
                .AddNewExtension<CompositionExtension>()
                .Configure<CompositionExtension>()
                .RegisterComposite(typeof(IGenericPart1Composite<>));
            
            var part1 = container.Resolve<IGenericPart1Composite<TestEntity>>();

            Assert.IsNotNull(part1);
            Assert.AreEqual(part1.Method1(), null);
        }

        [TestMethod]
        public void When_creating_a_multiple_interface_mixin_the_method_of_the_mixin()
        {
            var container = new UnityContainer();
            container
                .RegisterType<IGenericCombinedServiceCore, GenericCombinedServiceCore>()
                .RegisterType(typeof(IGenericPart1<>), typeof(GenericPart1<>))
                .RegisterType<INonGenericPart2, NonGenericPart2>()
                .AddNewExtension<CompositionExtension>()
                .Configure<CompositionExtension>()
                .RegisterComposite<IGenericCombinedService>();
            
            var instance = container.Resolve<IGenericCombinedService>();

            Assert.IsNotNull(instance);
            Assert.AreEqual(instance.Method1(), null);
            Assert.AreEqual(instance.Method2(), "Method2");
            Assert.AreEqual(instance.Method3(), "Method3");
        }
    }
}