using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace Soloco.Composition.UnitTests.Generation.CompositionFactoryBuilderSpecifications
{
    [TestClass]
    public class When_a_composition_is_created_given_a_muliple_composition_interface_is_used :
        CompositionFactoryBuilderSpecification<IMultipleComposition>
    {
        private Mock<IMultiplePart1> _partMock1;
        private Mock<IMultiplePart2> _partMock2;
        private Mock<IMultiplePart3> _partMock3;

        protected override void Arrange()
        {
            base.Arrange();

            _partMock1 = new Mock<IMultiplePart1>();
            _partMock2 = new Mock<IMultiplePart2>();
            _partMock3 = new Mock<IMultiplePart3>();

            CompositeParts.Add(typeof(IMultiplePart1), _partMock1.Object);
            CompositeParts.Add(typeof(IMultiplePart2), _partMock2.Object);
            CompositeParts.Add(typeof(IMultiplePart3), _partMock3.Object);
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
        public void then_the_method_of_the_composition_part3_should_be_callable()
        {
            Result.Test3();
            _partMock3.Verify(p => p.Test3());
        }
    }
}