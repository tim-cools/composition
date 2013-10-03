using Microsoft.VisualStudio.TestTools.UnitTesting;

using Soloco.Composition.Construction;

using System;
using System.Reflection;

namespace Soloco.Composition.UnitTests.Construction.ThisFieldSpecifications
{
    [TestClass]
    public class When_injecting
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void given_null_is_passed_when_constructing_then_a_argument_null_excetpion_should_be_thrown()
        {
            new ThisField(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void given_null_is_passed_as_parent_then_a_argument_null_excetpion_should_be_thrown()
        {
            var field = typeof(ThisFieldObject).GetField("_child1", BindingFlags.Instance | BindingFlags.NonPublic);
            var thisField = new ThisField(field);
            var mapping = new CompositeParts();

            thisField.Inject(null, mapping);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void given_null_is_passed_as_mapping_then_a_argument_null_excetpion_should_be_thrown()
        {
            var field = typeof(ThisFieldObject).GetField("_child1", BindingFlags.Instance | BindingFlags.NonPublic);
            var thisField = new ThisField(field);
            var parent = new ThisFieldObject();

            thisField.Inject(parent, null);
        }

        [TestMethod]
        public void given_field_type_is_found_then_field_should_be_set()
        {
            var field = typeof(ThisFieldObject).GetField("_child1", BindingFlags.Instance | BindingFlags.NonPublic);
            var thisField = new ThisField(field);

            var parent = new ThisFieldObject();
            var child = new Child();
            
            var mapping = new CompositeParts();
            mapping.Add(typeof(Child), child);

            thisField.Inject(parent, mapping);

            Assert.AreSame(parent.Child1, child);
        }


        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void given_field_type_is_not_found_then_a_invalid_operation_exception_should_be_thrown()
        {
            var field = typeof(ThisFieldObject).GetField("_child1", BindingFlags.Instance | BindingFlags.NonPublic);
            var thisField = new ThisField(field);
            var parent = new ThisFieldObject();
            var mapping = new CompositeParts();

            thisField.Inject(parent, mapping);
        }
    }
}