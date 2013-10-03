using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Soloco.Composition.UnitTests.Generation.CompositionFactoryBuilderSpecifications
{
    [TestClass]
    public class When_a_composition_is_created_given_an_interface_with_methods_with_out_ref_parameters_is_used :
        CompositionFactoryBuilderSpecification<IOutRefParameterComposition>
    {
        private readonly Exception _valueExceptionIn = new NotSupportedException("In TestValue");
        private readonly Exception _valueExceptionOut = new NotSupportedException("Out TestValue");

        private OutRefParameterPartMock _partMock;

        protected override void Arrange()
        {
            base.Arrange();

            _partMock = new OutRefParameterPartMock(_valueExceptionOut);

            CompositeParts.Add(typeof (IOutRefParameterPart), _partMock);
        }

        [TestMethod]
        public void then_the_constucted_type_should_not_be_null()
        {
            VerifyResultNotNull();
        }

        [TestMethod]
        public void then_the_out_parameter_method_should_be_callable()
        {
            Exception outValue;
            Result.TestOut(out outValue);

            Assert.AreSame(_valueExceptionOut, outValue);
        }

        [TestMethod]
        public void then_the_ref_parameter_method_should_be_callable()
        {
            Exception value = _valueExceptionIn;
            Result.TestRef(ref value);

            Assert.AreSame(_valueExceptionIn, _partMock.TestRefInValue);
            Assert.AreSame(_valueExceptionOut, value);
        }
    }
}