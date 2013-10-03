namespace Soloco.Composition.Examples.MoneyTransfer.Advanced
{
    /// <summary>
    /// Account implemented as a persistent Entity
    /// </summary>
    //@Mixins({MoneySinkAccountMixin.class, MoneySourceAccountMixin.class, AccountEntity.AccountMixin.class})
    public interface IAccountEntity : IAccount, TransferMoney.IMoneySource, TransferMoney.IMoneySink
    {
    }
}
