using System;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace Soloco.Composition.UnitTests.Generation.CompositionFactoryBuilderSpecifications
{
    [TestClass]
    public class When_a_composition_is_created_given_an_interface_with_generic_indexer_properties_is_used :
        CompositionFactoryBuilderSpecification<IGenericIndexerPropertyComposition>
    {
        private const char ValueTypeArgument = 'c';
        private readonly int? _genericTypeArgument = 7;
        private const string ObjectTypeArgument = "Index";
        
        private static readonly List<Exception> _returnValue = new List<Exception> { new Exception(), new NotSupportedException(), new NotImplementedException() };

        private Mock<IGenericIndexerPropertyPart> _partMock;

        protected override void Arrange()
        {
            base.Arrange();

            _partMock = new Mock<IGenericIndexerPropertyPart>();

            CompositeParts.Add(typeof(IGenericPropertyPart), _partMock.Object);

            _partMock
                .SetupGet(m => m[ValueTypeArgument])
                .Returns(_returnValue);

            _partMock
                .SetupGet(m => m[_genericTypeArgument])
                .Returns(_returnValue);

            _partMock
                .SetupGet(m => m[ObjectTypeArgument])
                .Returns(_returnValue);
        }

        [TestMethod]
        public void then_the_constucted_type_should_not_be_null()
        {
            VerifyResultNotNull();
        }

        [TestMethod]
        public void then_the_generic_type_indexer_property_should_return_the_correct_value()
        {
            Assert.AreEqual(_returnValue, Result[7]);
            _partMock.Verify(p => p[7]);
        }

        [TestMethod]
        public void then_the_generic_type_indexer_property_should_be_settable()
        {
            Result[7] = _returnValue;
            _partMock.VerifySet(p => p[7] = _returnValue);
        }

        [TestMethod]
        public void then_the_object_type_indexer_property_should_return_the_correct_value()
        {
            Assert.AreEqual(_returnValue, Result[ObjectTypeArgument]);
            _partMock.Verify(p => p[ObjectTypeArgument]);
        }

        [TestMethod]
        public void then_the_object_type_indexer_property_should_be_settable()
        {
            Result[ObjectTypeArgument] = _returnValue;
            _partMock.VerifySet(p => p[ObjectTypeArgument] = _returnValue);
        }
        [TestMethod]
        public void then_the_value_type_indexer_property_should_return_the_correct_value()
        {
            Assert.AreEqual(_returnValue, Result[ValueTypeArgument]);
            _partMock.Verify(p => p[ValueTypeArgument]);
        }

        [TestMethod]
        public void then_the_value_type_indexer_property_should_be_settable()
        {
            Result[_genericTypeArgument] = _returnValue;
            _partMock.VerifySet(p => p[_genericTypeArgument] = _returnValue);
        }
    }
}