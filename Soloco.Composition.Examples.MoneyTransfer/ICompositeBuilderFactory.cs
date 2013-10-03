namespace Soloco.Composition.Examples.MoneyTransfer
{
    public interface ICompositeBuilderFactory
    {
        T NewComposite<T>();
    }
}