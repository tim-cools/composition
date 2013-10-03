namespace Soloco.Composition.Examples.MoneyTransfer.Simple2
{
    public interface IAccountLogComposite : IAccount, TransferMoney.IMoneySource, TransferMoney.IMoneySink
    {
    }
}