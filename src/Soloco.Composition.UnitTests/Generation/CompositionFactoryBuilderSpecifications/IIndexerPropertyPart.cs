using System;

namespace Soloco.Composition.UnitTests.Generation.CompositionFactoryBuilderSpecifications
{
    public interface IIndexerPropertyPart
    {
        string this[Exception index] { get; set; }
        Exception this[string name] { get; set; }
    }
}