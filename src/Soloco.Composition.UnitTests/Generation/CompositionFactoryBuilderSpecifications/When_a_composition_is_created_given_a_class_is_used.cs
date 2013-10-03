using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace Soloco.Composition.UnitTests.Generation.CompositionFactoryBuilderSpecifications
{
    [TestClass]
    public class When_a_composition_is_created_given_a_class_is_used :
        CompositionFactoryBuilderSpecification<IClassComposition>
    {
        private Mock<IClassComposition> _partMock;

        protected override void Arrange()
        {
            base.Arrange();

            _partMock = new Mock<IClassComposition>();

            CompositeParts
                .Add(typeof(IClassComposition), _partMock.Object );
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
            Assert.AreEqual("Composite type 'Soloco.Composition.UnitTests.Generation.CompositionFactoryBuilderSpecifications.IClassComposition' should be an interface type.", Exception.Message);
        }
    }
}