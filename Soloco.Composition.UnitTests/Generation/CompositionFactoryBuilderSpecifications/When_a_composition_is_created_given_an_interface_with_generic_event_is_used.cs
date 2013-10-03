using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Soloco.Composition.UnitTests.Generation.CompositionFactoryBuilderSpecifications
{
    [TestClass]
    public class When_a_composition_is_created_given_an_interface_with_generic_event_is_used :
        CompositionFactoryBuilderSpecification<IGenericEventComposition>
    {
        private GenericEventPartMock _partMock;
        private bool _genericEventRaised;

        protected override void Arrange()
        {
            base.Arrange();

            _partMock = new GenericEventPartMock();

            CompositeParts.Add(typeof(IGenericEventPart), _partMock);
        }

        [TestMethod]
        public void then_the_constucted_type_should_not_be_null()
        {
            VerifyResultNotNull();
        }

        [TestMethod]
        public void then_the_generic_event_should_subscribable()
        {
            EventHandler<EventArgs> handler = Result_GenericEvent;
            Result.GenericEvent += handler;

            Assert.AreSame(handler, _partMock.GenericEventHandler);
            Assert.IsTrue(_partMock.GenericEventAdded);
        }

        [TestMethod]
        public void then_the_generic_event_should_unsubscribable()
        {
            EventHandler<EventArgs> handler = Result_GenericEvent;
            Result.GenericEvent -= handler;

            Assert.AreSame(handler, _partMock.GenericEventHandler);
            Assert.IsTrue(_partMock.GenericEventRemoved);
        }

        [TestMethod]
        public void then_subscriber_of_the_generic_event_should_called()
        {
            EventHandler<EventArgs> handler = Result_GenericEvent;
            Result.GenericEvent += handler;
            _partMock.RaiseGenericEvent();

            Assert.IsTrue(_genericEventRaised);
        }

        void Result_GenericEvent(object sender, EventArgs e)
        {
            _genericEventRaised = true;
        }
    }
}