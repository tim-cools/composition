using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Soloco.Composition.Generation;

namespace Soloco.Composition.UnitTests.Generation.CompositionPartInterfacesResolverSpecifications
{
    public class When_resolving_composition_interfaces
    {
        [TestClass]
        public class given_single_flat_interface_is_used
        {
            public interface IPart
            {
            }
            public interface IComposition : IPart
            {
            }

            [TestMethod]
            public void then_this_interface_should_be_returned()
            {
                var resolver = new CompositionPartInterfacesResolver();
                var result = resolver.Resolve(typeof(IComposition));

                Assert.AreEqual(1, result.Count);
                Assert.AreEqual(typeof(IPart), result[0]);
            }
        }

        [TestClass]
        public class given_multiple_flat_interfaces_are_used
        {
            public interface IPart1
            {
            }
            public interface IPart2
            {
            }
            public interface IComposition : IPart1, IPart2
            {
            }
            [TestMethod]
            public void then_these_interfaces_should_be_returned()
            {
                var resolver = new CompositionPartInterfacesResolver();
                var result = resolver.Resolve(typeof(IComposition));

                Assert.AreEqual(2, result.Count);
                Assert.AreEqual(1, result.Count(i => i == typeof(IPart1)));
                Assert.AreEqual(1, result.Count(i => i == typeof(IPart2)));
            }
        }

        [TestClass]
        public class given_single_flat_interface_is_used_with_base_interface
        {
            public interface IBasePart
            {
            }
            public interface IPart : IBasePart
            {
            }
            public interface IComposition : IPart
            {
            }

            [TestMethod]
            public void then_this_first_level_interface_should_be_returned()
            {
                var resolver = new CompositionPartInterfacesResolver();
                var result = resolver.Resolve(typeof(IComposition));

                Assert.AreEqual(1, result.Count);
                Assert.AreEqual(typeof(IPart), result[0]);
            }
        }

        [TestClass]
        public class given_multiple_flat_interfaces_are_used_with_base_interfaces
        {
            public interface IBasePart1
            {
            }
            public interface IBasePart2
            {
            }
            public interface IPart1 : IBasePart1
            {
            }
            public interface IPart2 : IBasePart2
            {
            }
            public interface IComposition : IPart1, IPart2
            {
            }
            [TestMethod]
            public void then_these_first_level_interfaces_should_be_returned()
            {
                var resolver = new CompositionPartInterfacesResolver();
                var result = resolver.Resolve(typeof(IComposition));

                Assert.AreEqual(2, result.Count);
                Assert.AreEqual(1, result.Count(i => i == typeof(IPart1)));
                Assert.AreEqual(1, result.Count(i => i == typeof(IPart2)));
            }
        }

        [TestClass]
        public class given_multiple_flat_interfaces_are_used_with_shared_base_interfaces : UnitSpecification
        {
            public interface IBasePart
            {
            }
            public interface IPart1 : IBasePart
            {
            }
            public interface IPart2 : IBasePart
            {
            }
            public interface IComposition : IPart1, IPart2
            {
            }

            protected override void Act()
            {
                ExpectException(delegate
                {
                    var resolver = new CompositionPartInterfacesResolver();
                    resolver.Resolve(typeof(IComposition));
                });
            }

            [TestMethod]
            public void then_an_invalid_composition_exception_should_be_thrown()
            {
                Assert.IsInstanceOfType(Exception, typeof(InvalidCompositionException));
            }

            [TestMethod]
            public void then_the_exception_message_should_contain_shared_base_class()
            {
                Assert.IsNotNull(Exception.Message);
                Assert.AreEqual(Exception.Message, "Composition base type 'Soloco.Composition.UnitTests.Generation.CompositionPartInterfacesResolverSpecifications.When_resolving_composition_interfaces+given_multiple_flat_interfaces_are_used_with_shared_base_interfaces+IBasePart' is shared by multiple composition parts: 'Soloco.Composition.UnitTests.Generation.CompositionPartInterfacesResolverSpecifications.When_resolving_composition_interfaces+given_multiple_flat_interfaces_are_used_with_shared_base_interfaces+IPart1, Soloco.Composition.UnitTests.Generation.CompositionPartInterfacesResolverSpecifications.When_resolving_composition_interfaces+given_multiple_flat_interfaces_are_used_with_shared_base_interfaces+IPart2'.");
            }
        }
    }
}