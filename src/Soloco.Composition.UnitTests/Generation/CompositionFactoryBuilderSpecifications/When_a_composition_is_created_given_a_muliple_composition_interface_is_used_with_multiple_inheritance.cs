using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace Soloco.Composition.UnitTests.Generation.CompositionFactoryBuilderSpecifications
{
    [TestClass]
    public class When_a_composition_is_created_given_a_muliple_composition_interface_is_used_with_multiple_inheritance :
        CompositionFactoryBuilderSpecification<IMultipleInheritanceComposition>
    {
        private Mock<IMultipleInheritancePart1> _partMock1;
        private Mock<IMultipleInheritancePart2> _partMock2;

        protected override void Arrange()
        {
            base.Arrange();

            _partMock1 = new Mock<IMultipleInheritancePart1>();
            _partMock2 = new Mock<IMultipleInheritancePart2>();

            CompositeParts.Add(typeof(IMultipleInheritancePart1), _partMock1.Object);
            CompositeParts.Add(typeof(IMultipleInheritancePart2), _partMock2.Object);
        }

        [TestMethod]
        public void then_the_constucted_type_should_not_be_null()
        {
            VerifyResultNotNull();
        }

        [TestMethod]
        public void then_the_method_of_the_composition_part1_should_be_callable()
        {
            Result.Test1();
            _partMock1.Verify(p => p.Test1());
        }

        [TestMethod]
        public void then_the_method_of_the_composition_part2_should_be_callable()
        {
            Result.Test2();
            _partMock2.Verify(p => p.Test2());
        }

        [TestMethod]
        public void then_the_method_of_the_base_composition_part1_should_be_callable()
        {
            Result.Test1Base();
            _partMock1.Verify(p => p.Test1Base());
        }

        [TestMethod]
        public void then_the_method_of_the_base_composition_part2_should_be_callable()
        {
            Result.Test2Base();
            _partMock2.Verify(p => p.Test2Base());
        }
    }
}