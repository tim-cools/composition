namespace Soloco.Composition.Container.IntegrationTests
{
    public interface IPerson
    {
        string Name { get; set; }
        
        void Say(string message);
    }
}