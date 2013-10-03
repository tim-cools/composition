using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace Soloco.Composition.UnitTests.Generation.CompositionFactoryBuilderSpecifications
{
    [TestClass]
    public class When_a_composition_is_created_given_a_single_composition_interface_is_used :
        CompositionFactoryBuilderSpecification<ISingleComposition>
    {
        private Mock<ISinglePart> _partMock;

        protected override void Arrange()
        {
            base.Arrange();

            _partMock = new Mock<ISinglePart>();

            CompositeParts.Add(typeof(ISinglePart), _partMock.Object);
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
    }
}