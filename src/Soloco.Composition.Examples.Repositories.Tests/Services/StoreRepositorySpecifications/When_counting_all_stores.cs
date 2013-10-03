using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Soloco.Composition.Examples.Repositories.Tests.Services.StoreRepositorySpecifications
{
    public class When_counting_all_stores
    {
        [TestClass]
        public class given_there_are_stores_in_the_database : StoreRepositorySpecification
        {
            private int _countActual;
            private int _countExpected;

            protected override void Arrange()
            {
                base.Arrange();

                var stores = TestStoreFactory.CreateList();
                _countExpected = stores.Count();
                Session.PersistAll(stores);
            }

            protected override void Act()
            {
                _countActual = Repository.Count();
            }

            [TestMethod]
            public void then_all_the_stores_should_be_returned()
            {
                Assert.AreEqual(_countExpected, _countActual);
            }
        }

        [TestClass]
        public class given_no_stores_are_in_the_database : StoreRepositorySpecification
        {
            private long _countActual;
            
            protected override void Act()
            {
                _countActual = Repository.Count();
            }

            [TestMethod]
            public void then_all_the_stores_should_be_returned()
            {
                Assert.AreEqual(0, _countActual);
            }
        }
    }
}