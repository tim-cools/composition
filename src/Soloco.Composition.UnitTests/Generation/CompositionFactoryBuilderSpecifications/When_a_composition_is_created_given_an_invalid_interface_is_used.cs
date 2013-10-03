using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace Soloco.Composition.UnitTests.Generation.CompositionFactoryBuilderSpecifications
{
    [TestClass]
    [Ignore]     //I currently don't know how to break the compilation
    public class When_a_composition_is_created_given_an_invalid_interface_is_used :
        CompositionFactoryBuilderSpecification<IInvalidComposition>
    {
        private Mock<IInvalidCompositionPart> _partMock;

        protected override void Arrange()
        {
            base.Arrange();

            _partMock = new Mock<IInvalidCompositionPart>();

            CompositeParts.Add(typeof(IInvalidCompositionPart), _partMock.Object);
        }

        protected override void Act()
        {
            ExpectException(DoAct);
        }

        private void DoAct()
        {
            base.Act();
        } 
        [TestMethod]
        public void then_an_InvalidCompositionException_should_be_thrown()
        {
            Assert.IsInstanceOfType(Exception, typeof(InvalidOperationException));
            Assert.AreEqual(Exception.Message, "Error occured while compiling composed type 'TC.Unity.Composition.Tests.Generation.CompositionFactoryBuilderSpecifications.IInvalidComposition'(14, 18) [CS0535] 'TC.Unity.Composition.Tests.Generation.CompositionFactoryBuilderSpecifications.InvalidComposition' does not implement interface member 'TC.Unity.Composition.Tests.Generation.CompositionFactoryBuilderSpecifications.IInvalidCompositionPart.Event'\r\n");
        }
    }
}