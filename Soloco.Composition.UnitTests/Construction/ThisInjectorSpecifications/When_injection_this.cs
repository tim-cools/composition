using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Soloco.Composition.Construction;

namespace Soloco.Composition.UnitTests.Construction.ThisInjectorSpecifications
{
    [TestClass]
    public class When_injection_this
    {
        internal interface IParent
        {
        }

        internal interface IChild
        {
        }

        private class Parent : IParent
        {
            [This]
            private IChild _child;

            public IChild Child
            {
                get { return _child; }
            }
        }

        private class Child : IChild
        {
        }

        [TestMethod]
        public void given_a_this_attribute_is_found_with_a_corresponding_instance_then_this_instance_his_this_field_should_be_assigned()
        {
            var injector = new ThisInjector();
            var parent = new Parent();
            var child = new Child();

            var mappedInstances = new CompositeParts();
            mappedInstances.Add(typeof (IParent), parent);
            mappedInstances.Add(typeof (IChild), child);

            injector.Inject(mappedInstances);

            Assert.AreSame(child, parent.Child);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void given_a_this_attribute_is_found_with_no_corresponding_instance_then_this_an_invalid_operation_exception_should_be_thrown()
        {
            var injector = new ThisInjector();
            var parent = new Parent();

            var mappedInstances = new CompositeParts();
            mappedInstances.Add(typeof(IParent), parent);

            injector.Inject(mappedInstances);
        }
    }
}