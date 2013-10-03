using TC.Unity.Composition.UnitTests.Dependencies;

namespace Soloco.Composition.UnitTests.Generation.CompositionFactoryBuilderSpecifications
{
    public interface IInvalidCompositionPart : ISomePart    , ISomeComposition
    {
        event InvalidCompositionHandler Event;
    }
}