using System;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace Soloco.Composition.UnitTests.Generation.CompositionFactoryBuilderSpecifications
{
    [TestClass]
    public class When_a_composition_is_created_given_an_interface_with_methods_with_parameters_is_used :
        CompositionFactoryBuilderSpecification<IParameterComposition>
    {
        private const int ValueInt = 7;
        
        private readonly List<double> _valueList = new List<double>();
        private readonly Exception _valueException = new NotSupportedException("TestValue");

        private Mock<IParameterPart> _partMock;

        protected override void Arrange()
        {
            base.Arrange();

            _partMock = new Mock<IParameterPart>();

            CompositeParts.Add(typeof(IParameterPart), _partMock.Object);
        }

        [TestMethod]
        public void then_the_constucted_type_should_not_be_null()
        {
            VerifyResultNotNull();
        }

        [TestMethod]
        public void then_the_value_type_parameter_method_should_be_callable()
        {
            Result.TestValueType(ValueInt);
            _partMock.Verify(p => p.TestValueType(ValueInt));
        }

        [TestMethod]
        public void then_the_generic_type_parameter_method_should_be_callable()
        {
            Result.TestGenericType(_valueList);
            _partMock.Verify(p => p.TestGenericType(_valueList));
        }

        [TestMethod]
        public void then_the_object_type_parameter_method_should_be_callable()
        {
            Result.TestReferenceType(_valueException);
            _partMock.Verify(p => p.TestReferenceType(_valueException));
        }

        [TestMethod]
        public void then_the_many_parameter_method_should_be_callable()
        {
            Result.TestMany(ValueInt, ValueInt, ValueInt, ValueInt, ValueInt, ValueInt, ValueInt, ValueInt, ValueInt, ValueInt);
            _partMock.Verify(p => p.TestMany(ValueInt, ValueInt, ValueInt, ValueInt, ValueInt, ValueInt, ValueInt, ValueInt, ValueInt, ValueInt));
        }
    }
}