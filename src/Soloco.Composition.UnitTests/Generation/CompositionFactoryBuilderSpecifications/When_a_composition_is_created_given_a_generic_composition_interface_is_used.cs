using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace Soloco.Composition.UnitTests.Generation.CompositionFactoryBuilderSpecifications
{
    [TestClass]
    public class When_a_composition_is_created_given_a_generic_composition_interface_is_used :
        CompositionFactoryBuilderSpecification<IGenericComposition<string>>
    {
        private const string ReturnValue = "This is a random value";
        private const string ParamaterValue = "This is a random value";

        private const int OtherReturnValue = 11;
        private const int OtherParameterValue = 9;

        private Mock<ISingleGenericPart<string>> _partMock;

        protected override void Arrange()
        {
            base.Arrange();

            _partMock = new Mock<ISingleGenericPart<string>>();
            _partMock
                .Setup(m => m.TestGenericReturnValue())
                .Returns(ReturnValue);
            _partMock
                .Setup(m => m.TestGenericReturnValueAndParameter(ParamaterValue))
                .Returns(ReturnValue);
            _partMock
                .Setup(m => m.TestGenericReturnValueAndParameterAndAnotherGenericParameter<int>(ParamaterValue, OtherParameterValue))
                .Returns(ReturnValue);
            _partMock
                .Setup(m => m.TestGenericReturnValueAndParameterAndAnotherGenericReturnValue<int>(ParamaterValue))
                .Returns(OtherReturnValue);

            CompositeParts.Add(typeof(ISingleGenericPart<string>), _partMock.Object);
        }

        [TestMethod]
        public void then_the_constucted_type_should_not_be_null()
        {
            VerifyResultNotNull();
        }

        [TestMethod]
        public void then_the_method_with_the_generic_parameter_of_the_composition_interface_should_be_callable()
        {
            Result.TestGenericParameter(ParamaterValue);
            _partMock.Verify(p => p.TestGenericParameter(ParamaterValue));
        }

        [TestMethod]
        public void then_the_method_with_the_generic_return_value_of_the_composition_interface_should_be_callable()
        {
            Assert.AreEqual(ReturnValue, Result.TestGenericReturnValue());
            _partMock.Verify(p => p.TestGenericReturnValue());
        }

        [TestMethod]
        public void then_the_method_with_the_generic_parameter_and_return_value_of_the_composition_interface_should_be_callable()
        {
            Assert.AreEqual(ReturnValue, Result.TestGenericReturnValueAndParameter(ParamaterValue));
            _partMock.Verify(p => p.TestGenericReturnValueAndParameter(ParamaterValue));
        }


        [TestMethod]
        public void then_the_method_with_the_generic_parameter_and_return_value_and_another_generic_parameter_of_the_composition_interface_should_be_callable()
        {
            Assert.AreEqual(ReturnValue, Result.TestGenericReturnValueAndParameterAndAnotherGenericParameter<int>(ParamaterValue, OtherParameterValue));
            _partMock.Verify(p => p.TestGenericReturnValueAndParameterAndAnotherGenericParameter(ParamaterValue, OtherParameterValue));
        }


        [TestMethod]
        public void then_the_method_with_the_generic_parameter_and_return_value_and_another_generic_return_of_the_composition_interface_should_be_callable()
        {
            Assert.AreEqual(OtherReturnValue, Result.TestGenericReturnValueAndParameterAndAnotherGenericReturnValue<int>(ParamaterValue));
            _partMock.Verify(p => p.TestGenericReturnValueAndParameterAndAnotherGenericReturnValue<int>(ParamaterValue));
        }
    }
}