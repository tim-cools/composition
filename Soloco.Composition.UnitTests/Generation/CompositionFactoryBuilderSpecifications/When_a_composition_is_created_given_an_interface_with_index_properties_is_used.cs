using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace Soloco.Composition.UnitTests.Generation.CompositionFactoryBuilderSpecifications
{
    [TestClass]
    public class When_a_composition_is_created_given_an_interface_with_index_properties_is_used :
        CompositionFactoryBuilderSpecification<IIndexerPropertyComposition>
    {
        private const string ReturnValueString = "This is a value";
        private const string IndexerString = "7";
        private readonly Exception _returnValueException = new InvalidOperationException();
        private readonly Exception _indexerException = new NotSupportedException();

        private Mock<IIndexerPropertyPart> _partMock;

        protected override void Arrange()
        {
            base.Arrange();

            _partMock = new Mock<IIndexerPropertyPart>();

            CompositeParts.Add(typeof(IIndexerPropertyPart), _partMock.Object);

            _partMock
                .SetupGet(m => m[_indexerException])
                .Returns(ReturnValueString);
            _partMock
                .SetupGet(m => m[IndexerString])
                .Returns(_returnValueException);
        }

        [TestMethod]
        public void then_the_constucted_type_should_not_be_null()
        {
            VerifyResultNotNull();
        }

        [TestMethod]
        public void then_the_value_type_indexer_property_should_return_the_correct_value()
        {
            Assert.AreEqual(_returnValueException, Result[IndexerString]);
            _partMock.Verify(p => p[IndexerString]);
        }

        [TestMethod]
        public void then_the_object_type_indexer_property_should_return_the_correct_value()
        {
            Assert.AreEqual(ReturnValueString, Result[_indexerException]);
            _partMock.Verify(p => p[_indexerException]);
        }


        [TestMethod]
        public void then_the_value_type_indexer_property_should_be_settable()
        {
            Result[IndexerString] = _returnValueException;
            _partMock.VerifySet(p => p[IndexerString] = _returnValueException);
        }

        [TestMethod]
        public void then_the_object_type_indexer_property_should_be_settable()
        {
            Result[_indexerException] = ReturnValueString;
            _partMock.VerifySet(p => p[_indexerException] = ReturnValueString); 
        }
    }
}