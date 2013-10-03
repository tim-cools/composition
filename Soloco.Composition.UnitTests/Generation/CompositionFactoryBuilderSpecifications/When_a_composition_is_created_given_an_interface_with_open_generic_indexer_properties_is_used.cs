using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace Soloco.Composition.UnitTests.Generation.CompositionFactoryBuilderSpecifications
{
    [TestClass]
    public class When_a_composition_is_created_given_an_interface_with_open_generic_indexer_properties_is_used :
        CompositionFactoryBuilderSpecification<IOpenGenericIndexerPropertyComposite<string>>
    {
        private readonly Tuple<string> _genericTypeArgument = new Tuple<string>("string1", "string2");
        private readonly Tuple<string> _genericTypeReturnValue = new Tuple<string>("return1", "return2");
        
        private Mock<IOpenGenericIndexerPropertyPart<string>> _partMock;

        protected override void Arrange()
        {
            base.Arrange();

            _partMock = new  Mock<IOpenGenericIndexerPropertyPart<string>>();

            CompositeParts.Add(typeof(IGenericPropertyPart), _partMock.Object);

            _partMock
                .SetupGet(m => m[_genericTypeArgument])
                .Returns(_genericTypeReturnValue);
        }

        [TestMethod]
        public void then_the_constucted_type_should_not_be_null()
        {
            VerifyResultNotNull();
        }

        [TestMethod]
        public void then_the_generic_type_indexer_property_should_return_the_correct_value()
        {
            Assert.AreEqual(_genericTypeReturnValue, Result[_genericTypeArgument]);
            _partMock.Verify(p => p[_genericTypeArgument]);
        }

        [TestMethod]
        public void then_the_generic_type_indexer_property_should_be_settable()
        {
            Result[_genericTypeArgument] = _genericTypeReturnValue;
            _partMock.VerifySet(p => p[_genericTypeArgument] = _genericTypeReturnValue);
        }
    }
}