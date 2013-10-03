using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Soloco.Composition.UnitTests.Generation.CompositionFactoryBuilderSpecifications
{
    [TestClass]
    public class When_a_composition_is_created_given_an_interface_with_events_is_used :
        CompositionFactoryBuilderSpecification<IEventComposition>
    {
        private EventPartMock _partMock;
        private bool _simpleEventRaised;

        protected override void Arrange()
        {
            base.Arrange();

            _partMock = new EventPartMock();

            CompositeParts.Add(typeof (IEventPart), _partMock );
        }

        [TestMethod]
        public void then_the_constucted_type_should_not_be_null()
        {
            VerifyResultNotNull();
        }

        [TestMethod]
        public void then_the_simple_event_should_subscribable()
        {
            EventHandler handler = Result_SimpleEvent;
            Result.SimpleEvent += handler;

            Assert.AreSame(handler, _partMock.SimpleEventHandler);
            Assert.IsTrue(_partMock.SimpleEventAdded);
        }

        [TestMethod]
        public void then_the_simple_event_should_unsubscribable()
        {
            EventHandler handler = Result_SimpleEvent;
            Result.SimpleEvent -= handler;

            Assert.AreSame(handler, _partMock.SimpleEventHandler);
            Assert.IsTrue(_partMock.SimpleEventRemoved);
        }

        [TestMethod]
        public void then_subscriber_of_the_simple_event_should_called()
        {
            EventHandler handler = Result_SimpleEvent;
            Result.SimpleEvent += handler;
            _partMock.RaiseSimpleEvent();

            Assert.IsTrue(_simpleEventRaised);
        }

        void Result_SimpleEvent(object sender, EventArgs e)
        {
            _simpleEventRaised = true;
        }
    }
}