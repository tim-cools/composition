using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Soloco.Composition.Examples.MoneyTransfer.Simple2;
using Soloco.Composition.Unity;

namespace Soloco.Composition.Examples.MoneyTransfer.Tests.Simple2
{
    [TestClass]
    public class TransferMoneyTest : CompositionTestBase
    {
        public override void Initialize()
        {
            Container.RegisterComposite<IAccountLogComposite>();
            Container.RegisterType<TransferMoneyDummyAccountLogContext>();
        }

        [TestMethod]
        public void GivenTransferMoneyWhenExecutedThenMoneyIsTransferred()
        {
            // Create context
            var accountLogContext = Container.Resolve<TransferMoneyDummyAccountLogContext>();

            // Execute context to get a money transfer
            var tm = accountLogContext.Call();

            // Set up the transfer
            tm.Amount = 200;

            // Execute it
            tm.Run();
        }
    }
}