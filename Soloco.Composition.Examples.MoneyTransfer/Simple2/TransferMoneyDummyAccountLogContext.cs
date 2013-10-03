
namespace Soloco.Composition.Examples.MoneyTransfer.Simple2
{
    /// <summary>
    /// Create a TransferMoney interaction with dummy accounts
    /// </summary>
    public class TransferMoneyDummyAccountLogContext
    {
        private readonly ICompositeBuilderFactory _compositeBuilderFactory;

        public TransferMoneyDummyAccountLogContext(ICompositeBuilderFactory compositeBuilderFactory)
        {
            _compositeBuilderFactory = compositeBuilderFactory;
        }

        public TransferMoney Call()
        {
            var account1 = _compositeBuilderFactory.NewComposite<IAccountLogComposite>();
            account1.Receive(10000);

            var account2 = _compositeBuilderFactory.NewComposite<IAccountLogComposite>();

            return new TransferMoney(account1, account2);
        }
    }
}