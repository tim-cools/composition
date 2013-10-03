namespace Soloco.Composition.Examples.MoneyTransfer.Simple
{
    public interface IAccountComposite : IAccount, TransferMoney.IMoneySource, TransferMoney.IMoneySink
    {
    }
}