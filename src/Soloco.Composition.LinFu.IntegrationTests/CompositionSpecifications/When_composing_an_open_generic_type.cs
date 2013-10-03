using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Soloco.Composition.LinFu.IntegrationTests.CompositionSpecifications
{
    [TestClass]
    [Ignore]  //Open generic composites are not yet supported
    public class When_composing_an_open_generic_type
    {
        [TestMethod]
        public void then_the_open_generic_should_be_closed()
        {
            //Create the container
            var container = ContainerFactory.Create();
            container.RegisterComposite(typeof(IOpenComposit<string>));

            IOpenComposit<string> open = container.GetService(typeof(IOpenComposit<string>)) as IOpenComposit<string>;

            //Assert
            Assert.IsNotNull(open);
            open.Get();
        }

        [TestMethod]
        public void then_the_generic_index_parameter_should_be_callable()
        {
            //Create the container
            var container = ContainerFactory.Create();
            container.RegisterComposite(typeof(IOpenComposit<>));

            IOpenComposit<string> open = container.GetService(typeof(IOpenComposit<string>)) as IOpenComposit<string>;

            //Assert
            Assert.IsNotNull(open);
            string result = open[new Tuple<string>("Part1", "Part2")];
            Assert.AreEqual("Part1", result);
        }
    }
}