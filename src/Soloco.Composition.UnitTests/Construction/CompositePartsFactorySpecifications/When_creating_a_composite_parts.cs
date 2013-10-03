using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using Soloco.Composition.Construction;
using Soloco.Composition.InstanceResolver;

namespace Soloco.Composition.UnitTests.Construction.CompositePartsFactorySpecifications
{
    [TestClass]
    public class When_creating_a_composite_parts
    {
        public interface IComposition : IDependency1, IDependency2, IDependency3
        {
        }

        public interface IDependency1
        {
        }

        internal class Dependency1 : IDependency1
        {
        }

        public interface IDependency2
        {
        }

        internal class Dependency2 : IDependency2
        {
        }

        public interface IDependency3
        {
        }

        internal class Dependency3 : IDependency3
        {
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void given_null_is_passed_as_part_types_then_a_argument_null_exception_should_be_thrown()
        {
            var resolverMock = new Mock<IInstanceResolver>();
            var mappingFactory = new CompositePartsFactory(resolverMock.Object);

            mappingFactory.Create(typeof(IComposition), null, new KnownPartTypes(), null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void given_null_is_passed_as_composition_type_then_a_argument_null_exception_should_be_thrown()
        {
            var resolverMock = new Mock<IInstanceResolver>();
            var mappingFactory = new CompositePartsFactory(resolverMock.Object);

            mappingFactory.Create(null, new Type[] {}, new KnownPartTypes(), null);
        }

        [TestClass]
        public class given_valid_parameters_are_passed : UnitSpecification
        {    
            private Mock<IInstanceResolver> _resolverMock;
            private CompositeParts _mapping;
            private IDependency1 _dependency1;
            private IDependency2 _dependency2;
            private IDependency3 _dependency3;

            protected override void Act()
            {
                _dependency1 = new Dependency1();
                _dependency2 = new Dependency2();
                _dependency3 = new Dependency3();

                _resolverMock = new Mock<IInstanceResolver>();
                _resolverMock.Setup(m => m.Resolve(typeof(IDependency1))).Returns(_dependency1);
                _resolverMock.Setup(m => m.Resolve(typeof(IDependency2))).Returns(_dependency2);
                _resolverMock.Setup(m => m.Resolve(typeof(IDependency3))).Returns(_dependency3);

                var compositePartsFactory = new CompositePartsFactory(_resolverMock.Object);

                var partTypes = new Type[3];
                partTypes[0] = typeof (IDependency1);
                partTypes[1] = typeof (IDependency2);
                partTypes[2] = typeof (IDependency3);

                _mapping = compositePartsFactory.Create(typeof(IComposition), partTypes, new KnownPartTypes(), null);
            }

            [TestMethod]
            public void then_parameter_types_should_be_requested_at_container()
            {
                _resolverMock.Verify(m => m.Resolve(typeof(IDependency1)));
                _resolverMock.Verify(m => m.Resolve(typeof(IDependency2)));
                _resolverMock.Verify(m => m.Resolve(typeof(IDependency3)));
            }

            [TestMethod]
            public void then_mapping_should_containe_all_resolved_objects()
            {
                var instances = _mapping.GetInstances();
                Assert.AreEqual(3, instances.Length);
                Assert.AreEqual(_dependency1, _mapping[typeof(IDependency1)]);
                Assert.AreEqual(_dependency2, _mapping[typeof(IDependency2)]);
                Assert.AreEqual(_dependency3, _mapping[typeof(IDependency3)]);
            }
        }
    }
}