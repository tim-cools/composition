using System;

namespace Soloco.Composition.UnitTests.Generation.CompositionFactoryBuilderSpecifications
{
    public interface IOutRefParameterPart
    {
        void TestOut(out Exception value);
        void TestRef(ref Exception value);
    }
}