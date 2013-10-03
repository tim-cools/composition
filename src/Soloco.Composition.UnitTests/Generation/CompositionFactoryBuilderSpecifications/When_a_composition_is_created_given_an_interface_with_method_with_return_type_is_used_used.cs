using System;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace Soloco.Composition.UnitTests.Generation.CompositionFactoryBuilderSpecifications
{
    [TestClass]
    public class When_a_composition_is_created_given_an_interface_with_method_with_return_type_is_used_used:
        CompositionFactoryBuilderSpecification<IReturnTypeComposition>
    {
        private const int ReturnValue1 = 7;
        
        private readonly List<double> _returnValue2 = new List<double>();
        private readonly Exception _returnValue3 = new NotSupportedException("TestValue");

        private Mock<IReturnTypePart> _partMock;

        protected override void Arrange()
        {
            base.Arrange();

            _partMock = new Mock<IReturnTypePart>();

            CompositeParts.Add(typeof(IReturnTypePart), _partMock.Object);
            _partMock
                .Setup(m => m.Test1())
                .Returns(ReturnValue1);
            _partMock
                .Setup(m => m.Test2())
                .Returns(_returnValue2);
            _partMock
                .Setup(m => m.Test3())
                .Returns(_returnValue3);
        }

        [TestMethod]
        public void then_the_constucted_type_should_not_be_null()
        {
            VerifyResultNotNull();
        }

        [TestMethod]
        public void then_the_return_value_type_method_should_return_the_correct_value()
        {
            Assert.AreEqual(ReturnValue1, Result.Test1());
            _partMock.Verify(p => p.Test1());
        }

        [TestMethod]
        public void then_the_return_generic_type_method_should_return_the_correct_value()
        {
            Assert.AreSame(_returnValue2, Result.Test2());
            _partMock.Verify(p => p.Test2());
        }

        [TestMethod]
        public void then_the_return_object_type_method_should_return_the_correct_value()
        {
            Assert.AreSame(_returnValue3, Result.Test3());
            _partMock.Verify(p => p.Test3());
        }
    }
}