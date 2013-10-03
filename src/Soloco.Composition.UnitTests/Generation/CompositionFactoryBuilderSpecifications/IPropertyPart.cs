using System;

namespace Soloco.Composition.UnitTests.Generation.CompositionFactoryBuilderSpecifications
{
    public interface IPropertyPart
    {
        string TestString { get; set; }
        Exception TestException { get; set; }

        string TestGetOnly { get; }
        Exception TestSetOnly { set; }
    }
}