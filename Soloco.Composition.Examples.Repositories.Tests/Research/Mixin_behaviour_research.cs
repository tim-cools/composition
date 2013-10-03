using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Soloco.Composition.Unity;

namespace Soloco.Composition.Examples.Repositories.Tests.Research
{
    [TestClass]
    public class Mixin_behaviour_research
    {
        [TestMethod]
        public void When_creating_a_single_interface_mixin_the_method_of_the_mixin()
        {
            var container = new UnityContainer();
            container
                .RegisterType<IPart1, Part1>()
                .AddNewExtension<CompositionExtension>()
                .Configure<CompositionExtension>()
                   .RegisterComposite<IPart1Composite>();
            
            var part1 = container.Resolve<IPart1Composite>();

            Assert.IsNotNull(part1);
            Assert.AreEqual(part1.Method1(), "Method1");
        }

        [TestMethod]
        public void When_creating_a_multiple_interface_mixin_the_methods_of_the_mixin_should_be_reachable()
        {
            var container = new UnityContainer();
            container
                .RegisterType<ICombinedServiceCore, CombinedServiceCore>()
                .RegisterType<IPart1, Part1>()
                .RegisterType<IPart2, Part2>()
                .AddNewExtension<CompositionExtension>()
                .Configure<CompositionExtension>()
                   .RegisterComposite<ICombinedService>();
            
            
            var instance = container.Resolve<ICombinedService>();
            
            Assert.IsNotNull(instance);
            Assert.AreEqual(instance.Method1(), "Method1");
            Assert.AreEqual(instance.Method2(), "Method2");
            Assert.AreEqual(instance.Method3(), "Method3");
        }
    }
}