using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace Soloco.Composition.UnitTests.Generation.CompositionFactoryBuilderSpecifications
{
    [TestClass]
    public class When_a_composition_is_created_given_a_double_nested_composition_interface_is_used:
        CompositionFactoryBuilderSpecification<ParentParentNested.ParentNested.IDoubleNestedSingleComposition>
    {
        private Mock<ParentParentNested.ParentNested.IDoubleNestedPart> _partMock;

        protected override void Arrange()
        {
            base.Arrange();

            _partMock = new Mock<ParentParentNested.ParentNested.IDoubleNestedPart>();

            CompositeParts.Add(typeof(ParentParentNested.ParentNested.IDoubleNestedPart), _partMock.Object);
        }

        [TestMethod]
        public void then_the_constucted_instance_should_not_be_null()
        {
            VerifyResultNotNull();
        }

        [TestMethod]
        public void then_the_method_of_the_composition_interface_should_be_callable()
        {
            Result.Test();
            _partMock.Verify(p => p.Test());
        }
    }
}