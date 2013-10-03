namespace Soloco.Composition.LinFu.IntegrationTests
{
    public interface IPerson
    {
        string Name { get; set; }
        
        void Say(string message);
    }
}