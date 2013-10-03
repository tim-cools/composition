using NHibernate;

using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Soloco.Composition.Examples.Repositories.Tests
{
    [DeploymentItem("x86\\SQLite.Interop.dll")]
    [DeploymentItem("System.Data.SQLite.dll")]
    [TestClass]
    public abstract class DatabaseBasedSpecification : UnitSpecification
    {
        private ITransaction _transaction;

        public ISession Session { get; private set;  }
        public IUnityContainer Container { get; private set; }

        protected override void Arrange()
        {
            base.Arrange();

            CreateUnityContainer();
            OpenDatabase();
        }

        private void CreateUnityContainer()
        {
            Container = UnityContainerFactory.Create();
        }

        private void OpenDatabase()
        {
            Session = Container.Resolve<ISession>();
            _transaction = Session.BeginTransaction();
        }

        protected override void CleanUp()
        {
            base.CleanUp();

            CloseDatabase();
        }

        private void CloseDatabase()
        {
            _transaction.Rollback();
        }
    }
}