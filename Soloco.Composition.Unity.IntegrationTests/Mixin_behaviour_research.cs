using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Soloco.Composition.Unity;

namespace Soloco.Composition.Unity.IntegrationTests
{
    [TestClass]
    public class Mixin_behaviour_research
    {
        [TestMethod]
        public void When_creating_a_single_interface_composite_the_method_of_the_mixin()
        {
            var container = new UnityContainer();
            container
                .RegisterType<IPart1, Part1>()
                .AddNewExtension<CompositionExtension>()
                .Configure<CompositionExtension>()
                .RegisterComposite<IPart1Composite>();

            var composition = container.Resolve<IPart1Composite>();

            Assert.IsNotNull(composition);
            Assert.AreEqual(composition.Method1(), "Method1");
        }

        [TestMethod]
        public void When_creating_a_multiple_interface_composite_the_methods_of_the_mixin_should_be_reachable()
        {
            var container = new UnityContainer();
            container
                .RegisterType<IPart1, Part1>()
                .RegisterType<IPart2, Part2>()
                .AddNewExtension<CompositionExtension>()
                .Configure<CompositionExtension>()
                   .RegisterComposite<ICombinedType>();

            var composition = container.Resolve<ICombinedType>();

            Assert.IsNotNull(composition);
            Assert.AreEqual(composition.Method1(), "Method1");
            Assert.AreEqual(composition.Method2(), "Method2");
        }
    }
}