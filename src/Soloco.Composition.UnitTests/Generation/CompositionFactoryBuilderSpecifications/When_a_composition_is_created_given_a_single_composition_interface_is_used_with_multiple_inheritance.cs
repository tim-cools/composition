using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace Soloco.Composition.UnitTests.Generation.CompositionFactoryBuilderSpecifications
{
    [TestClass]
    public class When_a_composition_is_created_given_a_single_composition_interface_is_used_with_multiple_inheritance :
        CompositionFactoryBuilderSpecification<ISingleInheritanceComposition>
    {
        private Mock<ISingleInheritanceComposition> _partMock;

        protected override void Arrange()
        {
            base.Arrange();

            _partMock = new Mock<ISingleInheritanceComposition>();

            CompositeParts.Add(typeof(ISingleInheritanceComposition), _partMock.Object);
        }

        [TestMethod]
        public void then_the_constucted_type_should_not_be_null()
        {
            VerifyResultNotNull();
        }

        [TestMethod]
        public void then_the_method_of_the_composition_interface_should_be_callable()
        {
            Result.Test();
            _partMock.Verify(p => p.Test());
        }
    
        [TestMethod]
        public void then_the_method_of_the_composition_base_interface_1_should_be_callable()
        {
            Result.TestPart1();
            _partMock.Verify(p => p.TestPart1());
        }
    
        [TestMethod]
        public void then_the_method_of_the_composition_base_interface_2_should_be_callable()
        {
            Result.TestPart2();
            _partMock.Verify(p => p.TestPart2());
        }
    }
}