namespace Soloco.Composition.Container.IntegrationTests
{
    public interface IPersonBehaviorChecker
    {
        void HasDanced();
        void HasDeveloped();

        void HasSayed(string message);
    }
}