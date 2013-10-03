using Soloco.Composition.Construction;

namespace Soloco.Composition.Examples.MoneyTransfer
{
    /// <summary>
    /// Implementation of money sink that maps to an Account
    /// </summary>
    public class MoneySinkAccountMixin : TransferMoney.IMoneySink
    { 
        [This]
        private readonly IAccount _account;

        public void Receive(int amount)
        {
            _account.Deposit(amount);
        }
    }
}