namespace Soloco.Composition.Examples.MoneyTransfer.Advanced
{
    public interface IRepository<T>
    {
        T GetById(string id);
    }
}