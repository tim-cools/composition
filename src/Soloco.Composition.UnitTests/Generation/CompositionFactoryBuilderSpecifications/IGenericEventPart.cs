using System;

namespace Soloco.Composition.UnitTests.Generation.CompositionFactoryBuilderSpecifications
{
    public interface IGenericEventPart
    {
        event EventHandler<EventArgs> GenericEvent;
    }
}