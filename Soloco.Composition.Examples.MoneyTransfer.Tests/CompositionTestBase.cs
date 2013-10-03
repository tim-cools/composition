using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Soloco.Composition.Unity;

namespace Soloco.Composition.Examples.MoneyTransfer.Tests
{
    [TestClass]
    public abstract class CompositionTestBase
    {
        protected IUnityContainer Container { get; private set; }

        public abstract void Initialize();

        [TestInitialize]
        public void TestInitialize()
        {
            InitializeContainer();
            Initialize();
        }

        private void InitializeContainer()
        {
            Container = new UnityContainer();
            Container.AddNewExtension<CompositionExtension>();
            Container.RegisterType<ICompositeBuilderFactory, UnityCompositeBuilderFactory>();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            Container.Dispose();
        }
    }
}