using System;

namespace Soloco.Composition.UnitTests.Generation.CompositionFactoryBuilderSpecifications
{
    class GenericEventPartMock : IGenericEventPart
    {
        public event EventHandler<EventArgs> GenericEvent
        {
            add
            {
                GenericEventHandler = value;
                GenericEventAdded = true;
            }
            remove
            {
                GenericEventHandler = value;
                GenericEventRemoved = true;
            }
        }

        public EventHandler<EventArgs> GenericEventHandler { get; private set; }

        public bool GenericEventAdded { get; private set; }
        public bool GenericEventRemoved { get; private set; }

        internal void RaiseGenericEvent()
        {
            if (GenericEventHandler == null)
            {
                throw new InvalidOperationException("SimpleEventHandler == null");
            }
            GenericEventHandler(this, EventArgs.Empty);
        }
    }
}