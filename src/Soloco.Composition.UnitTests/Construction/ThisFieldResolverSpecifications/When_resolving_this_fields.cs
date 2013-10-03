using Microsoft.VisualStudio.TestTools.UnitTesting;

using Soloco.Composition.Construction;

using System;

namespace Soloco.Composition.UnitTests.Construction.ThisFieldResolverSpecifications
{
    [TestClass]
    public class When_resolving_this_fields
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void given_null_is_passed_then_a_argument_null_excetpion_should_be_thrown()
        {
            var resolver = new ThisFieldResolver();
            resolver.Resolve(null);
        }

        [TestMethod]
        public void given_there_are_no_this_fields_then_no_ThisField_instances_should_be_returned()
        {
            var resolver = new ThisFieldResolver();
            var fields = resolver.Resolve(typeof(NoThisFieldObject));

            Assert.IsNotNull(fields);
            Assert.AreEqual(0, fields.Count);
        }

        [TestMethod]
        public void given_there_is_a_single_this_fields_then_a_single_ThisField_instances_should_be_returned()
        {
            var resolver = new ThisFieldResolver();
            var fields = resolver.Resolve(typeof(SingleThisFieldObject));

            Assert.IsNotNull(fields);
            Assert.AreEqual(1, fields.Count);
            Assert.AreEqual("_child1", fields[0].Field.Name);
        }

        [TestMethod]
        public void given_there_are_multiple_this_fields_then_multiple_ThisField_instances_should_be_returned()
        {
            var resolver = new ThisFieldResolver();
            var fields = resolver.Resolve(typeof(MultiplThisFieldObject));

            Assert.IsNotNull(fields);
            Assert.AreEqual(3, fields.Count);
            Assert.AreEqual("_child1", fields[0].Field.Name);
            Assert.AreEqual("_child2", fields[1].Field.Name);
            Assert.AreEqual("_child3", fields[2].Field.Name);
        }
    }
}