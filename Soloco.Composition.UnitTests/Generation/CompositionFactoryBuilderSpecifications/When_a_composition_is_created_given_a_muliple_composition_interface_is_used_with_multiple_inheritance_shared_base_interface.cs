using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using Soloco.Composition.Generation;

namespace Soloco.Composition.UnitTests.Generation.CompositionFactoryBuilderSpecifications
{
    [TestClass]
    public class When_a_composition_is_created_given_a_muliple_composition_interface_is_used_with_multiple_inheritance_shared_base_interface :
        CompositionFactoryBuilderSpecification<IMultipleSharedInheritanceComposition>
    {
        private Mock<IMultipleSharedInheritancePart1> _partMock1;
        private Mock<IMultipleSharedInheritancePart2> _partMock2;

        protected override void Arrange()
        {
            base.Arrange();

            _partMock1 = new Mock<IMultipleSharedInheritancePart1>();
            _partMock2 = new Mock<IMultipleSharedInheritancePart2>();

            CompositeParts.Add(typeof (IMultipleSharedInheritancePart1), _partMock1.Object);
            CompositeParts.Add(typeof(IMultipleSharedInheritancePart2), _partMock2.Object);
        }

        protected override void Act()
        {
            ExpectException(DoAct);
        }

        private void DoAct()
        {
            base.Act();
        }
        [TestMethod]
        public void then_an_invalid_operation_exception_should_be_thrown()
        {
            Assert.IsInstanceOfType(Exception, typeof(InvalidCompositionException));
        }
    }
}