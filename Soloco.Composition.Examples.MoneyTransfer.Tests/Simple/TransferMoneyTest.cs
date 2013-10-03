using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Soloco.Composition.Examples.MoneyTransfer.Simple;
using Soloco.Composition.Unity;

namespace Soloco.Composition.Examples.MoneyTransfer.Tests.Simple
{
    [TestClass]
    public class TransferMoneyTest : CompositionTestBase
    {
        public override void Initialize()
        {
            Container.RegisterComposite<IAccountComposite>();
            Container.RegisterType<TransferMoney.IMoneySink, MoneySinkAccountMixin>();
            Container.RegisterType<TransferMoney.IMoneySource, MoneySourceAccountMixin>();
            Container.RegisterType<IAccount, AccountMixin>();

            Container.RegisterType<TransferMoneyDummyAccountContext>();
        }

        [TestMethod]
        public void GivenTransferMoneyWhenExecutedThenMoneyIsTransferred()
        {
            // Create context
            var accountContext = Container.Resolve<TransferMoneyDummyAccountContext>();

            // Execute context to get a money transfer
            var tm = accountContext.Call();

            // Set up the transfer
            tm.Amount = 200;

            // Execute it
            tm.Run();
        }
    }
}