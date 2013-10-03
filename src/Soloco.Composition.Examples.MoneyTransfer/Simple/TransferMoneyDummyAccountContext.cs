
using System;

namespace Soloco.Composition.Examples.MoneyTransfer.Simple
{
    /// <summary>
    /// Create a TransferMoney interaction with dummy accounts
    /// </summary>
    public class TransferMoneyDummyAccountContext
    {
        private readonly ICompositeBuilderFactory _compositeBuilderFactory;

        public TransferMoneyDummyAccountContext(ICompositeBuilderFactory compositeBuilderFactory)
        {
            if (compositeBuilderFactory == null) throw new ArgumentNullException("compositeBuilderFactory");

            _compositeBuilderFactory = compositeBuilderFactory;
        }

        public TransferMoney Call()
        {
            var account1 = _compositeBuilderFactory.NewComposite<IAccountComposite>();
            account1.Receive(10000);

            var account2 = _compositeBuilderFactory.NewComposite<IAccountComposite>();

            return new TransferMoney(account1, account2);
        }
    }

}