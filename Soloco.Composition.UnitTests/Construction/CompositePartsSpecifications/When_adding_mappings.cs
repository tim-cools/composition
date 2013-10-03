using Microsoft.VisualStudio.TestTools.UnitTesting;

using Soloco.Composition.Construction;

namespace Soloco.Composition.UnitTests.Construction.CompositePartsSpecifications
{
    [TestClass]
    public class When_adding_mappings
    {
        internal interface IParent
        {
        }

        internal interface IChild
        {
        }

        private class Parent : IParent
        {
        }

        private class Child : IChild
        {
        }

        [TestMethod]
        public void then_type_indexer_should_return_added_instances_by_interface_type()
        {
            var parent = new Parent();
            var child = new Child();

            var mapping = new CompositeParts();
            mapping.Add(typeof(IParent), parent);
            mapping.Add(typeof(IChild), child);

            Assert.AreEqual(mapping[typeof(IParent)], parent);
            Assert.AreEqual(mapping[typeof(IChild)], child);
        }

        [TestMethod]
        public void then_type_indexer_should_return_added_instances_by_implementation_type()
        {
            var parent = new Parent();
            var child = new Child();

            var mapping = new CompositeParts();
            mapping.Add(typeof(IParent), parent);
            mapping.Add(typeof(IChild), child);

            Assert.AreEqual(mapping[typeof(Parent)], parent);
            Assert.AreEqual(mapping[typeof(Child)], child);
        }

        [TestMethod]
        public void then_values_array_should_return_added_instances_in_same_order()
        {
            var parent = new Parent();
            var child = new Child();

            var mapping = new CompositeParts();
            mapping.Add(typeof(IParent), parent);
            mapping.Add(typeof(IChild), child);

            Assert.AreEqual(mapping.GetInstances()[0], parent);
            Assert.AreEqual(mapping.GetInstances()[1], child);
        }
    }
}