using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace Soloco.Composition.UnitTests.Generation.CompositionFactoryBuilderSpecifications
{
    [TestClass]
    public class When_a_composition_is_created_given_a_composition_interface_is_used_with_closed_parts :
        CompositionFactoryBuilderSpecification<ISingleClosedGenericComposition>
    {
        private Mock<ISingleGenericPart<string>> _partMock;

        protected override void Arrange()
        {
            base.Arrange();

            _partMock = new Mock<ISingleGenericPart<string>>();

            CompositeParts
                .Add(typeof(ISingleGenericPart<string>), _partMock.Object);
        }

        [TestMethod]
        public void then_the_constucted_type_should_not_be_null()
        {
            VerifyResultNotNull();
        }

        [TestMethod]
        public void then_the_method_of_the_composition_interface_should_be_callable()
        {
            Result.TestGenericParameter("bla");
            _partMock.Verify(p => p.TestGenericParameter("bla"));
        }
    }
}