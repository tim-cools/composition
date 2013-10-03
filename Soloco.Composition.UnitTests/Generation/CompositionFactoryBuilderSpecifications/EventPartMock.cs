using System;

namespace Soloco.Composition.UnitTests.Generation.CompositionFactoryBuilderSpecifications
{
    class EventPartMock : IEventPart
    {
        public event EventHandler SimpleEvent
        {
            add
            {
                SimpleEventHandler = value;
                SimpleEventAdded = true;
            }
            remove
            {
                SimpleEventHandler = value;
                SimpleEventRemoved = true;
            }
        }
        
        public EventHandler SimpleEventHandler { get; private set; }

        public bool SimpleEventAdded { get; private set; }
        public bool SimpleEventRemoved { get; private set; }

        internal void RaiseSimpleEvent()
        {
            if (SimpleEventHandler == null)
            {
                throw new InvalidOperationException("SimpleEventHandler == null");
            }
            SimpleEventHandler(this, EventArgs.Empty);
        }
    }
}