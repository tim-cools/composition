using LinFu.IoC;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Soloco.Composition.LinFu.IntegrationTests.Research
{
    [TestClass]
    public class Mixin_behaviour_research
    {
        [TestMethod]
        public void When_creating_a_single_interface_composite_the_method_of_the_mixin()
        {
            var container = new ServiceContainer();
            container.Inject<IPart1>().Using<Part1>().AsSingleton();
            container.RegisterComposite<IPart1Composite>();

            var composition = container.GetService<IPart1Composite>();

            Assert.IsNotNull(composition);
            Assert.AreEqual(composition.Method1(), "Method1");
        }

        [TestMethod]
        public void When_creating_a_multiple_interface_composite_the_methods_of_the_mixin_should_be_reachable()
        {
            var container = new ServiceContainer();
            container.Inject<IPart1>().Using<Part1>().AsSingleton();
            container.Inject<IPart2>().Using<Part2>().AsSingleton();
            container.RegisterComposite<ICombinedType>();

            var composition = container.GetService<ICombinedType>();

            Assert.IsNotNull(composition);
            Assert.AreEqual(composition.Method1(), "Method1");
            Assert.AreEqual(composition.Method2(), "Method2");
        }
    }
}