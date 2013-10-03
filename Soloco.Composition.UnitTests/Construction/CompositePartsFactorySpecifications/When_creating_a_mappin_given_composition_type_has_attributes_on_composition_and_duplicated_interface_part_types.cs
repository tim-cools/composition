using Soloco.Composition.Construction;
using Soloco.Composition.Generation;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Soloco.Composition.UnitTests.Construction.CompositePartsFactorySpecifications
{
    [TestClass]
    public class When_creating_a_mappin_given_composition_type_has_attributes_on_composition_and_duplicated_interface_part_types
    {
        [Composite(typeof(Dependency1), typeof(Dependency2))]
        public interface IComposition : IDependency1, IDependency2
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

        internal class Dependency2 : IDependency1
        {
        }

        [TestClass]
        public class given_valid_parameters_are_passed : UnitSpecification
        {    
            protected override void Act()
            {
                var partTypes = new []
                {
                    typeof (IDependency1),
                    typeof (IDependency2),
                };

                ExpectException(() => new AttributeKnownPartTypesResolver().Resolve(typeof (IComposition), partTypes));
            }

            [TestMethod]
            public void then_and_invalid_composition_exception_should_be_thrown()
            {
                Assert.IsInstanceOfType(Exception, typeof(InvalidCompositionException));
                Assert.AreEqual(
                    @"Part interface Soloco.Composition.UnitTests.Construction.CompositePartsFactorySpecifications.When_creating_a_mappin_given_composition_type_has_attributes_on_composition_and_duplicated_interface_part_types+IDependency1 already added.",
                    Exception.Message);
            }
        }
    }
}