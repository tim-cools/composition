
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Soloco.Composition.Container.IntegrationTests.CompositionSpecifications
{
    [TestClass]
    [Ignore]
    //"This test is not relevant, the composite types are compiled during initialization"
    public class When_composing_multiple_composits_of_the_same_type
    {
        [TestMethod]
        public void then_the_construction_should_be_a_lot_faster_from_the_second_construction()
        {
            //Create the container
            var container = ContainerFactory.Create();

            //Get the first animal
            IPartyDeveloper first, second, third;
            var milliseconds = CreatePartyDeveloper(container, out first);
            var secondMilliseconds = CreatePartyDeveloper(container, out second);
            var thirdMilliseconds = CreatePartyDeveloper(container, out third);

            Console.WriteLine("1st : " + milliseconds);
            Console.WriteLine("2nd : " + secondMilliseconds);
            Console.WriteLine("3rd: " + thirdMilliseconds);

            Assert.AreNotEqual(first, second);
            Assert.AreNotEqual(second, third);

            Assert.IsTrue(secondMilliseconds < milliseconds / 10);
            Assert.IsTrue(thirdMilliseconds < milliseconds / 10);
        }

        private static double CreatePartyDeveloper(ICompositionContainer container, out IPartyDeveloper partyDeveloper)
        {
            var start = DateTime.Now;
            partyDeveloper = container.Compose<IPartyDeveloper>();
            return (DateTime.Now - start).TotalMilliseconds;
        }
    }
}