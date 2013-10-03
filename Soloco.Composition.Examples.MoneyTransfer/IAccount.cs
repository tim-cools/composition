using Soloco.Composition.Examples.MoneyTransfer.Simple2;

namespace Soloco.Composition.Examples.MoneyTransfer
{
    /// <summary>
    /// Account domain interface. This is implemented by a private mixin, and is therefore
    /// only accessible by the role implementations.
    /// </summary>
    [Implementation(typeof(AccountLogMixin))]
    public interface IAccount
    {
        int Balance { get; }
        void Deposit(int amount);
        void Withdraw(int amount);
    }
}