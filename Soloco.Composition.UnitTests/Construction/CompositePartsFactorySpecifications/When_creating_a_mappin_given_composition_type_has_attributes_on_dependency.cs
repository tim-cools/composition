using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using Soloco.Composition.Construction;
using Soloco.Composition.InstanceResolver;

namespace Soloco.Composition.UnitTests.Construction.CompositePartsFactorySpecifications
{
    [TestClass]
    public class When_creating_a_mappin_given_composition_type_has_attributes_on_dependency
    {
        public interface IComposition : IDependency1, IDependency2, IDependency3
        {
        }

        [Implementation(typeof(Dependency1))]
        public interface IDependency1
        {
        }

        internal class Dependency1 : IDependency1
        {
        }

        [Implementation(typeof(Dependency2))]
        public interface IDependency2
        {
        }

        internal class Dependency2 : IDependency2
        {
        }

        [Implementation(typeof(Dependency3))]
        public interface IDependency3
        {
        }

        internal class Dependency3 : IDependency3
        {
        }

        [TestClass]
        public class given_valid_parameters_are_passed : UnitSpecification
        {    
            private Mock<IInstanceResolver> _resolverMock;
            private CompositeParts _mapping;

            protected override void Act()
            {

                _resolverMock = new Mock<IInstanceResolver>();

                var mappingFactory = new CompositePartsFactory(_resolverMock.Object);
                _resolverMock.Setup(m => m.Resolve(typeof(Dependency1)))
                    .Returns(new Dependency1());
                _resolverMock.Setup(m => m.Resolve(typeof(Dependency2)))
                    .Returns(new Dependency2());
                _resolverMock.Setup(m => m.Resolve(typeof(Dependency3)))
                    .Returns(new Dependency3());

                var partTypes = new Type[3];
                partTypes[0] = typeof (IDependency1);
                partTypes[1] = typeof (IDependency2);
                partTypes[2] = typeof (IDependency3);

                var knownPartTypes = new AttributeKnownPartTypesResolver().Resolve(typeof(IComposition), partTypes);
                _mapping = mappingFactory.Create(typeof(IComposition), partTypes, knownPartTypes, null);   
            }

            [TestMethod]
            public void then_mapping_should_contain_all_resolved_objects()
            {
                Assert.AreEqual(3, _mapping.GetInstances().Length);
                
                Assert.IsNotNull(_mapping[typeof(IDependency1)]);
                Assert.IsNotNull(_mapping[typeof(IDependency2)]);
                Assert.IsNotNull(_mapping[typeof(IDependency3)]);

                Assert.IsInstanceOfType(_mapping[typeof(IDependency1)], typeof(Dependency1));
                Assert.IsInstanceOfType(_mapping[typeof(IDependency2)], typeof(Dependency2));
                Assert.IsInstanceOfType(_mapping[typeof(IDependency3)], typeof(Dependency3));
            }
        }
    }
}