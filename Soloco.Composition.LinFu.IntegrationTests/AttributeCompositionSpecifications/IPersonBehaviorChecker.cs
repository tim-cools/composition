namespace Soloco.Composition.LinFu.IntegrationTests.AttributeCompositionSpecifications
{
    public interface IPersonBehaviorChecker
    {
        void HasDeveloped();
        void HasDevelopedOverride();

        void HasSayed(string message);
        void HasSayedOverride(string message);
    }
}