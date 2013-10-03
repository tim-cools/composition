using System.Collections.Generic;
using System.Linq;

namespace Soloco.Composition.Examples.MoneyTransfer.Simple2
{
    public class AccountLogMixin : IAccount
    {
        private readonly List<int> _transactions = new List<int>();

        public int Balance
        {
            get
            {
                return _transactions.Sum();
            }
        }

        public void Deposit(int amount)
        {
            _transactions.Add(amount);
        }

        public void Withdraw(int amount)
        {
            _transactions.Add(-amount);
        }
    }
}
