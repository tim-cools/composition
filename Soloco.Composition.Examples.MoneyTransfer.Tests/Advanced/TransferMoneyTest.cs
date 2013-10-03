
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Soloco.Composition.Examples.MoneyTransfer.Advanced;
using Soloco.Composition.Unity;

namespace Soloco.Composition.Examples.MoneyTransfer.Tests.Advanced
{
    [TestClass]
    public class TransferMoneyTest : CompositionTestBase
    {
        private const string SourceId = "ABC";
        private const string SinkId = "XYZ";

        private IAccountEntity _account1;
        private IAccountEntity _account2;
        private readonly Mock<IRepository<TransferMoney.IMoneySource>> _moneySourceRepositoryMock = new Mock<IRepository<TransferMoney.IMoneySource>>();
        private readonly Mock<IRepository<TransferMoney.IMoneySink>> _moneySinkRepositoryMock = new Mock<IRepository<TransferMoney.IMoneySink>>();

        public override void Initialize()
        {
            InitializeContainer();
            InitializeAccounts();
        }

        private void InitializeContainer()
        {
            Container.RegisterComposite<IAccountEntity>();
            Container.RegisterType<TransferMoney.IMoneySink, MoneySinkAccountMixin>();
            Container.RegisterType<TransferMoney.IMoneySource, MoneySourceAccountMixin>();
            Container.RegisterType<IAccount, AccountMixin>();

            Container.RegisterType<TransferMoneyFindAccountContext>();
            Container.RegisterInstance(_moneySourceRepositoryMock.Object);
            Container.RegisterInstance(_moneySinkRepositoryMock.Object);
        }

        private void InitializeAccounts()
        {
            _account1 = Container.Compose<IAccountEntity>();
            _account1.Receive(10000);

            _moneySourceRepositoryMock
                .Setup(r => r.GetById(SourceId))
                .Returns(_account1);

            _account2 = Container.Compose<IAccountEntity>();
            _moneySinkRepositoryMock
                .Setup(r => r.GetById(SinkId))
                .Returns(_account2);
        }

        [TestMethod]
        public void GivenTransferMoneyWhenExecutedThenMoneyIsTransferred()
        {
            // Create context
            var accountContext = Container.Resolve<TransferMoneyFindAccountContext>();
            accountContext.SinkId = SinkId;
            accountContext.SourceId = SourceId;

            // Execute context to get an interaction
            TransferMoney tm = accountContext.Call();

            // Set up interaction
            tm.Amount = 200;    

            // Execute interaction
            tm.Run();

            //Assert
            Assert.AreEqual(9800, _account1.Balance);
            Assert.AreEqual(200, _account2.Balance);
        }
    }
}