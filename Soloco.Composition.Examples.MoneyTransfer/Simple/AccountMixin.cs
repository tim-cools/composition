namespace Soloco.Composition.Examples.MoneyTransfer.Simple
{
    public class AccountMixin : IAccount
    {
        public int Balance { get; private set; }

        public void Deposit(int amount)
        {
            Balance += amount;
        }

        public void Withdraw(int amount)
        {
            Balance -= amount;
        }
    }
}