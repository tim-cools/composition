namespace Soloco.Composition.UnitTests.Generation.CompositionFactoryBuilderSpecifications
{
    public interface IOpenGenericIndexerPropertyPart<T>
    {
        Tuple<T> this[Tuple<T> index] { get; set; }
    }
}