namespace Soloco.Composition.Unity.IntegrationTests
{
    public interface IPersonBehaviorChecker
    {
        void HasDanced();
        void HasDeveloped();

        void HasSayed(string message);
    }
}