using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace Soloco.Composition.UnitTests.Generation.CompositionFactoryBuilderSpecifications
{
    [TestClass]
    public class When_a_composition_is_created_given_an_interface_with_properties_is_used :
        CompositionFactoryBuilderSpecification<IPropertyComposition>
    {
        private const string ReturnValueString = "This is a value";
        private readonly Exception _returnValueException = new InvalidOperationException();

        private Mock<IPropertyPart> _partMock;

        protected override void Arrange()
        {
            base.Arrange();

            _partMock = new Mock<IPropertyPart>();

            CompositeParts.Add(typeof(IPropertyPart), _partMock.Object);

            _partMock
                .SetupGet(m => m.TestString)
                .Returns(ReturnValueString);
            _partMock
                .SetupGet(m => m.TestException)
                .Returns(_returnValueException);

            _partMock
                .SetupGet(m => m.TestGetOnly)
                .Returns(ReturnValueString);
        }

        [TestMethod]
        public void then_the_constucted_type_should_not_be_null()
        {
            VerifyResultNotNull();
        }

        [TestMethod]
        public void then_the_value_type_property_should_return_the_correct_value()
        {
            Assert.AreEqual(ReturnValueString, Result.TestString);
            _partMock.VerifyGet(p => p.TestString);
        }

        [TestMethod]
        public void then_the_object_type_property_should_return_the_correct_value()
        {
            Assert.AreEqual(_returnValueException, Result.TestException);
            _partMock.VerifyGet(p => p.TestException);
        }

        [TestMethod]
        public void then_the_get_only_property_should_return_the_correct_value()
        {
            Assert.AreEqual(ReturnValueString, Result.TestGetOnly);
            _partMock.VerifyGet(p => p.TestGetOnly);
        }

        [TestMethod]
        public void then_the_value_type_property_should_be_settable()
        {
            Result.TestString = ReturnValueString;
            _partMock.VerifySet(p => p.TestString = ReturnValueString);
        }

        [TestMethod]
        public void then_the_object_type_property_should_be_settable()
        {
            Result.TestException = _returnValueException;
            _partMock.VerifySet(p => p.TestException = _returnValueException);
        }

        [TestMethod]
        public void then_the_set_only_property_should_be_settable()
        {
            Result.TestSetOnly = _returnValueException;
            _partMock.VerifySet(p => p.TestSetOnly = _returnValueException);
        }
    }
}