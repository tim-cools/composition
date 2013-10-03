
using System;
using Soloco.Composition.Construction;

namespace Soloco.Composition.Examples.MoneyTransfer
{
    /// <summary>
    /// Implementation of MoneySource that maps to an Account
    /// </summary>
    public class MoneySourceAccountMixin : TransferMoney.IMoneySource
    {
        [This]
        private readonly IAccount _account;

        public void TransferTo(int amount, TransferMoney.IMoneySink sink)
        {
            if (sink == null) throw new ArgumentNullException("sink");

            if (_account.Balance < amount)
                throw new ArgumentException("Balance too low to allow withdraw");

            _account.Withdraw(amount);
            sink.Receive(amount);
        }
    }
}