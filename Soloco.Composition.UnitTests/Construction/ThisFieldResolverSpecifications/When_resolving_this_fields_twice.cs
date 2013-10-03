using Microsoft.VisualStudio.TestTools.UnitTesting;

using Soloco.Composition.Construction;

namespace Soloco.Composition.UnitTests.Construction.ThisFieldResolverSpecifications
{
    [TestClass]
    public class When_resolving_this_fields_twice
    {
        [TestMethod]
        public void given_there_are_no_this_fields_then_the_same_instance_should_be_returned()
        {
            var resolver = new ThisFieldResolver();
            var fields = resolver.Resolve(typeof(NoThisFieldObject));
            var fields2 = resolver.Resolve(typeof(NoThisFieldObject));
            Assert.AreSame(fields, fields2);
        }

        [TestMethod]
        public void given_there_is_a_single_this_fields_then_the_same_instance_should_be_returned()
        {
            var resolver = new ThisFieldResolver();
            var fields = resolver.Resolve(typeof(SingleThisFieldObject));
            var fields2 = resolver.Resolve(typeof(SingleThisFieldObject));
            Assert.AreSame(fields, fields2);
        }

        [TestMethod]
        public void given_there_are_multiple_this_fields_then_the_same_instance_should_be_returned()
        {
            var resolver = new ThisFieldResolver();
            var fields = resolver.Resolve(typeof(MultiplThisFieldObject));
            var fields2 = resolver.Resolve(typeof(MultiplThisFieldObject));

            Assert.AreSame(fields, fields2);
        }
    }
}