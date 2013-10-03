namespace Soloco.Composition.Unity.IntegrationTests
{
    public interface IPerson
    {
        string Name { get; set; }
        
        void Say(string message);
    }
}