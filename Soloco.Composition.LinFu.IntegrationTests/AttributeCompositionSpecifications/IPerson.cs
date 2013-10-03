namespace Soloco.Composition.LinFu.IntegrationTests.AttributeCompositionSpecifications
{
    [Implementation(typeof(Person))]
    public interface IPerson
    {
        string Name { get; set; }
        
        void Say(string message);
    }
}