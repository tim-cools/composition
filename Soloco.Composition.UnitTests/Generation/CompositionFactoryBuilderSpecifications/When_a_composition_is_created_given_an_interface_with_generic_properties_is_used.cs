using System;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace Soloco.Composition.UnitTests.Generation.CompositionFactoryBuilderSpecifications
{
    [TestClass]
    public class When_a_composition_is_created_given_an_interface_with_generic_properties_is_used :
        CompositionFactoryBuilderSpecification<IGenericPropertyComposition>
    {
        private static readonly List<Exception> _returnValue = new List<Exception> { new Exception(), new NotSupportedException(), new NotImplementedException()};

        private Mock<IGenericPropertyPart> _partMock;

        protected override void Arrange()
        {
            base.Arrange();

            _partMock = new Mock<IGenericPropertyPart>();

            CompositeParts.Add(typeof(IGenericPropertyPart), _partMock.Object);

            _partMock
                .SetupGet(m => m.TestGenericList)
                .Returns(_returnValue);
        }

        [TestMethod]
        public void then_the_constucted_type_should_not_be_null()
        {
            VerifyResultNotNull();
        }

        [TestMethod]
        public void then_the_value_type_indexer_property_should_return_the_correct_value()
        {
            Assert.AreEqual(_returnValue, Result.TestGenericList);
            _partMock.VerifyGet(p => p.TestGenericList);
        }

        [TestMethod]
        public void then_the_object_type_indexer_property_should_be_settable()
        {
            Result.TestGenericList = _returnValue;
            _partMock.VerifySet(p => p.TestGenericList);
        }
    }
}