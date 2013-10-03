using Microsoft.VisualStudio.TestTools.UnitTesting;

using Soloco.Composition.Example.Repositories;

namespace Soloco.Composition.Examples.Repositories.Tests.Services.StoreRepositorySpecifications
{
    public class When_retrieving_a_store_by_id
    {
        [TestClass]
        public class given_the_store_is_in_the_database : StoreRepositorySpecification
        {
            private Store _storesActual;
            private Store _storesExpected;

            protected override void Arrange()
            {
                base.Arrange();

                _storesExpected = TestStoreFactory.Create();
                Session.Persist(_storesExpected);
            }

            protected override void Act()
            {
                _storesActual = Repository.RetrieveById(_storesExpected.Id);
            }

            [TestMethod]
            public void then_the_store_should_be_returned()
            {
                Assert.IsNotNull(_storesActual);
                Assert.AreEqual(_storesExpected, _storesActual);
            }
        }

        [TestClass]
        public class given_no_stores_are_in_the_database : StoreRepositorySpecification
        {
            private Store _storeActual;

            protected override void Act()
            {
                _storeActual = Repository.RetrieveById(4);
            }

            [TestMethod]
            public void then_no_the_store_should_be_returned()
            {
                Assert.IsNull(_storeActual);
            }
        }
    }
}