using System;
using System.Collections.Generic;

namespace Soloco.Composition.UnitTests.Generation.CompositionFactoryBuilderSpecifications
{
    public interface IGenericIndexerPropertyPart
    {
        List<Exception> this[char index] { get; set; }
        List<Exception> this[int? index] { get; set; }
        List<Exception> this[string index] { get; set; }
    }
}