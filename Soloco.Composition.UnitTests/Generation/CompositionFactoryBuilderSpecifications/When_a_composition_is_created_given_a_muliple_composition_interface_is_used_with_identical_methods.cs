using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace Soloco.Composition.UnitTests.Generation.CompositionFactoryBuilderSpecifications
{
    [TestClass]
    public class When_a_composition_is_created_given_a_muliple_composition_interface_is_used_with_identical_methods :
        CompositionFactoryBuilderSpecification<IMultipleSharedComposition>
    {
        private Mock<IMultipleSharedPart1> _partMock1;
        private Mock<IMultipleSharedPart2> _partMock2;
        private Mock<IMultipleSharedPart3> _partMock3;

        protected override void Arrange()
        {
            base.Arrange();

            _partMock1 = new Mock<IMultipleSharedPart1>();
            _partMock2 = new Mock<IMultipleSharedPart2>();
            _partMock3 = new Mock<IMultipleSharedPart3>();

            CompositeParts.Add(typeof(IMultipleSharedPart1), _partMock1.Object);
            CompositeParts.Add(typeof(IMultipleSharedPart2), _partMock2.Object);
            CompositeParts.Add(typeof(IMultipleSharedPart3), _partMock3.Object);
        }

        [TestMethod]
        public void then_the_constucted_type_should_not_be_null()
        {
            VerifyResultNotNull();
        }

        [TestMethod]
        public void then_the_method_of_the_composition_part1_should_be_callable()
        {
            (Result as IMultipleSharedPart1).Test();
            _partMock1.Verify(p => p.Test());
        }

        [TestMethod]
        public void then_the_method_of_the_composition_part2_should_be_callable()
        {
            (Result as IMultipleSharedPart2).Test();
            _partMock2.Verify(p => p.Test());
        }

        [TestMethod]
        public void then_the_method_of_the_composition_part3_should_be_callable()
        {
            (Result as IMultipleSharedPart3).Test();
            _partMock3.Verify(p => p.Test());
        }
    }
}